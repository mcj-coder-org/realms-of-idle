// Canvas Rendering Engine for Realms of Idle
// Handles 2D canvas operations with pixel-art aesthetic

// Ensure we only initialize once
if (!window.realmsOfIdle) {
  window.realmsOfIdle = {};
}

(function () {
  'use strict';

  // Store canvas contexts by element ID
  const contexts = new Map();

  /**
   * Initialize a canvas for rendering
   * @param {string} canvasId - The ID of the canvas element
   * @param {number} width - The width of the canvas in pixels
   * @param {number} height - The height of the canvas in pixels
   * @returns {boolean} Success status
   */
  function initCanvas(canvasId, width, height) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) {
      console.error(`Canvas element with ID '${canvasId}' not found`);
      return false;
    }

    // Set canvas size
    canvas.width = width;
    canvas.height = height;

    // Enable pixelated rendering
    canvas.style.imageRendering = 'pixelated';
    canvas.style.imageRendering = '-moz-crisp-edges';
    canvas.style.imageRendering = 'crisp-edges';

    const ctx = canvas.getContext('2d');
    if (!ctx) {
      console.error(`Failed to get 2D context for canvas '${canvasId}'`);
      return false;
    }

    // Disable image smoothing for pixel art
    ctx.imageSmoothingEnabled = false;
    ctx.mozImageSmoothingEnabled = false;
    ctx.webkitImageSmoothingEnabled = false;
    ctx.msImageSmoothingEnabled = false;

    contexts.set(canvasId, ctx);
    return true;
  }

  /**
   * Clear the entire canvas
   * @param {string} canvasId - The ID of the canvas element
   */
  function clearCanvas(canvasId) {
    const ctx = contexts.get(canvasId);
    if (!ctx) {
      console.warn(`Canvas '${canvasId}' not initialized`);
      return;
    }

    const canvas = ctx.canvas;
    ctx.clearRect(0, 0, canvas.width, canvas.height);
  }

  /**
   * Draw a sprite at the specified position
   * @param {string} canvasId - The ID of the canvas element
   * @param {number} x - X position in pixels
   * @param {number} y - Y position in pixels
   * @param {string} color - Color value (hex, rgb, or named color)
   * @param {number} scale - Scale factor (default 1)
   */
  function drawSprite(canvasId, x, y, color, scale = 1) {
    const ctx = contexts.get(canvasId);
    if (!ctx) {
      console.warn(`Canvas '${canvasId}' not initialized`);
      return;
    }

    // Default sprite size: 16x20 pixels
    const spriteWidth = 16;
    const spriteHeight = 20;

    ctx.save();
    ctx.translate(x, y);
    ctx.scale(scale, scale);

    // Draw a simple pixel-art style character outline
    ctx.fillStyle = color;
    ctx.strokeStyle = color;
    ctx.lineWidth = 1;

    // Body (simple rectangle outline)
    ctx.strokeRect(0, 0, spriteWidth, spriteHeight);

    // Head area
    ctx.fillRect(4, 2, 8, 6);

    // Body area
    ctx.fillRect(2, 10, 12, 8);

    ctx.restore();
  }

  /**
   * Draw a tile at the specified position
   * @param {string} canvasId - The ID of the canvas element
   * @param {number} x - X position in pixels
   * @param {number} y - Y position in pixels
   * @param {string} tileType - Type of tile ('floor', 'wall', 'door', etc.)
   * @param {string} color - Color value
   * @param {number} tileSize - Size of the tile in pixels (default 16)
   */
  function drawTile(canvasId, x, y, tileType, color, tileSize = 16) {
    const ctx = contexts.get(canvasId);
    if (!ctx) {
      console.warn(`Canvas '${canvasId}' not initialized`);
      return;
    }

    ctx.save();
    ctx.fillStyle = color;
    ctx.strokeStyle = color;
    ctx.lineWidth = 1;

    switch (tileType) {
      case 'floor':
        // Draw floor pattern (dots)
        ctx.fillRect(x, y, tileSize, tileSize);
        ctx.fillStyle = 'rgba(0, 0, 0, 0.2)';
        ctx.fillRect(x + 4, y + 4, 2, 2);
        ctx.fillRect(x + 10, y + 10, 2, 2);
        break;

      case 'wall':
        // Draw solid wall with brick pattern
        ctx.fillRect(x, y, tileSize, tileSize);
        ctx.strokeStyle = 'rgba(0, 0, 0, 0.3)';
        ctx.strokeRect(x + 1, y + 4, tileSize - 2, 3);
        ctx.strokeRect(x + 1, y + 10, tileSize - 2, 3);
        break;

      case 'door':
        // Draw door frame
        ctx.fillRect(x, y, tileSize, tileSize);
        ctx.fillStyle = '#4a3c2a';
        ctx.fillRect(x + 4, y + 2, 8, tileSize - 4);
        ctx.strokeStyle = '#8b7355';
        ctx.strokeRect(x + 4, y + 2, 8, tileSize - 4);
        break;

      default:
        ctx.fillRect(x, y, tileSize, tileSize);
    }

    ctx.restore();
  }

  /**
   * Draw a background fill
   * @param {string} canvasId - The ID of the canvas element
   * @param {string} color - Background color
   */
  function drawBackground(canvasId, color) {
    const ctx = contexts.get(canvasId);
    if (!ctx) {
      console.warn(`Canvas '${canvasId}' not initialized`);
      return;
    }

    const canvas = ctx.canvas;
    ctx.fillStyle = color;
    ctx.fillRect(0, 0, canvas.width, canvas.height);
  }

  /**
   * Draw text on the canvas
   * @param {string} canvasId - The ID of the canvas element
   * @param {string} text - Text to draw
   * @param {number} x - X position
   * @param {number} y - Y position
   * @param {string} color - Text color
   * @param {string} font - CSS font string (optional)
   */
  function drawText(canvasId, text, x, y, color, font = '16px VT323') {
    const ctx = contexts.get(canvasId);
    if (!ctx) {
      console.warn(`Canvas '${canvasId}' not initialized`);
      return;
    }

    ctx.save();
    ctx.font = font;
    ctx.fillStyle = color;
    ctx.textBaseline = 'top';
    ctx.fillText(text, x, y);
    ctx.restore();
  }

  /**
   * Draw a rectangular outline
   * @param {string} canvasId - The ID of the canvas element
   * @param {number} x - X position
   * @param {number} y - Y position
   * @param {number} width - Width in pixels
   * @param {number} height - Height in pixels
   * @param {string} color - Border color
   * @param {number} lineWidth - Border width (default 1)
   */
  function drawRect(canvasId, x, y, width, height, color, lineWidth = 1) {
    const ctx = contexts.get(canvasId);
    if (!ctx) {
      console.warn(`Canvas '${canvasId}' not initialized`);
      return;
    }

    ctx.save();
    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;
    ctx.strokeRect(x, y, width, height);
    ctx.restore();
  }

  /**
   * Draw a filled rectangle
   * @param {string} canvasId - The ID of the canvas element
   * @param {number} x - X position
   * @param {number} y - Y position
   * @param {number} width - Width in pixels
   * @param {number} height - Height in pixels
   * @param {string} color - Fill color
   */
  function fillRect(canvasId, x, y, width, height, color) {
    const ctx = contexts.get(canvasId);
    if (!ctx) {
      console.warn(`Canvas '${canvasId}' not initialized`);
      return;
    }

    ctx.save();
    ctx.fillStyle = color;
    ctx.fillRect(x, y, width, height);
    ctx.restore();
  }

  /**
   * Get the canvas data URL (for screenshots, etc.)
   * @param {string} canvasId - The ID of the canvas element
   * @returns {string|null} Data URL or null if canvas not found
   */
  function toDataURL(canvasId) {
    const ctx = contexts.get(canvasId);
    if (!ctx) {
      console.warn(`Canvas '${canvasId}' not initialized`);
      return null;
    }

    return ctx.canvas.toDataURL();
  }

  // Export public API
  window.realmsOfIdle.canvasRenderer = {
    initCanvas,
    clearCanvas,
    drawSprite,
    drawTile,
    drawBackground,
    drawText,
    drawRect,
    fillRect,
    toDataURL,
  };

  console.log('Realms of Idle Canvas Renderer initialized');
})();
