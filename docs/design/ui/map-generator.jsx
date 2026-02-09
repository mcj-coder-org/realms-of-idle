import { useState, useEffect, useRef, useCallback } from "react";

// Simple seeded PRNG
function mulberry32(a) {
  return function () {
    a |= 0; a = (a + 0x6d2b79f5) | 0;
    var t = Math.imul(a ^ (a >>> 15), 1 | a);
    t = (t + Math.imul(t ^ (t >>> 7), 61 | t)) ^ t;
    return ((t ^ (t >>> 14)) >>> 0) / 4294967296;
  };
}

// Simplex-like 2D noise using permutation table
function createNoise(seed) {
  const rng = mulberry32(seed);
  const perm = Array.from({ length: 512 }, (_, i) => i % 256);
  for (let i = 255; i > 0; i--) {
    const j = Math.floor(rng() * (i + 1));
    [perm[i], perm[j]] = [perm[j], perm[i]];
  }
  for (let i = 0; i < 256; i++) perm[i + 256] = perm[i];

  const grad = [
    [1, 1], [-1, 1], [1, -1], [-1, -1],
    [1, 0], [-1, 0], [0, 1], [0, -1],
  ];

  function dot(g, x, y) { return g[0] * x + g[1] * y; }
  function fade(t) { return t * t * t * (t * (t * 6 - 15) + 10); }
  function lerp(a, b, t) { return a + t * (b - a); }

  return function noise(x, y) {
    const X = Math.floor(x) & 255;
    const Y = Math.floor(y) & 255;
    const xf = x - Math.floor(x);
    const yf = y - Math.floor(y);
    const u = fade(xf);
    const v = fade(yf);
    const aa = perm[perm[X] + Y] % 8;
    const ab = perm[perm[X] + Y + 1] % 8;
    const ba = perm[perm[X + 1] + Y] % 8;
    const bb = perm[perm[X + 1] + Y + 1] % 8;
    return lerp(
      lerp(dot(grad[aa], xf, yf), dot(grad[ba], xf - 1, yf), u),
      lerp(dot(grad[ab], xf, yf - 1), dot(grad[bb], xf - 1, yf - 1), u),
      v
    );
  };
}

function fbm(noise, x, y, octaves = 6, lacunarity = 2, gain = 0.5) {
  let sum = 0, amp = 1, freq = 1, maxAmp = 0;
  for (let i = 0; i < octaves; i++) {
    sum += noise(x * freq, y * freq) * amp;
    maxAmp += amp;
    amp *= gain;
    freq *= lacunarity;
  }
  return sum / maxAmp;
}

// Biome definitions
const BIOMES = {
  DEEP_WATER:   { name: "Deep Water",   color: "#2a5c8a", char: "≈" },
  WATER:        { name: "Water",        color: "#3d85c6", char: "~" },
  SAND:         { name: "Beach",        color: "#e8d5a3", char: "." },
  PLAINS:       { name: "Plains",       color: "#7db46c", char: "," },
  MEADOW:       { name: "Meadow",       color: "#a8cf8e", char: "'" },
  FOREST:       { name: "Forest",       color: "#3a7d32", char: "♣" },
  DENSE_FOREST: { name: "Dense Forest", color: "#2d5a27", char: "♠" },
  HILLS:        { name: "Hills",        color: "#8b9e5e", char: "∩" },
  MOUNTAIN:     { name: "Mountain",     color: "#8a8a8a", char: "▲" },
  SNOW:         { name: "Snow Peak",    color: "#e8e8f0", char: "△" },
  SWAMP:        { name: "Swamp",        color: "#5a7a4a", char: "≋" },
  DESERT:       { name: "Desert",       color: "#d4b86a", char: "∴" },
};

