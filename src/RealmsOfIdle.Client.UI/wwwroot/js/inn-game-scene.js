// Inn Game Scene Renderer
// Handles rendering of the inn game with entities, tiles, and facilities

let innScenes = {};

export function initInnScene(canvasId, width, height, tileSize, showMinimap, dotNetRef) {
  const canvas = document.getElementById(canvasId);
  if (!canvas) {
    console.error(`Canvas not found: ${canvasId}`);
    return;
  }

  const ctx = canvas.getContext('2d');
  canvas.width = width;
  canvas.height = height;

  innScenes[canvasId] = {
    canvas,
    ctx,
    width,
    height,
    tileSize,
    showMinimap,
    dotNetRef,
    tiles: [],
    customerSprites: [],
    staffSprites: [],
    facilitySprites: [],
    cameraX: 0,
    cameraY: 0,
  };

  // Setup click handler
  canvas.addEventListener('click', e => handleCanvasClick(canvasId, e));

  console.log(`Inn scene initialized: ${canvasId}`);
}

function handleCanvasClick(canvasId, event) {
  const scene = innScenes[canvasId];
  if (!scene) return;

  const rect = scene.canvas.getBoundingClientRect();
  const x = event.clientX - rect.left;
  const y = event.clientY - rect.top;

  // Convert to grid coordinates
  const tileX = Math.floor(x / scene.tileSize) + scene.cameraX;
  const tileY = Math.floor(y / scene.tileSize) + scene.cameraY;

  // Check if an entity was clicked
  const clickedEntity = findEntityAt(scene, tileX, tileY);
  if (clickedEntity) {
    scene.dotNetRef.invokeMethodAsync('OnEntityClicked', clickedEntity.id, clickedEntity.type);
  }
}

function findEntityAt(scene, tileX, tileY) {
  // Check customers
  for (const customer of scene.customerSprites) {
    const pos = parseGridPosition(customer.gridPosition);
    if (pos.x === tileX && pos.y === tileY) {
      return { id: customer.id, type: 'customer' };
    }
  }

  // Check staff
  for (const staff of scene.staffSprites) {
    const pos = parseGridPosition(staff.gridPosition);
    if (pos.x === tileX && pos.y === tileY) {
      return { id: staff.id, type: 'staff' };
    }
  }

  return null;
}

function parseGridPosition(posStr) {
  // Parse position strings like "area_node_id" or "x,y"
  // For now, use a simple hash-based approach
  const hash = hashCode(posStr);
  return {
    x: Math.abs(hash) % 50,
    y: Math.abs(hash >> 8) % 50,
  };
}

function hashCode(str) {
  let hash = 0;
  for (let i = 0; i < str.length; i++) {
    hash = (hash << 5) - hash + str.charCodeAt(i);
    hash |= 0;
  }
  return hash;
}

export function renderInnScene(
  canvasId,
  gameState,
  customerSprites,
  staffSprites,
  facilitySprites,
  animationFrame
) {
  const scene = innScenes[canvasId];
  if (!scene) return;

  scene.customerSprites = customerSprites;
  scene.staffSprites = staffSprites;
  scene.facilitySprites = facilitySprites;

  const ctx = scene.ctx;

  // Clear canvas
  ctx.fillStyle = '#1a1a2e';
  ctx.fillRect(0, 0, scene.width, scene.height);

  // Parse tiles from game state
  const tiles = JSON.parse(gameState).tiles || [];

  // Render tiles
  renderTiles(ctx, scene, tiles);

  // Render entities with interpolation
  renderEntities(ctx, scene, animationFrame);

  // Render facilities overlay
  renderFacilities(ctx, scene, facilitySprites);
}

function renderTiles(ctx, scene, tiles) {
  const ts = scene.tileSize;

  for (const tile of tiles) {
    const screenX = (tile.x - scene.cameraX) * ts;
    const screenY = (tile.y - scene.cameraY) * ts;

    // Skip if off-screen
    if (screenX < -ts || screenX > scene.width || screenY < -ts || screenY > scene.height) {
      continue;
    }

    // Draw tile based on type
    switch (tile.type) {
      case 1: // Floor
      case 2: // Floor2
      case 3: // Floor3
        ctx.fillStyle = '#2d3436';
        ctx.fillRect(screenX, screenY, ts, ts);
        // Add some texture
        ctx.fillStyle = '#353b3d';
        ctx.fillRect(screenX + 2, screenY + 2, ts - 4, ts - 4);
        break;

      case 10: // Wall
      case 11: // Wall2
        ctx.fillStyle = '#636e72';
        ctx.fillRect(screenX, screenY, ts, ts);
        ctx.fillStyle = '#2d3436';
        ctx.fillRect(screenX + 4, screenY + 4, ts - 8, ts - 8);
        break;

      case 20: // Door
        ctx.fillStyle = '#b2bec3';
        ctx.fillRect(screenX, screenY, ts, ts);
        ctx.fillStyle = '#636e72';
        ctx.fillRect(screenX + ts / 2 - 2, screenY + 2, 4, ts - 4);
        break;

      case 30: // Table
        ctx.fillStyle = '#a0522d';
        ctx.fillRect(screenX + 4, screenY + 4, ts - 8, ts - 8);
        break;

      case 31: // Chair
        ctx.fillStyle = '#8b4513';
        ctx.fillRect(screenX + 8, screenY + 8, ts - 16, ts - 16);
        break;

      case 32: // Counter
        ctx.fillStyle = '#654321';
        ctx.fillRect(screenX, screenY + ts / 2, ts, ts / 2);
        break;

      case 33: // Oven
        ctx.fillStyle = '#2c3e50';
        ctx.fillRect(screenX + 2, screenY + 2, ts - 4, ts - 4);
        ctx.fillStyle = '#e74c3c';
        ctx.fillRect(screenX + 6, screenY + 6, ts - 12, ts - 12);
        break;

      case 34: // Bed
        ctx.fillStyle = '#8b4513';
        ctx.fillRect(screenX + 2, screenY + 2, ts - 4, ts - 4);
        ctx.fillStyle = '#ecf0f1';
        ctx.fillRect(screenX + 4, screenY + 4, ts - 8, ts / 2);
        break;

      case 35: // Fireplace
        ctx.fillStyle = '#7f8c8d';
        ctx.fillRect(screenX + 2, screenY + 2, ts - 4, ts - 4);
        // Flickering effect
        const flicker = Math.sin(Date.now() / 200) * 0.3 + 0.7;
        ctx.fillStyle = `rgba(231, 76, 60, ${flicker})`;
        ctx.fillRect(screenX + 6, screenY + 6, ts - 12, ts - 12);
        break;
    }
  }
}

