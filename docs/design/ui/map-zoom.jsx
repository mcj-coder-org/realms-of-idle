import { useState, useEffect, useRef, useCallback, useMemo } from 'react';

// ── PRNG & Noise ──────────────────────────────────────────────
function mulberry32(a) {
  return function () {
    a |= 0;
    a = (a + 0x6d2b79f5) | 0;
    var t = Math.imul(a ^ (a >>> 15), 1 | a);
    t = (t + Math.imul(t ^ (t >>> 7), 61 | t)) ^ t;
    return ((t ^ (t >>> 14)) >>> 0) / 4294967296;
  };
}

function createNoise(seed) {
  const rng = mulberry32(seed);
  const perm = Array.from({ length: 512 }, (_, i) => i % 256);
  for (let i = 255; i > 0; i--) {
    const j = Math.floor(rng() * (i + 1));
    [perm[i], perm[j]] = [perm[j], perm[i]];
  }
  for (let i = 0; i < 256; i++) perm[i + 256] = perm[i];
  const grad = [
    [1, 1],
    [-1, 1],
    [1, -1],
    [-1, -1],
    [1, 0],
    [-1, 0],
    [0, 1],
    [0, -1],
  ];
  function dot(g, x, y) {
    return g[0] * x + g[1] * y;
  }
  function fade(t) {
    return t * t * t * (t * (t * 6 - 15) + 10);
  }
  function lerp(a, b, t) {
    return a + t * (b - a);
  }
  return function (x, y) {
    const X = Math.floor(x) & 255,
      Y = Math.floor(y) & 255;
    const xf = x - Math.floor(x),
      yf = y - Math.floor(y);
    const u = fade(xf),
      v = fade(yf);
    return lerp(
      lerp(
        dot(grad[perm[perm[X] + Y] % 8], xf, yf),
        dot(grad[perm[perm[X + 1] + Y] % 8], xf - 1, yf),
        u
      ),
      lerp(
        dot(grad[perm[perm[X] + Y + 1] % 8], xf, yf - 1),
        dot(grad[perm[perm[X + 1] + Y + 1] % 8], xf - 1, yf - 1),
        u
      ),
      v
    );
  };
}

function fbm(noise, x, y, oct = 6, lac = 2, gain = 0.5) {
  let sum = 0,
    amp = 1,
    freq = 1,
    mx = 0;
  for (let i = 0; i < oct; i++) {
    sum += noise(x * freq, y * freq) * amp;
    mx += amp;
    amp *= gain;
    freq *= lac;
  }
  return sum / mx;
}

// ── Biomes ────────────────────────────────────────────────────
const BIOMES = {
  DEEP_WATER: { id: 'dw', name: 'Deep Ocean', color: '#2a5c8a', darkColor: '#1e4a72' },
  WATER: { id: 'w', name: 'Water', color: '#3d85c6', darkColor: '#2d6da6' },
  SAND: { id: 'sa', name: 'Beach', color: '#e8d5a3', darkColor: '#d4c090' },
  PLAINS: { id: 'pl', name: 'Plains', color: '#7db46c', darkColor: '#6a9a5a' },
  MEADOW: { id: 'me', name: 'Meadow', color: '#a8cf8e', darkColor: '#90b87a' },
  FOREST: { id: 'fo', name: 'Forest', color: '#3a7d32', darkColor: '#2d6627' },
  DENSE_FOREST: { id: 'df', name: 'Dense Forest', color: '#2d5a27', darkColor: '#1f4a1a' },
  HILLS: { id: 'hi', name: 'Hills', color: '#8b9e5e', darkColor: '#7a8a4e' },
  MOUNTAIN: { id: 'mt', name: 'Mountain', color: '#8a8a8a', darkColor: '#6a6a6a' },
  SNOW: { id: 'sn', name: 'Snow Peak', color: '#e8e8f0', darkColor: '#c8c8d8' },
  SWAMP: { id: 'sw', name: 'Swamp', color: '#5a7a4a', darkColor: '#4a6a3a' },
  DESERT: { id: 'de', name: 'Desert', color: '#d4b86a', darkColor: '#b8a058' },
};

function getBiome(e, m) {
  if (e < 0.28) return BIOMES.DEEP_WATER;
  if (e < 0.35) return BIOMES.WATER;
  if (e < 0.38) return BIOMES.SAND;
  if (e < 0.5) {
    if (m > 0.65) return BIOMES.SWAMP;
    if (m < 0.3) return BIOMES.DESERT;
    if (m > 0.5) return BIOMES.MEADOW;
    return BIOMES.PLAINS;
  }
  if (e < 0.65) {
    if (m > 0.6) return BIOMES.DENSE_FOREST;
    if (m > 0.35) return BIOMES.FOREST;
    return BIOMES.HILLS;
  }
  if (e < 0.78) return BIOMES.MOUNTAIN;
  return BIOMES.SNOW;
}

// ── POI Types per zoom level ──────────────────────────────────
const POI_WORLD = {
  CAPITAL: {
    name: 'Capital',
    emoji: '♛',
    color: '#f0d060',
    minBiome: ['pl', 'me'],
    nearWater: true,
  },
  CITY: {
    name: 'City',
    emoji: '▣',
    color: '#d4a054',
    minBiome: ['pl', 'me', 'fo'],
    nearWater: false,
  },
  FORTRESS: {
    name: 'Fortress',
    emoji: '⊞',
    color: '#8a7a6a',
    minBiome: ['hi', 'mt'],
    nearWater: false,
  },
  PORT: { name: 'Port', emoji: '⚓', color: '#5a9aba', minBiome: ['sa', 'pl'], nearWater: true },
  DUNGEON: {
    name: 'Dungeon',
    emoji: '▼',
    color: '#9a4a6a',
    minBiome: ['mt', 'hi', 'df'],
    nearWater: false,
  },
};

const POI_REGION = {
  TOWN: { name: 'Town', emoji: '◈', color: '#e8c170', minBiome: ['pl', 'me'], nearWater: false },
  VILLAGE: {
    name: 'Village',
    emoji: '◇',
    color: '#b8a870',
    minBiome: ['pl', 'me', 'fo'],
    nearWater: false,
  },
  MINE: { name: 'Mine', emoji: '⛏', color: '#7a6652', minBiome: ['mt', 'hi'], nearWater: false },
  CAVE: { name: 'Cave', emoji: '◖', color: '#4a4a5a', minBiome: ['mt', 'hi'], nearWater: false },
  RUINS: {
    name: 'Ruins',
    emoji: 'Ω',
    color: '#9a8a7a',
    minBiome: ['pl', 'me', 'fo', 'df', 'hi', 'de', 'sw'],
    nearWater: false,
  },
  SHRINE: {
    name: 'Shrine',
    emoji: '✦',
    color: '#c4a0d4',
    minBiome: ['fo', 'df', 'mt'],
    nearWater: false,
  },
  FARM: { name: 'Farm', emoji: '≋', color: '#c8b060', minBiome: ['pl', 'me'], nearWater: false },
  CAMP: {
    name: 'Camp',
    emoji: '△',
    color: '#b87040',
    minBiome: ['fo', 'df', 'de'],
    nearWater: false,
  },
  TOWER: { name: 'Tower', emoji: '↑', color: '#6a6a8a', minBiome: ['hi', 'mt'], nearWater: false },
  DOCK: { name: 'Dock', emoji: '⊏', color: '#5a7a9a', minBiome: ['sa', 'pl'], nearWater: true },
};