const POI_TYPES = {
  VILLAGE:    { name: "Village",     icon: "🏘️", emoji: "V", color: "#e8c170" },
  TOWN:       { name: "Town",        icon: "🏰", emoji: "T", color: "#d4a054" },
  MINE:       { name: "Mine",        icon: "⛏️",  emoji: "M", color: "#7a6652" },
  CAVE:       { name: "Cave",        icon: "🕳️", emoji: "C", color: "#4a4a5a" },
  RUINS:      { name: "Ruins",       icon: "🏛️", emoji: "R", color: "#9a8a7a" },
  SHRINE:     { name: "Shrine",      icon: "⛩️", emoji: "S", color: "#c4a0d4" },
  FARM:       { name: "Farm",        icon: "🌾", emoji: "F", color: "#c8b060" },
  CAMP:       { name: "Camp",        icon: "⛺", emoji: "⌂", color: "#b87040" },
  TOWER:      { name: "Tower",       icon: "🗼", emoji: "!", color: "#6a6a8a" },
  DOCK:       { name: "Dock",        icon: "⚓", emoji: "D", color: "#5a7a9a" },
};

function getBiome(elevation, moisture) {
  if (elevation < 0.28) return BIOMES.DEEP_WATER;
  if (elevation < 0.35) return BIOMES.WATER;
  if (elevation < 0.38) return BIOMES.SAND;
  if (elevation < 0.5) {
    if (moisture > 0.65) return BIOMES.SWAMP;
    if (moisture < 0.3) return BIOMES.DESERT;
    if (moisture > 0.5) return BIOMES.MEADOW;
    return BIOMES.PLAINS;
  }
  if (elevation < 0.65) {
    if (moisture > 0.6) return BIOMES.DENSE_FOREST;
    if (moisture > 0.35) return BIOMES.FOREST;
    return BIOMES.HILLS;
  }
  if (elevation < 0.78) return BIOMES.MOUNTAIN;
  return BIOMES.SNOW;
}

function generateMap(width, height, seed, scale) {
  const elevNoise = createNoise(seed);
  const moistNoise = createNoise(seed + 1000);
  const detailNoise = createNoise(seed + 2000);

  const tiles = [];
  const elevations = [];
  const moistures = [];

  for (let y = 0; y < height; y++) {
    tiles[y] = [];
    elevations[y] = [];
    moistures[y] = [];
    for (let x = 0; x < width; x++) {
      const nx = x / scale;
      const ny = y / scale;

      let e = (fbm(elevNoise, nx, ny, 6) + 1) / 2;
      // Island shaping — push edges toward water
      const dx = (x / width - 0.5) * 2;
      const dy = (y / height - 0.5) * 2;
      const dist = Math.sqrt(dx * dx + dy * dy);
      e = e * (1 - 0.6 * Math.pow(dist, 2.2));
      e = Math.max(0, Math.min(1, e));

      let m = (fbm(moistNoise, nx * 0.8, ny * 0.8, 4) + 1) / 2;
      m = Math.max(0, Math.min(1, m));

      elevations[y][x] = e;
      moistures[y][x] = m;
      tiles[y][x] = getBiome(e, m);
    }
  }

  return { tiles, elevations, moistures };
}