function renderEntities(ctx, scene, animationFrame) {
  const ts = scene.tileSize;

  // Render customers
  for (const customer of scene.customerSprites) {
    const pos = parseGridPosition(customer.gridPosition);
    const screenX = (pos.x - scene.cameraX) * ts;
    const screenY = (pos.y - scene.cameraY) * ts;

    // Skip if off-screen
    if (screenX < -ts || screenX > scene.width || screenY < -ts || screenY > scene.height) {
      continue;
    }

    // Draw customer sprite
    drawEntity(ctx, screenX, screenY, ts, customer.spriteId, customer.state, animationFrame);
  }

  // Render staff
  for (const staff of scene.staffSprites) {
    const pos = parseGridPosition(staff.gridPosition);
    const screenX = (pos.x - scene.cameraX) * ts;
    const screenY = (pos.y - scene.cameraY) * ts;

    // Skip if off-screen
    if (screenX < -ts || screenX > scene.width || screenY < -ts || screenY > scene.height) {
      continue;
    }

    // Draw staff sprite
    drawEntity(ctx, screenX, screenY, ts, staff.spriteId, staff.state, animationFrame);
  }
}

function drawEntity(ctx, x, y, tileSize, spriteId, state, frame) {
  const bounce = Math.sin(frame / 10) * 2;

  // Shadow
  ctx.fillStyle = 'rgba(0, 0, 0, 0.3)';
  ctx.beginPath();
  ctx.ellipse(x + tileSize / 2, y + tileSize - 2, tileSize / 3, tileSize / 6, 0, 0, Math.PI * 2);
  ctx.fill();

  // Entity body
  ctx.fillStyle = getEntityColor(spriteId);
  ctx.fillRect(x + 4, y + 4 + bounce, tileSize - 8, tileSize - 12);

  // Face
  ctx.fillStyle = '#ffeaa7';
  ctx.fillRect(x + 8, y + 6 + bounce, tileSize - 16, tileSize / 3);

  // State indicator
  drawStateIndicator(ctx, x, y, tileSize, state);
}

function getEntityColor(spriteId) {
  return spriteId.includes('customer') ? '#74b9ff' : '#55efc4';
}

function drawStateIndicator(ctx, x, y, tileSize, state) {
  const iconSize = 6;
  const offsetX = x + tileSize - iconSize - 2;
  const offsetY = y + 2;

  switch (state) {
    case 'eating':
      ctx.fillStyle = '#00b894';
      ctx.fillRect(offsetX, offsetY, iconSize, iconSize);
      break;
    case 'waiting':
      ctx.fillStyle = '#fdcb6e';
      ctx.fillRect(offsetX, offsetY, iconSize, iconSize);
      break;
    case 'seated':
      ctx.fillStyle = '#0984e3';
      ctx.fillRect(offsetX, offsetY, iconSize, iconSize);
      break;
    case 'idle':
      ctx.fillStyle = '#636e72';
      ctx.fillRect(offsetX, offsetY, iconSize, iconSize);
      break;
  }
}

function renderFacilities(ctx, scene, facilitySprites) {
  const ts = scene.tileSize;

  for (const facility of facilitySprites) {
    // Draw facility level indicators above the facility
    // This is a simplified version - in production, you'd track facility positions
    const levelColor = facility.level > 1 ? '#00b894' : '#636e72';
    ctx.fillStyle = levelColor;
    ctx.font = '10px monospace';
    ctx.fillText(`Lv${facility.level}`, 10, 20);
  }
}

export function requestAnimationFrame(dotNetRef) {
  requestAnimationFrame(timestamp => {
    dotNetRef.invokeMethodAsync('OnAnimationFrame', timestamp);
  });
}

export function disposeInnScene(canvasId) {
  delete innScenes[canvasId];
  console.log(`Inn scene disposed: ${canvasId}`);
}