// ── Settlement building types ─────────────────────────────────
const BUILDING_TYPES = {
  HOUSE: { name: 'House', emoji: '⌂', color: '#b89a6a' },
  SHOP: { name: 'Shop', emoji: '$', color: '#c8a850' },
  TAVERN: { name: 'Tavern', emoji: 'T', color: '#d4884a' },
  SMITHY: { name: 'Smithy', emoji: '⚒', color: '#8a6a5a' },
  TEMPLE: { name: 'Temple', emoji: '✝', color: '#a090c0' },
  BARRACKS: { name: 'Barracks', emoji: '⊞', color: '#7a7a8a' },
  MARKET: { name: 'Market', emoji: '◈', color: '#c4b060' },
  WELL: { name: 'Well', emoji: '○', color: '#6aaaca' },
  GARDEN: { name: 'Garden', emoji: '♣', color: '#6ab060' },
  WALL: { name: 'Wall', emoji: '█', color: '#6a5a4a' },
  GATE: { name: 'Gate', emoji: '⊓', color: '#8a7a5a' },
  HALL: { name: 'Guild Hall', emoji: 'H', color: '#b8a070' },
};

const SETTLEMENT_GROUND = {
  ROAD: { name: 'Road', color: '#8a7a60' },
  DIRT: { name: 'Dirt', color: '#a09070' },
  GRASS: { name: 'Grass', color: '#6a9a5a' },
  STONE: { name: 'Stone', color: '#9a9a8a' },
  WATER: { name: 'Water', color: '#4a80b0' },
  BRIDGE: { name: 'Bridge', color: '#9a8a6a' },
};

// ── Name Generation ───────────────────────────────────────────
const N = {
  pre: [
    'Elm',
    'Oak',
    'Iron',
    'Storm',
    'Moon',
    'Star',
    'Silver',
    'Gold',
    'Green',
    'Dark',
    'White',
    'Raven',
    'Wolf',
    'Frost',
    'Dawn',
    'Dusk',
    'Amber',
    'Thorn',
    'Shadow',
    'Copper',
  ],
  mid: [
    'wood',
    'vale',
    'ford',
    'wick',
    'holm',
    'dale',
    'mere',
    'stead',
    'bridge',
    'haven',
    'watch',
    'crest',
    'fell',
    'brook',
    'moor',
    'gate',
    'stone',
    'field',
    'marsh',
    'cliff',
  ],
  suf: ['ton', 'bury', 'ham', 'shire', 'land', 'keep', 'hold', 'spire', 'wall', 'rest'],
  adj: [
    'Whispering',
    'Ancient',
    'Forgotten',
    'Golden',
    'Emerald',
    'Crimson',
    'Silent',
    'Wandering',
    'Sunken',
    'Misty',
    'Hollow',
    'Verdant',
    'Frostborn',
    'Starlit',
    'Amber',
    'Twilight',
  ],
  noun: [
    'Vale',
    'Reach',
    'Shire',
    'Crossing',
    'Haven',
    'Glade',
    'Peaks',
    'Hollow',
    'Dell',
    'Coast',
    'Basin',
    'Ridge',
    'Wilds',
    'Expanse',
    'Frontier',
    'Dominion',
  ],
};

function genName(seed, style = 'settlement') {
  const r = mulberry32(seed);
  const pick = a => a[Math.floor(r() * a.length)];
  if (style === 'world') return `The ${pick(N.adj)} ${pick(N.noun)}`;
  if (style === 'region') return `${pick(N.adj)} ${pick(N.noun)}`;
  return pick(N.pre) + pick(N.mid) + (r() > 0.5 ? pick(N.suf) : '');
}

// ── Zoom Levels ───────────────────────────────────────────────
const ZOOM = {
  WORLD: { id: 0, label: 'World Map', gridW: 96, gridH: 72, scale: 28, tileSize: 7 },
  REGION: { id: 1, label: 'Region Map', gridW: 80, gridH: 60, scale: 22, tileSize: 8 },
  SETTLEMENT: { id: 2, label: 'Settlement', gridW: 48, gridH: 40, scale: 12, tileSize: 12 },
};

// ── Map Generation ────────────────────────────────────────────
function generateTerrain(w, h, seed, scale, islandMask = true) {
  const eN = createNoise(seed),
    mN = createNoise(seed + 1000);
  const tiles = [],
    elevs = [],
    moists = [];
  for (let y = 0; y < h; y++) {
    tiles[y] = [];
    elevs[y] = [];
    moists[y] = [];
    for (let x = 0; x < w; x++) {
      let e = (fbm(eN, x / scale, y / scale, 6) + 1) / 2;
      if (islandMask) {
        const dx = (x / w - 0.5) * 2,
          dy = (y / h - 0.5) * 2;
        e *= 1 - 0.6 * Math.pow(Math.sqrt(dx * dx + dy * dy), 2.2);
      }
      e = Math.max(0, Math.min(1, e));
      let m = (fbm(mN, (x / scale) * 0.8, (y / scale) * 0.8, 4) + 1) / 2;
      m = Math.max(0, Math.min(1, m));
      elevs[y][x] = e;
      moists[y][x] = m;
      tiles[y][x] = getBiome(e, m);
    }
  }
  return { tiles, elevs, moists };
}

function placePOIsForLevel(tiles, w, h, seed, poiDefs, counts) {
  const rng = mulberry32(seed + 5000);
  const pois = [];
  function biomeAt(x, y) {
    return x >= 0 && x < w && y >= 0 && y < h ? tiles[y][x] : null;
  }
  function nearWater(x, y, r = 3) {
    for (let dy = -r; dy <= r; dy++)
      for (let dx = -r; dx <= r; dx++) {
        const b = biomeAt(x + dx, y + dy);
        if (b === BIOMES.WATER || b === BIOMES.DEEP_WATER) return true;
      }
    return false;
  }
  function tooClose(x, y, d) {
    return pois.some(p => Math.abs(p.x - x) + Math.abs(p.y - y) < d);
  }

  for (const [key, maxCount] of Object.entries(counts)) {
    const def = poiDefs[key];
    if (!def) continue;
    for (let att = 0; att < 400 && pois.filter(p => p.type === def).length < maxCount; att++) {
      const x = Math.floor(rng() * w),
        y = Math.floor(rng() * h);
      const b = biomeAt(x, y);
      if (!b) continue;
      const matchBiome = def.minBiome.includes(b.id);
      if (!matchBiome) continue;
      if (def.nearWater && !nearWater(x, y, 4)) continue;
      if (tooClose(x, y, key === 'CAPITAL' || key === 'CITY' ? 14 : key === 'TOWN' ? 10 : 7))
        continue;
      pois.push({ x, y, type: def, key, seed: seed * 31 + x * 997 + y * 1301 });
    }
  }
  return pois;
}