function placePOIs(tiles, elevations, moistures, width, height, seed) {
  const rng = mulberry32(seed + 5000);
  const pois = [];

  function isBiome(x, y, ...biomes) {
    if (x < 0 || x >= width || y < 0 || y >= height) return false;
    return biomes.includes(tiles[y][x]);
  }

  function nearWater(x, y, radius = 3) {
    for (let dy = -radius; dy <= radius; dy++)
      for (let dx = -radius; dx <= radius; dx++)
        if (isBiome(x + dx, y + dy, BIOMES.WATER, BIOMES.DEEP_WATER)) return true;
    return false;
  }

  function tooClose(x, y, minDist = 6) {
    return pois.some(p => Math.abs(p.x - x) + Math.abs(p.y - y) < minDist);
  }

  // Place towns on plains/meadow near water
  for (let attempts = 0; attempts < 200 && pois.filter(p => p.type === POI_TYPES.TOWN).length < 3; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    if (isBiome(x, y, BIOMES.PLAINS, BIOMES.MEADOW) && nearWater(x, y, 5) && !tooClose(x, y, 12)) {
      pois.push({ x, y, type: POI_TYPES.TOWN });
    }
  }

  // Villages on plains/meadow/forest edges
  for (let attempts = 0; attempts < 300 && pois.filter(p => p.type === POI_TYPES.VILLAGE).length < 6; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    if (isBiome(x, y, BIOMES.PLAINS, BIOMES.MEADOW, BIOMES.FOREST) && !tooClose(x, y, 8)) {
      pois.push({ x, y, type: POI_TYPES.VILLAGE });
    }
  }

  // Mines in mountains/hills
  for (let attempts = 0; attempts < 200 && pois.filter(p => p.type === POI_TYPES.MINE).length < 4; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    if (isBiome(x, y, BIOMES.MOUNTAIN, BIOMES.HILLS) && !tooClose(x, y, 7)) {
      pois.push({ x, y, type: POI_TYPES.MINE });
    }
  }

  // Caves in mountains
  for (let attempts = 0; attempts < 150 && pois.filter(p => p.type === POI_TYPES.CAVE).length < 3; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    if (isBiome(x, y, BIOMES.MOUNTAIN, BIOMES.HILLS) && !tooClose(x, y, 8)) {
      pois.push({ x, y, type: POI_TYPES.CAVE });
    }
  }

  // Ruins scattered
  for (let attempts = 0; attempts < 200 && pois.filter(p => p.type === POI_TYPES.RUINS).length < 3; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    if (!isBiome(x, y, BIOMES.WATER, BIOMES.DEEP_WATER, BIOMES.SNOW) && !tooClose(x, y, 10)) {
      pois.push({ x, y, type: POI_TYPES.RUINS });
    }
  }

  // Shrines in forests/mountains
  for (let attempts = 0; attempts < 200 && pois.filter(p => p.type === POI_TYPES.SHRINE).length < 3; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    if (isBiome(x, y, BIOMES.FOREST, BIOMES.DENSE_FOREST, BIOMES.MOUNTAIN) && !tooClose(x, y, 10)) {
      pois.push({ x, y, type: POI_TYPES.SHRINE });
    }
  }

  // Farms near towns/villages
  for (let attempts = 0; attempts < 200 && pois.filter(p => p.type === POI_TYPES.FARM).length < 4; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    const nearSettlement = pois.some(p =>
      (p.type === POI_TYPES.TOWN || p.type === POI_TYPES.VILLAGE) &&
      Math.abs(p.x - x) + Math.abs(p.y - y) < 10
    );
    if (isBiome(x, y, BIOMES.PLAINS, BIOMES.MEADOW) && nearSettlement && !tooClose(x, y, 5)) {
      pois.push({ x, y, type: POI_TYPES.FARM });
    }
  }

  // Camps in forests/desert
  for (let attempts = 0; attempts < 150 && pois.filter(p => p.type === POI_TYPES.CAMP).length < 3; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    if (isBiome(x, y, BIOMES.FOREST, BIOMES.DENSE_FOREST, BIOMES.DESERT) && !tooClose(x, y, 8)) {
      pois.push({ x, y, type: POI_TYPES.CAMP });
    }
  }

  // Towers on hills
  for (let attempts = 0; attempts < 150 && pois.filter(p => p.type === POI_TYPES.TOWER).length < 2; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    if (isBiome(x, y, BIOMES.HILLS, BIOMES.MOUNTAIN) && !tooClose(x, y, 12)) {
      pois.push({ x, y, type: POI_TYPES.TOWER });
    }
  }

  // Docks near water on sand/plains
  for (let attempts = 0; attempts < 150 && pois.filter(p => p.type === POI_TYPES.DOCK).length < 3; attempts++) {
    const x = Math.floor(rng() * width);
    const y = Math.floor(rng() * height);
    if (isBiome(x, y, BIOMES.SAND, BIOMES.PLAINS) && nearWater(x, y, 2) && !tooClose(x, y, 10)) {
      pois.push({ x, y, type: POI_TYPES.DOCK });
    }
  }

  return pois;
}

// Generate paths between settlements
function generatePaths(pois, tiles, width, height) {
  const settlements = pois.filter(p =>
    p.type === POI_TYPES.TOWN || p.type === POI_TYPES.VILLAGE
  );

  const paths = [];

  // Connect each settlement to nearest 1-2 others
  for (const s of settlements) {
    const others = settlements
      .filter(o => o !== s)
      .map(o => ({ ...o, dist: Math.hypot(o.x - s.x, o.y - s.y) }))
      .sort((a, b) => a.dist - b.dist)
      .slice(0, 2);

    for (const target of others) {
      // Check if path already exists
      const exists = paths.some(p =>
        (p.from === s && p.to === target) || (p.from === target && p.to === s)
      );
      if (!exists) {
        // Simple A*-ish bresenham with slight wander
        const points = [];
        let cx = s.x, cy = s.y;
        while (Math.abs(cx - target.x) > 1 || Math.abs(cy - target.y) > 1) {
          points.push({ x: Math.round(cx), y: Math.round(cy) });
          const dx = target.x - cx;
          const dy = target.y - cy;
          const len = Math.sqrt(dx * dx + dy * dy);
          cx += dx / len * 1.2;
          cy += dy / len * 1.2;
        }
        paths.push({ from: s, to: target, points });
      }
    }
  }

  return paths;
}

const MAP_W = 96;
const MAP_H = 72;