function generatePaths(pois, filterKeys) {
  const nodes = pois.filter(p => filterKeys.includes(p.key));
  const paths = [];
  const connected = new Set();
  for (const s of nodes) {
    const others = nodes
      .filter(o => o !== s)
      .map(o => ({ ...o, dist: Math.hypot(o.x - s.x, o.y - s.y) }))
      .sort((a, b) => a.dist - b.dist)
      .slice(0, 2);
    for (const t of others) {
      const id = [s.x, s.y, t.x, t.y].sort().join(',');
      if (connected.has(id)) continue;
      connected.add(id);
      const pts = [];
      let cx = s.x,
        cy = s.y;
      while (Math.abs(cx - t.x) > 1 || Math.abs(cy - t.y) > 1) {
        pts.push({ x: Math.round(cx), y: Math.round(cy) });
        const dx = t.x - cx,
          dy = t.y - cy,
          len = Math.sqrt(dx * dx + dy * dy);
        cx += (dx / len) * 1.2;
        cy += (dy / len) * 1.2;
      }
      paths.push({ from: s, to: t, points: pts });
    }
  }
  return paths;
}

// ── Settlement Interior Generation ────────────────────────────
function generateSettlement(w, h, seed, parentBiome) {
  const rng = mulberry32(seed);
  const ground = Array.from({ length: h }, () => Array(w).fill(SETTLEMENT_GROUND.GRASS));
  const buildings = [];

  // Base ground from parent biome
  const baseGround =
    parentBiome === BIOMES.DESERT || parentBiome === BIOMES.SAND
      ? SETTLEMENT_GROUND.DIRT
      : SETTLEMENT_GROUND.GRASS;
  for (let y = 0; y < h; y++) for (let x = 0; x < w; x++) ground[y][x] = baseGround;

  // Noise for variation
  const gNoise = createNoise(seed + 100);
  for (let y = 0; y < h; y++)
    for (let x = 0; x < w; x++) {
      const n = (fbm(gNoise, x / 8, y / 8, 3) + 1) / 2;
      if (n > 0.7) ground[y][x] = SETTLEMENT_GROUND.STONE;
    }

  // River through settlement?
  const hasRiver = rng() > 0.5;
  if (hasRiver) {
    let rx = Math.floor(w * 0.3 + rng() * w * 0.4);
    for (let y = 0; y < h; y++) {
      rx += Math.floor(rng() * 3) - 1;
      rx = Math.max(2, Math.min(w - 3, rx));
      for (let dx = -1; dx <= 1; dx++) ground[y][rx + dx] = SETTLEMENT_GROUND.WATER;
    }
    // Bridge
    const bridgeY = Math.floor(h * 0.3 + rng() * h * 0.4);
    for (let dx = -1; dx <= 1; dx++) ground[bridgeY][rx + dx] = SETTLEMENT_GROUND.BRIDGE;
    if (bridgeY + 1 < h)
      for (let dx = -1; dx <= 1; dx++) ground[bridgeY + 1][rx + dx] = SETTLEMENT_GROUND.BRIDGE;
  }

  // Roads — main cross
  const mainRoadY = Math.floor(h * 0.4 + rng() * h * 0.2);
  const mainRoadX = Math.floor(w * 0.4 + rng() * w * 0.2);
  for (let x = 2; x < w - 2; x++) {
    if (ground[mainRoadY][x] !== SETTLEMENT_GROUND.WATER)
      ground[mainRoadY][x] = SETTLEMENT_GROUND.ROAD;
    if (mainRoadY + 1 < h && ground[mainRoadY + 1][x] !== SETTLEMENT_GROUND.WATER)
      ground[mainRoadY + 1][x] = SETTLEMENT_GROUND.ROAD;
  }
  for (let y = 2; y < h - 2; y++) {
    if (ground[y][mainRoadX] !== SETTLEMENT_GROUND.WATER)
      ground[y][mainRoadX] = SETTLEMENT_GROUND.ROAD;
    if (mainRoadX + 1 < w && ground[y][mainRoadX + 1] !== SETTLEMENT_GROUND.WATER)
      ground[y][mainRoadX + 1] = SETTLEMENT_GROUND.ROAD;
  }

  // Side streets
  for (let i = 0; i < 3; i++) {
    const sy = Math.floor(rng() * h);
    const sx1 = Math.floor(rng() * w * 0.3);
    const sx2 = Math.floor(w * 0.7 + rng() * w * 0.3);
    for (let x = sx1; x < Math.min(sx2, w); x++) {
      if (sy < h && ground[sy][x] === baseGround) ground[sy][x] = SETTLEMENT_GROUND.ROAD;
    }
  }

  // Place buildings
  function canPlace(bx, by, bw, bh) {
    if (bx < 1 || by < 1 || bx + bw >= w - 1 || by + bh >= h - 1) return false;
    for (let y = by - 1; y <= by + bh; y++)
      for (let x = bx - 1; x <= bx + bw; x++) {
        if (y < 0 || y >= h || x < 0 || x >= w) return false;
        if (ground[y][x] === SETTLEMENT_GROUND.WATER) return false;
        if (buildings.some(b => x >= b.x && x < b.x + b.w && y >= b.y && y < b.y + b.h))
          return false;
      }
    return true;
  }

  function nearRoad(bx, by, bw, bh) {
    for (let y = by - 1; y <= by + bh; y++)
      for (let x = bx - 1; x <= bx + bw; x++) {
        if (y >= 0 && y < h && x >= 0 && x < w && ground[y][x] === SETTLEMENT_GROUND.ROAD)
          return true;
      }
    return false;
  }

  // Central buildings first
  const centralTypes = [BUILDING_TYPES.HALL, BUILDING_TYPES.TEMPLE, BUILDING_TYPES.MARKET];
  for (const bt of centralTypes) {
    for (let att = 0; att < 80; att++) {
      const bw = 3 + Math.floor(rng() * 2),
        bh = 3 + Math.floor(rng() * 2);
      const bx = Math.floor(mainRoadX - 4 + rng() * 8),
        by = Math.floor(mainRoadY - 4 + rng() * 8);
      if (canPlace(bx, by, bw, bh) && nearRoad(bx, by, bw, bh)) {
        buildings.push({ x: bx, y: by, w: bw, h: bh, type: bt });
        break;
      }
    }
  }

  // Tavern, Smithy, Shop
  for (const bt of [
    BUILDING_TYPES.TAVERN,
    BUILDING_TYPES.SMITHY,
    BUILDING_TYPES.SHOP,
    BUILDING_TYPES.SHOP,
  ]) {
    for (let att = 0; att < 100; att++) {
      const bw = 2 + Math.floor(rng() * 2),
        bh = 2 + Math.floor(rng() * 2);
      const bx = Math.floor(rng() * (w - bw - 2)) + 1,
        by = Math.floor(rng() * (h - bh - 2)) + 1;
      if (canPlace(bx, by, bw, bh) && nearRoad(bx, by, bw, bh)) {
        buildings.push({ x: bx, y: by, w: bw, h: bh, type: bt });
        break;
      }
    }
  }

  // Houses
  for (let i = 0; i < 18; i++) {
    for (let att = 0; att < 120; att++) {
      const bw = 2 + Math.floor(rng() * 2),
        bh = 2 + Math.floor(rng());
      const bx = Math.floor(rng() * (w - bw - 2)) + 1,
        by = Math.floor(rng() * (h - bh - 2)) + 1;
      if (canPlace(bx, by, bw, bh)) {
        buildings.push({ x: bx, y: by, w: bw, h: bh, type: BUILDING_TYPES.HOUSE });
        break;
      }
    }
  }

  // Wells & gardens in gaps
  for (const bt of [BUILDING_TYPES.WELL, BUILDING_TYPES.GARDEN, BUILDING_TYPES.GARDEN]) {
    for (let att = 0; att < 80; att++) {
      const bx = Math.floor(rng() * (w - 2)) + 1,
        by = Math.floor(rng() * (h - 2)) + 1;
      if (canPlace(bx, by, 1, 1)) {
        buildings.push({ x: bx, y: by, w: 1, h: 1, type: bt });
        break;
      }
    }
  }

  // Walls around perimeter (partial)
  if (rng() > 0.3) {
    for (let x = 1; x < w - 1; x++) {
      if (rng() > 0.15 && ground[1][x] !== SETTLEMENT_GROUND.WATER)
        buildings.push({ x, y: 0, w: 1, h: 1, type: BUILDING_TYPES.WALL });
      if (rng() > 0.15 && ground[h - 2][x] !== SETTLEMENT_GROUND.WATER)
        buildings.push({ x, y: h - 1, w: 1, h: 1, type: BUILDING_TYPES.WALL });
    }
    for (let y = 1; y < h - 1; y++) {
      if (rng() > 0.15) buildings.push({ x: 0, y, w: 1, h: 1, type: BUILDING_TYPES.WALL });
      if (rng() > 0.15) buildings.push({ x: w - 1, y, w: 1, h: 1, type: BUILDING_TYPES.WALL });
    }
    // Gates on roads
    const gates = buildings.filter(
      b =>
        (b.type === BUILDING_TYPES.WALL &&
          (b.y === 0 || b.y === h - 1) &&
          (Math.abs(b.x - mainRoadX) <= 1 || Math.abs(b.x - mainRoadX - 1) <= 0)) ||
        ((b.x === 0 || b.x === w - 1) &&
          (Math.abs(b.y - mainRoadY) <= 1 || Math.abs(b.y - mainRoadY - 1) <= 0))
    );
    gates.forEach(g => (g.type = BUILDING_TYPES.GATE));
  }

  return { ground, buildings };
}