export default function CozyMapGenerator() {
  const [seed, setSeed] = useState(42);
  const [scale, setScale] = useState(28);
  const [showPOIs, setShowPOIs] = useState(true);
  const [showPaths, setShowPaths] = useState(true);
  const [showGrid, setShowGrid] = useState(false);
  const [hoveredTile, setHoveredTile] = useState(null);
  const [hoveredPOI, setHoveredPOI] = useState(null);
  const [mapName, setMapName] = useState("");
  const canvasRef = useRef(null);
  const [mapData, setMapData] = useState(null);
  const [tileSize, setTileSize] = useState(7);

  const REGION_NAMES_A = ["Emerald", "Whispering", "Golden", "Misty", "Silver", "Moonlit", "Sunken", "Amber", "Crimson", "Verdant", "Frostborn", "Ancient", "Hollow", "Wandering", "Starlit"];
  const REGION_NAMES_B = ["Vale", "Reach", "Shire", "Crossing", "Haven", "Marsh", "Glade", "Peaks", "Hollow", "Dell", "Coast", "Basin", "Ridge", "Wilds", "Expanse"];

  const generateName = useCallback((s) => {
    const rng = mulberry32(s + 9999);
    const a = REGION_NAMES_A[Math.floor(rng() * REGION_NAMES_A.length)];
    const b = REGION_NAMES_B[Math.floor(rng() * REGION_NAMES_B.length)];
    return `The ${a} ${b}`;
  }, []);

  const generate = useCallback(() => {
    const { tiles, elevations, moistures } = generateMap(MAP_W, MAP_H, seed, scale);
    const pois = placePOIs(tiles, elevations, moistures, MAP_W, MAP_H, seed);
    const paths = generatePaths(pois, tiles, MAP_W, MAP_H);
    setMapData({ tiles, elevations, moistures, pois, paths });
    setMapName(generateName(seed));
  }, [seed, scale, generateName]);

  useEffect(() => { generate(); }, [generate]);

  // Canvas rendering
  useEffect(() => {
    if (!mapData || !canvasRef.current) return;
    const canvas = canvasRef.current;
    const ctx = canvas.getContext("2d");
    const ts = tileSize;
    canvas.width = MAP_W * ts;
    canvas.height = MAP_H * ts;

    const { tiles, elevations, pois, paths } = mapData;

    // Draw tiles
    for (let y = 0; y < MAP_H; y++) {
      for (let x = 0; x < MAP_W; x++) {
        const biome = tiles[y][x];
        const e = elevations[y][x];

        // Base color with slight elevation shading
        let r = parseInt(biome.color.slice(1, 3), 16);
        let g = parseInt(biome.color.slice(3, 5), 16);
        let b = parseInt(biome.color.slice(5, 7), 16);

        // Subtle variation
        const shade = (e - 0.5) * 30;
        r = Math.max(0, Math.min(255, r + shade));
        g = Math.max(0, Math.min(255, g + shade));
        b = Math.max(0, Math.min(255, b + shade));

        ctx.fillStyle = `rgb(${r|0},${g|0},${b|0})`;
        ctx.fillRect(x * ts, y * ts, ts, ts);

        // Pixel detail for forests
        if ((biome === BIOMES.FOREST || biome === BIOMES.DENSE_FOREST) && ts >= 5) {
          if ((x + y * 3) % 4 === 0) {
            ctx.fillStyle = biome === BIOMES.DENSE_FOREST ? "#1e4a1a" : "#2d6625";
            ctx.fillRect(x * ts + 1, y * ts, ts - 2, ts - 2);
          }
        }

        // Wave effect for water
        if ((biome === BIOMES.WATER || biome === BIOMES.DEEP_WATER) && ts >= 5) {
          if ((x + y) % 5 === 0) {
            ctx.fillStyle = "rgba(255,255,255,0.12)";
            ctx.fillRect(x * ts + 1, y * ts + Math.floor(ts/2), ts - 2, 1);
          }
        }

        // Snow sparkle
        if (biome === BIOMES.SNOW && (x * 7 + y * 13) % 9 === 0 && ts >= 5) {
          ctx.fillStyle = "rgba(255,255,255,0.4)";
          ctx.fillRect(x * ts + 2, y * ts + 1, 1, 1);
        }

        // Sand texture
        if ((biome === BIOMES.SAND || biome === BIOMES.DESERT) && (x * 3 + y * 7) % 6 === 0 && ts >= 5) {
          ctx.fillStyle = "rgba(180,150,80,0.3)";
          ctx.fillRect(x * ts + 2, y * ts + 2, 1, 1);
        }
      }
    }

    // Grid
    if (showGrid) {
      ctx.strokeStyle = "rgba(0,0,0,0.08)";
      ctx.lineWidth = 0.5;
      for (let x = 0; x <= MAP_W; x++) {
        ctx.beginPath(); ctx.moveTo(x * ts, 0); ctx.lineTo(x * ts, MAP_H * ts); ctx.stroke();
      }
      for (let y = 0; y <= MAP_H; y++) {
        ctx.beginPath(); ctx.moveTo(0, y * ts); ctx.lineTo(MAP_W * ts, y * ts); ctx.stroke();
      }
    }

    // Paths
    if (showPaths && paths.length > 0) {
      ctx.strokeStyle = "rgba(139,119,90,0.6)";
      ctx.lineWidth = ts >= 6 ? 2 : 1;
      ctx.setLineDash(ts >= 6 ? [3, 3] : [2, 2]);
      for (const path of paths) {
        ctx.beginPath();
        for (let i = 0; i < path.points.length; i++) {
          const p = path.points[i];
          const px = p.x * ts + ts / 2;
          const py = p.y * ts + ts / 2;
          if (i === 0) ctx.moveTo(px, py);
          else ctx.lineTo(px, py);
        }
        ctx.stroke();
      }
      ctx.setLineDash([]);
    }

    // POIs
    if (showPOIs) {
      for (const poi of pois) {
        const px = poi.x * ts;
        const py = poi.y * ts;
        const size = ts * 2.5;

        // Glow background
        ctx.fillStyle = "rgba(0,0,0,0.35)";
        ctx.beginPath();
        ctx.arc(px + ts/2, py + ts/2, size * 0.5, 0, Math.PI * 2);
        ctx.fill();

        ctx.fillStyle = poi.type.color;
        ctx.beginPath();
        ctx.arc(px + ts/2, py + ts/2, size * 0.38, 0, Math.PI * 2);
        ctx.fill();

        // Icon
        ctx.fillStyle = "#fff";
        ctx.font = `bold ${Math.max(8, ts)}px monospace`;
        ctx.textAlign = "center";
        ctx.textBaseline = "middle";
        ctx.fillText(poi.type.emoji, px + ts/2, py + ts/2 + 0.5);
      }
    }

    // Hover highlight
    if (hoveredTile) {
      ctx.strokeStyle = "rgba(255,255,255,0.7)";
      ctx.lineWidth = 1.5;
      ctx.strokeRect(hoveredTile.x * ts, hoveredTile.y * ts, ts, ts);
    }
  }, [mapData, showPOIs, showPaths, showGrid, hoveredTile, tileSize]);

  const handleCanvasMove = useCallback((e) => {
    if (!mapData) return;
    const canvas = canvasRef.current;
    const rect = canvas.getBoundingClientRect();
    const scaleX = canvas.width / rect.width;
    const scaleY = canvas.height / rect.height;
    const mx = Math.floor((e.clientX - rect.left) * scaleX / tileSize);
    const my = Math.floor((e.clientY - rect.top) * scaleY / tileSize);

    if (mx >= 0 && mx < MAP_W && my >= 0 && my < MAP_H) {
      setHoveredTile({ x: mx, y: my });
      const poi = mapData.pois.find(p => Math.abs(p.x - mx) <= 1 && Math.abs(p.y - my) <= 1);
      setHoveredPOI(poi || null);
    } else {
      setHoveredTile(null);
      setHoveredPOI(null);
    }
  }, [mapData, tileSize]);

  const biomeCounts = mapData ? (() => {
    const counts = {};
    for (let y = 0; y < MAP_H; y++)
      for (let x = 0; x < MAP_W; x++) {
        const b = mapData.tiles[y][x].name;
        counts[b] = (counts[b] || 0) + 1;
      }
    return Object.entries(counts).sort((a, b) => b[1] - a[1]);
  })() : [];

  const totalTiles = MAP_W * MAP_H;

  return (
    <div style={{
      minHeight: "100vh",
      background: "linear-gradient(145deg, #1a1a2e 0%, #16213e 50%, #1a1a2e 100%)",
      color: "#d4cfc4",
      fontFamily: "'Courier New', monospace",
      padding: "20px",
      boxSizing: "border-box",
    }}>
      {/* Title */}
      <div style={{ textAlign: "center", marginBottom: 16 }}>
        <h1 style={{
          fontSize: 26,
          color: "#e8d5a3",
          letterSpacing: 4,
          margin: 0,
          textShadow: "0 0 20px rgba(232,213,163,0.3)",
          fontFamily: "'Courier New', monospace",
        }}>
          ⚔ REALM CARTOGRAPHER ⚔
        </h1>
        <div style={{ fontSize: 11, color: "#7a7a8a", letterSpacing: 2, marginTop: 4 }}>
          PROCEDURAL COZY RPG MAP GENERATOR
        </div>
      </div>

      <div style={{ display: "flex", gap: 16, maxWidth: 1100, margin: "0 auto", flexWrap: "wrap", justifyContent: "center" }}>
        {/* Controls */}
        <div style={{
          width: 220,
          flexShrink: 0,
          display: "flex",
          flexDirection: "column",
          gap: 10,
        }}>
          {/* Seed */}
          <div style={panelStyle}>
            <label style={labelStyle}>SEED</label>
            <div style={{ display: "flex", gap: 4 }}>
              <input
                type="number"
                value={seed}
                onChange={e => setSeed(Number(e.target.value))}
                style={inputStyle}
              />
              <button
                onClick={() => setSeed(Math.floor(Math.random() * 99999))}
                style={btnSmallStyle}
                title="Random seed"
              >🎲</button>
            </div>
          </div>

          {/* Scale */}
          <div style={panelStyle}>
            <label style={labelStyle}>TERRAIN SCALE: {scale}</label>
            <input
              type="range" min={12} max={50} value={scale}
              onChange={e => setScale(Number(e.target.value))}
              style={{ width: "100%", accentColor: "#e8d5a3" }}
            />
            <div style={{ display: "flex", justifyContent: "space-between", fontSize: 9, color: "#6a6a7a" }}>
              <span>Jagged</span><span>Smooth</span>
            </div>
          </div>

          {/* Tile Size */}
          <div style={panelStyle}>
            <label style={labelStyle}>TILE SIZE: {tileSize}px</label>
            <input
              type="range" min={4} max={10} value={tileSize}
              onChange={e => setTileSize(Number(e.target.value))}
              style={{ width: "100%", accentColor: "#e8d5a3" }}
            />
            <div style={{ display: "flex", justifyContent: "space-between", fontSize: 9, color: "#6a6a7a" }}>
              <span>Compact</span><span>Large</span>
            </div>
          </div>

          {/* Toggles */}
          <div style={panelStyle}>
            <label style={labelStyle}>LAYERS</label>
            {[
              ["Points of Interest", showPOIs, setShowPOIs],
              ["Trade Routes", showPaths, setShowPaths],
              ["Grid Overlay", showGrid, setShowGrid],
            ].map(([label, val, set]) => (
              <label key={label} style={{
                display: "flex", alignItems: "center", gap: 8,
                fontSize: 11, cursor: "pointer", padding: "3px 0",
              }}>
                <input
                  type="checkbox" checked={val}
                  onChange={e => set(e.target.checked)}
                  style={{ accentColor: "#e8d5a3" }}
                />
                {label}
              </label>
            ))}
          </div>

          {/* Generate Button */}
          <button onClick={() => setSeed(Math.floor(Math.random() * 99999))} style={{
            ...btnSmallStyle,
            padding: "10px 0",
            fontSize: 13,
            letterSpacing: 2,
            background: "linear-gradient(180deg, #4a3a2a, #3a2a1a)",
            border: "2px solid #6a5a3a",
          }}>
            ✦ NEW REALM ✦
          </button>

          {/* Biome Legend */}
          <div style={{ ...panelStyle, maxHeight: 300, overflowY: "auto" }}>
            <label style={labelStyle}>BIOMES</label>
            {biomeCounts.map(([name, count]) => {
              const biome = Object.values(BIOMES).find(b => b.name === name);
              return (
                <div key={name} style={{ display: "flex", alignItems: "center", gap: 6, fontSize: 10, padding: "2px 0" }}>
                  <div style={{
                    width: 12, height: 12, borderRadius: 2,
                    background: biome?.color || "#555",
                    border: "1px solid rgba(255,255,255,0.15)",
                    flexShrink: 0,
                  }} />
                  <span style={{ flex: 1 }}>{name}</span>
                  <span style={{ color: "#6a6a7a" }}>{((count / totalTiles) * 100).toFixed(0)}%</span>
                </div>
              );
            })}
          </div>

          {/* POI Legend */}
          {showPOIs && mapData && (
            <div style={panelStyle}>
              <label style={labelStyle}>LOCATIONS ({mapData.pois.length})</label>
              {Object.values(POI_TYPES).map(type => {
                const count = mapData.pois.filter(p => p.type === type).length;
                if (count === 0) return null;
                return (
                  <div key={type.name} style={{ display: "flex", alignItems: "center", gap: 6, fontSize: 10, padding: "2px 0" }}>
                    <span style={{ width: 16, textAlign: "center" }}>{type.icon}</span>
                    <span style={{ flex: 1 }}>{type.name}</span>
                    <span style={{ color: "#6a6a7a" }}>×{count}</span>
                  </div>
                );
              })}
            </div>
          )}
        </div>

        {/* Map Canvas */}
        <div style={{ flex: 1, minWidth: 0 }}>
          {/* Map Title */}
          <div style={{
            textAlign: "center",
            padding: "8px 0",
            fontSize: 18,
            color: "#e8d5a3",
            letterSpacing: 3,
            textShadow: "0 0 10px rgba(232,213,163,0.2)",
          }}>
            {mapName}
          </div>

          <div style={{
            background: "rgba(0,0,0,0.3)",
            border: "2px solid #3a3a4a",
            borderRadius: 4,
            padding: 4,
            overflow: "auto",
            maxWidth: "100%",
            position: "relative",
          }}>
            <canvas
              ref={canvasRef}
              onMouseMove={handleCanvasMove}
              onMouseLeave={() => { setHoveredTile(null); setHoveredPOI(null); }}
              style={{
                display: "block",
                imageRendering: "pixelated",
                width: MAP_W * tileSize,
                maxWidth: "100%",
                cursor: "crosshair",
              }}
            />
          </div>

          {/* Info Bar */}
          <div style={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            padding: "8px 4px",
            fontSize: 11,
            color: "#7a7a8a",
            borderTop: "1px solid #2a2a3a",
            marginTop: 4,
            flexWrap: "wrap",
            gap: 8,
          }}>
            <span>
              {hoveredTile
                ? `[${hoveredTile.x}, ${hoveredTile.y}] ${mapData?.tiles[hoveredTile.y][hoveredTile.x].name} · E:${mapData?.elevations[hoveredTile.y][hoveredTile.x].toFixed(2)} M:${mapData?.moistures[hoveredTile.y][hoveredTile.x].toFixed(2)}`
                : "Hover over the map to inspect tiles"
              }
            </span>
            {hoveredPOI && (
              <span style={{ color: hoveredPOI.type.color }}>
                {hoveredPOI.type.icon} {hoveredPOI.type.name}
              </span>
            )}
            <span>{MAP_W}×{MAP_H} · Seed: {seed}</span>
          </div>
        </div>
      </div>

      {/* Algo notes */}
      <div style={{
        maxWidth: 1100,
        margin: "16px auto 0",
        padding: 12,
        background: "rgba(0,0,0,0.2)",
        border: "1px solid #2a2a3a",
        borderRadius: 4,
        fontSize: 10,
        color: "#5a5a6a",
        lineHeight: 1.6,
      }}>
        <strong style={{ color: "#7a7a8a" }}>Generation Pipeline:</strong>{" "}
        FBM noise (6 octaves) → island mask → elevation/moisture biome lookup →
        POI placement (biome-weighted with min-distance constraints) →
        path generation between settlements → canvas render with per-biome pixel details
      </div>
    </div>
  );
}

const panelStyle = {
  background: "rgba(0,0,0,0.25)",
  border: "1px solid #2a2a3a",
  borderRadius: 4,
  padding: "8px 10px",
};

const labelStyle = {
  display: "block",
  fontSize: 10,
  letterSpacing: 2,
  color: "#8a8a9a",
  marginBottom: 6,
  fontWeight: "bold",
};

const inputStyle = {
  flex: 1,
  background: "#1a1a2a",
  border: "1px solid #3a3a4a",
  color: "#d4cfc4",
  padding: "4px 8px",
  borderRadius: 3,
  fontFamily: "'Courier New', monospace",
  fontSize: 12,
};

const btnSmallStyle = {
  background: "linear-gradient(180deg, #3a3a4a, #2a2a3a)",
  border: "1px solid #4a4a5a",
  color: "#d4cfc4",
  padding: "4px 8px",
  borderRadius: 3,
  cursor: "pointer",
  fontFamily: "'Courier New', monospace",
  fontSize: 13,
};