// ── Canvas Renderer ───────────────────────────────────────────
function renderWorld(ctx, data, zoom, ts, showPOIs, showPaths, showGrid, hoveredTile) {
  const { tiles, elevs, pois, paths } = data;
  const h = tiles.length,
    w = tiles[0].length;

  for (let y = 0; y < h; y++)
    for (let x = 0; x < w; x++) {
      const b = tiles[y][x],
        e = elevs[y][x];
      let [r, g, bl] = hexToRgb(b.color);
      const shade = (e - 0.5) * 30;
      r = clamp(r + shade);
      g = clamp(g + shade);
      bl = clamp(bl + shade);
      ctx.fillStyle = `rgb(${r | 0},${g | 0},${bl | 0})`;
      ctx.fillRect(x * ts, y * ts, ts, ts);

      // Biome texture
      if (ts >= 5) {
        if ((b === BIOMES.FOREST || b === BIOMES.DENSE_FOREST) && (x + y * 3) % 4 === 0) {
          ctx.fillStyle = b === BIOMES.DENSE_FOREST ? '#1e4a1a' : '#2d6625';
          ctx.fillRect(x * ts + 1, y * ts, ts - 2, ts - 2);
        }
        if ((b === BIOMES.WATER || b === BIOMES.DEEP_WATER) && (x + y) % 5 === 0) {
          ctx.fillStyle = 'rgba(255,255,255,0.12)';
          ctx.fillRect(x * ts + 1, y * ts + Math.floor(ts / 2), ts - 2, 1);
        }
        if (b === BIOMES.SNOW && (x * 7 + y * 13) % 9 === 0) {
          ctx.fillStyle = 'rgba(255,255,255,0.4)';
          ctx.fillRect(x * ts + 2, y * ts + 1, 1, 1);
        }
        if ((b === BIOMES.SAND || b === BIOMES.DESERT) && (x * 3 + y * 7) % 6 === 0) {
          ctx.fillStyle = 'rgba(180,150,80,0.3)';
          ctx.fillRect(x * ts + 2, y * ts + 2, 1, 1);
        }
        if (b === BIOMES.MOUNTAIN && (x * 5 + y * 11) % 7 === 0) {
          ctx.fillStyle = 'rgba(0,0,0,0.12)';
          ctx.fillRect(x * ts + 1, y * ts + 1, ts - 2, 1);
        }
      }
    }

  if (showGrid) {
    ctx.strokeStyle = 'rgba(0,0,0,0.08)';
    ctx.lineWidth = 0.5;
    for (let x = 0; x <= w; x++) {
      ctx.beginPath();
      ctx.moveTo(x * ts, 0);
      ctx.lineTo(x * ts, h * ts);
      ctx.stroke();
    }
    for (let y = 0; y <= h; y++) {
      ctx.beginPath();
      ctx.moveTo(0, y * ts);
      ctx.lineTo(w * ts, y * ts);
      ctx.stroke();
    }
  }

  if (showPaths)
    for (const path of paths) {
      ctx.strokeStyle = 'rgba(139,119,90,0.55)';
      ctx.lineWidth = ts >= 6 ? 2 : 1;
      ctx.setLineDash(ts >= 6 ? [3, 3] : [2, 2]);
      ctx.beginPath();
      path.points.forEach((p, i) => {
        const px = p.x * ts + ts / 2,
          py = p.y * ts + ts / 2;
        i === 0 ? ctx.moveTo(px, py) : ctx.lineTo(px, py);
      });
      ctx.stroke();
      ctx.setLineDash([]);
    }

  if (showPOIs)
    for (const poi of pois) {
      const px = poi.x * ts,
        py = poi.y * ts,
        size = ts * 2.5;
      ctx.fillStyle = 'rgba(0,0,0,0.4)';
      ctx.beginPath();
      ctx.arc(px + ts / 2, py + ts / 2, size * 0.5, 0, Math.PI * 2);
      ctx.fill();
      ctx.fillStyle = poi.type.color;
      ctx.beginPath();
      ctx.arc(px + ts / 2, py + ts / 2, size * 0.38, 0, Math.PI * 2);
      ctx.fill();
      ctx.fillStyle = '#fff';
      ctx.font = `bold ${Math.max(8, ts)}px monospace`;
      ctx.textAlign = 'center';
      ctx.textBaseline = 'middle';
      ctx.fillText(poi.type.emoji, px + ts / 2, py + ts / 2 + 0.5);
    }

  if (hoveredTile) {
    ctx.strokeStyle = 'rgba(255,255,200,0.8)';
    ctx.lineWidth = 2;
    ctx.strokeRect(hoveredTile.x * ts, hoveredTile.y * ts, ts, ts);
  }
}

function renderSettlement(ctx, data, ts, showGrid, hoveredTile) {
  const { ground, buildings } = data;
  const h = ground.length,
    w = ground[0].length;

  for (let y = 0; y < h; y++)
    for (let x = 0; x < w; x++) {
      ctx.fillStyle = ground[y][x].color;
      ctx.fillRect(x * ts, y * ts, ts, ts);
      // Road texture
      if (ground[y][x] === SETTLEMENT_GROUND.ROAD && ts >= 8 && (x + y) % 3 === 0) {
        ctx.fillStyle = 'rgba(0,0,0,0.08)';
        ctx.fillRect(x * ts + 2, y * ts + 2, ts - 4, 1);
      }
      if (ground[y][x] === SETTLEMENT_GROUND.GRASS && ts >= 8 && (x * 7 + y * 3) % 5 === 0) {
        ctx.fillStyle = 'rgba(80,140,60,0.3)';
        ctx.fillRect(x * ts + 3, y * ts + 4, 2, 2);
      }
      if (ground[y][x] === SETTLEMENT_GROUND.WATER && ts >= 8 && (x + y) % 4 === 0) {
        ctx.fillStyle = 'rgba(255,255,255,0.15)';
        ctx.fillRect(x * ts + 2, y * ts + ts / 2, ts - 4, 1);
      }
    }

  for (const b of buildings) {
    // Shadow
    ctx.fillStyle = 'rgba(0,0,0,0.2)';
    ctx.fillRect(b.x * ts + 2, b.y * ts + 2, b.w * ts, b.h * ts);
    // Building body
    ctx.fillStyle = b.type.color;
    ctx.fillRect(b.x * ts, b.y * ts, b.w * ts, b.h * ts);
    // Roof / accent
    if (
      b.type !== BUILDING_TYPES.WALL &&
      b.type !== BUILDING_TYPES.GATE &&
      b.type !== BUILDING_TYPES.WELL &&
      b.type !== BUILDING_TYPES.GARDEN
    ) {
      const [r, g, bl] = hexToRgb(b.type.color);
      ctx.fillStyle = `rgb(${clamp(r - 30) | 0},${clamp(g - 30) | 0},${clamp(bl - 30) | 0})`;
      ctx.fillRect(b.x * ts, b.y * ts, b.w * ts, Math.max(2, Math.floor(ts * 0.3)));
    }
    // Outline
    ctx.strokeStyle = 'rgba(0,0,0,0.3)';
    ctx.lineWidth = 1;
    ctx.strokeRect(b.x * ts, b.y * ts, b.w * ts, b.h * ts);
    // Label
    if (ts >= 8 && b.w >= 2 && b.h >= 2) {
      ctx.fillStyle = '#fff';
      ctx.font = `bold ${Math.max(9, ts - 2)}px monospace`;
      ctx.textAlign = 'center';
      ctx.textBaseline = 'middle';
      ctx.fillText(b.type.emoji, b.x * ts + (b.w * ts) / 2, b.y * ts + (b.h * ts) / 2 + 1);
    }
  }

  if (showGrid) {
    ctx.strokeStyle = 'rgba(0,0,0,0.06)';
    ctx.lineWidth = 0.5;
    for (let x = 0; x <= w; x++) {
      ctx.beginPath();
      ctx.moveTo(x * ts, 0);
      ctx.lineTo(x * ts, h * ts);
      ctx.stroke();
    }
    for (let y = 0; y <= h; y++) {
      ctx.beginPath();
      ctx.moveTo(0, y * ts);
      ctx.lineTo(w * ts, y * ts);
      ctx.stroke();
    }
  }

  if (
    hoveredTile &&
    hoveredTile.x >= 0 &&
    hoveredTile.x < w &&
    hoveredTile.y >= 0 &&
    hoveredTile.y < h
  ) {
    ctx.strokeStyle = 'rgba(255,255,200,0.8)';
    ctx.lineWidth = 2;
    ctx.strokeRect(hoveredTile.x * ts, hoveredTile.y * ts, ts, ts);
  }
}

function hexToRgb(hex) {
  return [
    parseInt(hex.slice(1, 3), 16),
    parseInt(hex.slice(3, 5), 16),
    parseInt(hex.slice(5, 7), 16),
  ];
}
function clamp(v) {
  return Math.max(0, Math.min(255, v));
}

// ── Main Component ────────────────────────────────────────────
export default function CozyMapGenerator() {
  const [worldSeed, setWorldSeed] = useState(42);
  const [zoomLevel, setZoomLevel] = useState(0); // 0=world, 1=region, 2=settlement
  const [zoomStack, setZoomStack] = useState([]); // [{level,seed,name,parentBiome}]
  const [showPOIs, setShowPOIs] = useState(true);
  const [showPaths, setShowPaths] = useState(true);
  const [showGrid, setShowGrid] = useState(false);
  const [hoveredTile, setHoveredTile] = useState(null);
  const [hoveredPOI, setHoveredPOI] = useState(null);
  const [hoveredBuilding, setHoveredBuilding] = useState(null);
  const canvasRef = useRef(null);

  const currentZoom =
    zoomLevel === 0 ? ZOOM.WORLD : zoomLevel === 1 ? ZOOM.REGION : ZOOM.SETTLEMENT;
  const currentSeed = zoomStack.length > 0 ? zoomStack[zoomStack.length - 1].seed : worldSeed;
  const currentName =
    zoomStack.length > 0 ? zoomStack[zoomStack.length - 1].name : genName(worldSeed, 'world');
  const parentBiome = zoomStack.length > 0 ? zoomStack[zoomStack.length - 1].parentBiome : null;

  // Generate map data
  const mapData = useMemo(() => {
    const z = currentZoom;
    if (zoomLevel === 2) {
      // Settlement view
      const sett = generateSettlement(z.gridW, z.gridH, currentSeed, parentBiome || BIOMES.PLAINS);
      return { type: 'settlement', ...sett, w: z.gridW, h: z.gridH };
    }
    // World or Region view
    const { tiles, elevs, moists } = generateTerrain(
      z.gridW,
      z.gridH,
      currentSeed,
      z.scale,
      zoomLevel === 0
    );
    const poiDefs = zoomLevel === 0 ? POI_WORLD : POI_REGION;
    const counts =
      zoomLevel === 0
        ? { CAPITAL: 1, CITY: 4, FORTRESS: 3, PORT: 3, DUNGEON: 3 }
        : {
            TOWN: 2,
            VILLAGE: 5,
            MINE: 3,
            CAVE: 2,
            RUINS: 2,
            SHRINE: 2,
            FARM: 4,
            CAMP: 2,
            TOWER: 2,
            DOCK: 2,
          };
    const pois = placePOIsForLevel(tiles, z.gridW, z.gridH, currentSeed, poiDefs, counts);
    const pathKeys = zoomLevel === 0 ? ['CAPITAL', 'CITY', 'PORT'] : ['TOWN', 'VILLAGE', 'DOCK'];
    const paths = generatePaths(pois, pathKeys);
    return { type: 'terrain', tiles, elevs, moists, pois, paths, w: z.gridW, h: z.gridH };
  }, [currentSeed, zoomLevel, currentZoom, parentBiome]);

  // Render
  useEffect(() => {
    if (!canvasRef.current || !mapData) return;
    const canvas = canvasRef.current;
    const ctx = canvas.getContext('2d');
    const ts = currentZoom.tileSize;
    canvas.width = mapData.w * ts;
    canvas.height = mapData.h * ts;
    if (mapData.type === 'settlement') renderSettlement(ctx, mapData, ts, showGrid, hoveredTile);
    else renderWorld(ctx, mapData, currentZoom, ts, showPOIs, showPaths, showGrid, hoveredTile);
  }, [mapData, showPOIs, showPaths, showGrid, hoveredTile, currentZoom]);

  // Mouse handling
  const handleMove = useCallback(
    e => {
      if (!mapData || !canvasRef.current) return;
      const canvas = canvasRef.current;
      const rect = canvas.getBoundingClientRect();
      const ts = currentZoom.tileSize;
      const sx = canvas.width / rect.width,
        sy = canvas.height / rect.height;
      const mx = Math.floor(((e.clientX - rect.left) * sx) / ts),
        my = Math.floor(((e.clientY - rect.top) * sy) / ts);
      if (mx >= 0 && mx < mapData.w && my >= 0 && my < mapData.h) {
        setHoveredTile({ x: mx, y: my });
        if (mapData.type === 'terrain') {
          setHoveredPOI(
            mapData.pois.find(p => Math.abs(p.x - mx) <= 1 && Math.abs(p.y - my) <= 1) || null
          );
          setHoveredBuilding(null);
        } else {
          setHoveredPOI(null);
          setHoveredBuilding(
            mapData.buildings.find(
              b => mx >= b.x && mx < b.x + b.w && my >= b.y && my < b.y + b.h
            ) || null
          );
        }
      } else {
        setHoveredTile(null);
        setHoveredPOI(null);
        setHoveredBuilding(null);
      }
    },
    [mapData, currentZoom]
  );

  // Click to zoom
  const handleClick = useCallback(
    e => {
      if (!mapData || !canvasRef.current) return;
      const canvas = canvasRef.current;
      const rect = canvas.getBoundingClientRect();
      const ts = currentZoom.tileSize;
      const sx = canvas.width / rect.width,
        sy = canvas.height / rect.height;
      const mx = Math.floor(((e.clientX - rect.left) * sx) / ts),
        my = Math.floor(((e.clientY - rect.top) * sy) / ts);

      if (zoomLevel === 0 && mapData.type === 'terrain') {
        // Click on world → zoom to region
        const poi = mapData.pois.find(p => Math.abs(p.x - mx) <= 2 && Math.abs(p.y - my) <= 2);
        const derivedSeed = worldSeed * 31 + mx * 997 + my * 1301;
        const biome = mapData.tiles[my]?.[mx] || BIOMES.PLAINS;
        const name = poi
          ? `${genName(derivedSeed, 'region')} (${poi.type.name})`
          : genName(derivedSeed, 'region');
        setZoomStack([
          { level: 1, seed: derivedSeed, name, parentBiome: biome, worldX: mx, worldY: my },
        ]);
        setZoomLevel(1);
      } else if (zoomLevel === 1 && mapData.type === 'terrain') {
        // Click on region → zoom to settlement (if clicking a POI)
        const poi = mapData.pois.find(p => Math.abs(p.x - mx) <= 2 && Math.abs(p.y - my) <= 2);
        if (poi && (poi.key === 'TOWN' || poi.key === 'VILLAGE' || poi.key === 'DOCK')) {
          const derivedSeed = poi.seed;
          const biome = mapData.tiles[my]?.[mx] || BIOMES.PLAINS;
          const name = genName(derivedSeed, 'settlement');
          setZoomStack(prev => [
            ...prev,
            { level: 2, seed: derivedSeed, name, parentBiome: biome, worldX: mx, worldY: my },
          ]);
          setZoomLevel(2);
        }
      }
    },
    [mapData, zoomLevel, worldSeed, currentZoom]
  );

  const zoomOut = () => {
    if (zoomStack.length > 1) {
      setZoomStack(prev => prev.slice(0, -1));
      setZoomLevel(zoomStack[zoomStack.length - 2].level);
    } else {
      setZoomStack([]);
      setZoomLevel(0);
    }
    setHoveredTile(null);
    setHoveredPOI(null);
    setHoveredBuilding(null);
  };

  // Biome / building stats
  const stats = useMemo(() => {
    if (!mapData) return [];
    if (mapData.type === 'terrain') {
      const c = {};
      for (let y = 0; y < mapData.h; y++)
        for (let x = 0; x < mapData.w; x++) {
          const n = mapData.tiles[y][x].name;
          c[n] = (c[n] || 0) + 1;
        }
      return Object.entries(c).sort((a, b) => b[1] - a[1]);
    }
    const c = {};
    mapData.buildings.forEach(b => {
      const n = b.type.name;
      c[n] = (c[n] || 0) + 1;
    });
    return Object.entries(c).sort((a, b) => b[1] - a[1]);
  }, [mapData]);

  const totalTiles = mapData ? mapData.w * mapData.h : 1;

  // Info text
  const infoText =
    hoveredTile && mapData
      ? mapData.type === 'terrain'
        ? `[${hoveredTile.x},${hoveredTile.y}] ${mapData.tiles[hoveredTile.y]?.[hoveredTile.x]?.name || ''} · E:${mapData.elevs[hoveredTile.y]?.[hoveredTile.x]?.toFixed(2) || ''} M:${mapData.moists[hoveredTile.y]?.[hoveredTile.x]?.toFixed(2) || ''}`
        : `[${hoveredTile.x},${hoveredTile.y}] ${mapData.ground[hoveredTile.y]?.[hoveredTile.x]?.name || ''}`
      : zoomLevel < 2
        ? 'Click a location to zoom in'
        : 'Settlement view';

  return (
    <div
      style={{
        minHeight: '100vh',
        background: 'linear-gradient(145deg,#1a1a2e 0%,#16213e 50%,#1a1a2e 100%)',
        color: '#d4cfc4',
        fontFamily: "'Courier New',monospace",
        padding: 16,
        boxSizing: 'border-box',
      }}
    >
      {/* Header */}
      <div style={{ textAlign: 'center', marginBottom: 12 }}>
        <h1
          style={{
            fontSize: 24,
            color: '#e8d5a3',
            letterSpacing: 4,
            margin: 0,
            textShadow: '0 0 20px rgba(232,213,163,0.3)',
          }}
        >
          ⚔ REALM CARTOGRAPHER ⚔
        </h1>
        <div style={{ fontSize: 10, color: '#6a6a7a', letterSpacing: 2, marginTop: 2 }}>
          PROCEDURAL MAP GENERATOR · CLICK TO EXPLORE
        </div>
      </div>

      <div
        style={{
          display: 'flex',
          gap: 14,
          maxWidth: 1150,
          margin: '0 auto',
          flexWrap: 'wrap',
          justifyContent: 'center',
        }}
      >
        {/* Sidebar */}
        <div
          style={{ width: 210, flexShrink: 0, display: 'flex', flexDirection: 'column', gap: 8 }}
        >
          {/* Breadcrumb */}
          <div style={panelStyle}>
            <label style={labelStyle}>NAVIGATION</label>
            <div style={{ display: 'flex', flexDirection: 'column', gap: 4 }}>
              <button
                onClick={() => {
                  setZoomStack([]);
                  setZoomLevel(0);
                  setHoveredTile(null);
                }}
                style={{ ...breadStyle, color: zoomLevel === 0 ? '#e8d5a3' : '#7a7a8a' }}
              >
                ◉ World
              </button>
              {zoomStack
                .filter(z => z.level === 1)
                .map((z, i) => (
                  <button
                    key={i}
                    onClick={() => {
                      setZoomStack(prev => prev.slice(0, 1));
                      setZoomLevel(1);
                    }}
                    style={{
                      ...breadStyle,
                      paddingLeft: 16,
                      color: zoomLevel === 1 ? '#e8d5a3' : '#7a7a8a',
                    }}
                  >
                    ├ {z.name.split('(')[0].trim().slice(0, 18)}
                  </button>
                ))}
              {zoomStack
                .filter(z => z.level === 2)
                .map((z, i) => (
                  <button key={i} style={{ ...breadStyle, paddingLeft: 28, color: '#e8d5a3' }}>
                    └ {z.name}
                  </button>
                ))}
            </div>
            {zoomLevel > 0 && (
              <button
                onClick={zoomOut}
                style={{ ...btnStyle, marginTop: 6, width: '100%', fontSize: 11 }}
              >
                ← Zoom Out
              </button>
            )}
          </div>

          {/* Seed */}
          <div style={panelStyle}>
            <label style={labelStyle}>WORLD SEED</label>
            <div style={{ display: 'flex', gap: 4 }}>
              <input
                type="number"
                value={worldSeed}
                onChange={e => {
                  setWorldSeed(Number(e.target.value));
                  setZoomStack([]);
                  setZoomLevel(0);
                }}
                style={inputStyle}
              />
              <button
                onClick={() => {
                  setWorldSeed(Math.floor(Math.random() * 99999));
                  setZoomStack([]);
                  setZoomLevel(0);
                }}
                style={btnStyle}
                title="Random"
              >
                🎲
              </button>
            </div>
          </div>

          {/* Toggles */}
          <div style={panelStyle}>
            <label style={labelStyle}>LAYERS</label>
            {[
              ['Locations', showPOIs, setShowPOIs],
              ['Trade Routes', showPaths, setShowPaths],
              ['Grid', showGrid, setShowGrid],
            ].map(([l, v, s]) => (
              <label
                key={l}
                style={{
                  display: 'flex',
                  alignItems: 'center',
                  gap: 8,
                  fontSize: 11,
                  cursor: 'pointer',
                  padding: '2px 0',
                }}
              >
                <input
                  type="checkbox"
                  checked={v}
                  onChange={e => s(e.target.checked)}
                  style={{ accentColor: '#e8d5a3' }}
                />
                {l}
              </label>
            ))}
          </div>

          <button
            onClick={() => {
              setWorldSeed(Math.floor(Math.random() * 99999));
              setZoomStack([]);
              setZoomLevel(0);
            }}
            style={{
              ...btnStyle,
              padding: '10px 0',
              fontSize: 13,
              letterSpacing: 2,
              background: 'linear-gradient(180deg,#4a3a2a,#3a2a1a)',
              border: '2px solid #6a5a3a',
            }}
          >
            ✦ NEW REALM ✦
          </button>

          {/* Legend */}
          <div style={{ ...panelStyle, maxHeight: 260, overflowY: 'auto' }}>
            <label style={labelStyle}>
              {mapData?.type === 'settlement' ? 'BUILDINGS' : 'BIOMES'}
            </label>
            {stats.map(([name, count]) => {
              let color = '#555';
              if (mapData?.type === 'terrain') {
                const b = Object.values(BIOMES).find(b => b.name === name);
                color = b?.color || '#555';
              } else {
                const b = Object.values(BUILDING_TYPES).find(b => b.name === name);
                color = b?.color || '#555';
              }
              return (
                <div
                  key={name}
                  style={{
                    display: 'flex',
                    alignItems: 'center',
                    gap: 6,
                    fontSize: 10,
                    padding: '2px 0',
                  }}
                >
                  <div
                    style={{
                      width: 12,
                      height: 12,
                      borderRadius: 2,
                      background: color,
                      border: '1px solid rgba(255,255,255,0.15)',
                      flexShrink: 0,
                    }}
                  />
                  <span style={{ flex: 1 }}>{name}</span>
                  <span style={{ color: '#6a6a7a' }}>
                    {mapData?.type === 'terrain'
                      ? `${((count / totalTiles) * 100).toFixed(0)}%`
                      : `×${count}`}
                  </span>
                </div>
              );
            })}
          </div>

          {/* POI list */}
          {showPOIs && mapData?.type === 'terrain' && mapData.pois.length > 0 && (
            <div style={panelStyle}>
              <label style={labelStyle}>LOCATIONS ({mapData.pois.length})</label>
              {(() => {
                const types = {};
                mapData.pois.forEach(p => {
                  types[p.type.name] = (types[p.type.name] || 0) + 1;
                });
                return Object.entries(types).map(([name, count]) => {
                  const t = mapData.pois.find(p => p.type.name === name)?.type;
                  return (
                    <div
                      key={name}
                      style={{
                        display: 'flex',
                        alignItems: 'center',
                        gap: 6,
                        fontSize: 10,
                        padding: '2px 0',
                      }}
                    >
                      <span style={{ width: 14, textAlign: 'center', color: t?.color || '#fff' }}>
                        {t?.emoji}
                      </span>
                      <span style={{ flex: 1 }}>{name}</span>
                      <span style={{ color: '#6a6a7a' }}>×{count}</span>
                    </div>
                  );
                });
              })()}
              {zoomLevel === 1 && (
                <div style={{ fontSize: 9, color: '#5a5a6a', marginTop: 4 }}>
                  Click a town/village to enter
                </div>
              )}
            </div>
          )}

          {/* Zoom hint */}
          <div
            style={{ ...panelStyle, background: 'rgba(232,213,163,0.06)', borderColor: '#4a4a3a' }}
          >
            <div style={{ fontSize: 10, color: '#8a8a6a', lineHeight: 1.5 }}>
              {zoomLevel === 0 && '🖱 Click anywhere to zoom into that region'}
              {zoomLevel === 1 && '🖱 Click a Town or Village to enter the settlement'}
              {zoomLevel === 2 && '🏘 Exploring settlement interior'}
            </div>
          </div>
        </div>

        {/* Map */}
        <div style={{ flex: 1, minWidth: 0 }}>
          {/* Title bar */}
          <div
            style={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              padding: '6px 4px',
              marginBottom: 4,
            }}
          >
            <div style={{ fontSize: 10, color: '#6a6a7a', letterSpacing: 1 }}>
              {['◈ WORLD', '◈ REGION', '◈ SETTLEMENT'][zoomLevel]}
            </div>
            <div
              style={{
                fontSize: 17,
                color: '#e8d5a3',
                letterSpacing: 3,
                textShadow: '0 0 10px rgba(232,213,163,0.2)',
                textAlign: 'center',
              }}
            >
              {currentName}
            </div>
            <div style={{ fontSize: 10, color: '#6a6a7a' }}>Seed: {currentSeed}</div>
          </div>

          <div
            style={{
              background: 'rgba(0,0,0,0.3)',
              border: '2px solid #3a3a4a',
              borderRadius: 4,
              padding: 4,
              overflow: 'auto',
              maxWidth: '100%',
              position: 'relative',
            }}
          >
            <canvas
              ref={canvasRef}
              onMouseMove={handleMove}
              onMouseLeave={() => {
                setHoveredTile(null);
                setHoveredPOI(null);
                setHoveredBuilding(null);
              }}
              onClick={handleClick}
              style={{
                display: 'block',
                imageRendering: 'pixelated',
                cursor: zoomLevel < 2 ? 'pointer' : 'crosshair',
                width: mapData ? mapData.w * currentZoom.tileSize : 600,
                maxWidth: '100%',
              }}
            />
          </div>

          {/* Status bar */}
          <div
            style={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              padding: '6px 4px',
              fontSize: 11,
              color: '#7a7a8a',
              borderTop: '1px solid #2a2a3a',
              marginTop: 4,
              flexWrap: 'wrap',
              gap: 6,
            }}
          >
            <span>{infoText}</span>
            <span>
              {hoveredPOI && (
                <span style={{ color: hoveredPOI.type.color }}>
                  {hoveredPOI.type.emoji} {hoveredPOI.type.name} ·{' '}
                </span>
              )}
              {hoveredBuilding && (
                <span style={{ color: hoveredBuilding.type.color }}>
                  {hoveredBuilding.type.emoji} {hoveredBuilding.type.name} ·{' '}
                </span>
              )}
              {mapData?.w}×{mapData?.h}
            </span>
          </div>
        </div>
      </div>

      {/* Footer */}
      <div
        style={{
          maxWidth: 1150,
          margin: '12px auto 0',
          padding: 10,
          background: 'rgba(0,0,0,0.2)',
          border: '1px solid #2a2a3a',
          borderRadius: 4,
          fontSize: 10,
          color: '#5a5a6a',
          lineHeight: 1.6,
        }}
      >
        <strong style={{ color: '#7a7a8a' }}>Zoom Pipeline:</strong> World (FBM island noise → biome
        grid → capitals/cities/ports) → Region (derived seed from world coords → detailed terrain →
        towns/mines/shrines) → Settlement (procedural roads/river → building placement with
        adjacency rules → wall perimeter)
      </div>
    </div>
  );
}

const panelStyle = {
  background: 'rgba(0,0,0,0.25)',
  border: '1px solid #2a2a3a',
  borderRadius: 4,
  padding: '8px 10px',
};
const labelStyle = {
  display: 'block',
  fontSize: 10,
  letterSpacing: 2,
  color: '#8a8a9a',
  marginBottom: 5,
  fontWeight: 'bold',
};
const inputStyle = {
  flex: 1,
  background: '#1a1a2a',
  border: '1px solid #3a3a4a',
  color: '#d4cfc4',
  padding: '4px 8px',
  borderRadius: 3,
  fontFamily: "'Courier New',monospace",
  fontSize: 12,
};
const btnStyle = {
  background: 'linear-gradient(180deg,#3a3a4a,#2a2a3a)',
  border: '1px solid #4a4a5a',
  color: '#d4cfc4',
  padding: '4px 8px',
  borderRadius: 3,
  cursor: 'pointer',
  fontFamily: "'Courier New',monospace",
  fontSize: 13,
};
const breadStyle = {
  background: 'none',
  border: 'none',
  color: '#7a7a8a',
  fontFamily: "'Courier New',monospace",
  fontSize: 11,
  textAlign: 'left',
  cursor: 'pointer',
  padding: '2px 0',
};
