// Scene Renderer for Realms of Idle
// Handles tile-based rendering with camera, viewport, and animations

if (!window.realmsOfIdle) {
    window.realmsOfIdle = {};
}

(function () {
    'use strict';

    // Scene state
    const scenes = new Map();

    /**
     * Initialize a scene
     */
    function initScene(canvasId, width, height, tileSize, showMinimap, dotNetRef) {
        const canvas = document.getElementById(canvasId);
        if (!canvas) {
            console.error(`Canvas element with ID '${canvasId}' not found`);
            return false;
        }

        canvas.width = width;
        canvas.height = height;
        canvas.style.imageRendering = 'pixelated';
        canvas.style.imageRendering = '-moz-crisp-edges';
        canvas.style.imageRendering = 'crisp-edges';

        const ctx = canvas.getContext('2d');
        if (!ctx) {
            console.error(`Failed to get 2D context for canvas '${canvasId}'`);
            return false;
        }

        ctx.imageSmoothingEnabled = false;

        scenes.set(canvasId, {
            ctx,
            width,
            height,
            tileSize,
            cameraX: 0,
            cameraY: 0,
            targetCameraX: 0,
            targetCameraY: 0,
            tiles: [],
            characters: [],
            lastTime: 0,
            animationFrame: 0
        });

        // Setup input handlers
        setupInputHandlers(canvasId, dotNetRef);

        return true;
    }

    /**
     * Setup mouse/touch/keyboard input handlers
     */
    function setupInputHandlers(canvasId, dotNetRef) {
        const canvas = document.getElementById(canvasId);
        if (!canvas) return;

        // Mouse drag panning
        let isDragging = false;
        let dragStartX = 0;
        let dragStartY = 0;

        canvas.addEventListener('mousedown', (e) => {
            isDragging = true;
            dragStartX = e.clientX;
            dragStartY = e.clientY;
        });

        canvas.addEventListener('mousemove', (e) => {
            if (!isDragging) return;

            const dx = e.clientX - dragStartX;
            const dy = e.clientY - dragStartY;

            if (Math.abs(dx) > 5 || Math.abs(dy) > 5) {
                // Send pan command to .NET
                const scene = scenes.get(canvasId);
                const tileSize = scene.tileSize;

                const panX = Math.sign(dx);
                const panY = Math.sign(dy);

                if (dotNetRef) {
                    dotNetRef.invokeMethodAsync('PanCamera', panX, panY);
                }

                dragStartX = e.clientX;
                dragStartY = e.clientY;
            }
        });

        canvas.addEventListener('mouseup', () => {
            isDragging = false;
        });

        canvas.addEventListener('mouseleave', () => {
            isDragging = false;
        });

        // Click handler
        canvas.addEventListener('click', async (e) => {
            const rect = canvas.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            if (dotNetRef) {
                await dotNetRef.invokeMethodAsync('HandleCanvasClick', Math.floor(x), Math.floor(y));
            }
        });

        // Keyboard panning (WASD/Arrows)
        document.addEventListener('keydown', (e) => {
            // Only handle if canvas is visible
            if (!canvas.offsetParent) return;

            let direction = null;
            switch (e.key) {
                case 'ArrowUp':
                case 'w':
                case 'W':
                    direction = 'North';
                    break;
                case 'ArrowDown':
                case 's':
                case 'S':
                    direction = 'South';
                    break;
                case 'ArrowLeft':
                case 'a':
                case 'A':
                    direction = 'West';
                    break;
                case 'ArrowRight':
                case 'd':
                case 'D':
                    direction = 'East';
                    break;
            }

            if (direction !== null && dotNetRef) {
                dotNetRef.invokeMethodAsync('PanCamera', direction);
            }
        });
    }

    /**
     * Render the scene
     */
    function renderScene(canvasId, tilesJson, cameraX, cameraY, tileSize, animationFrame) {
        const scene = scenes.get(canvasId);
        if (!scene) return;

        const ctx = scene.ctx;
        const width = scene.width;
        const height = scene.height;

        // Clear canvas
        ctx.fillStyle = '#0a0e14';
        ctx.fillRect(0, 0, width, height);

        const tiles = JSON.parse(tilesJson || '[]');

        // Calculate viewport bounds
        const startTileX = Math.floor(cameraX);
        const startTileY = Math.floor(cameraY);
        const endTileX = startTileX + Math.ceil(width / tileSize) + 1;
        const endTileY = startTileY + Math.ceil(height / tileSize) + 1;

        // Render tiles
        tiles.forEach(tile => {
            // Skip tiles outside viewport
            if (tile.x < startTileX || tile.x >= endTileX ||
                tile.y < startTileY || tile.y >= endTileY) {
                return;
            }

            const screenX = (tile.x - cameraX) * tileSize;
            const screenY = (tile.y - cameraY) * tileSize;

            drawTile(ctx, screenX, screenY, tileSize, tile, animationFrame);
        });
    }

    /**
     * Draw a tile based on its type
     */
    function drawTile(ctx, x, y, size, tile, frame) {
        ctx.save();

        switch (tile.type) {
            case 1: // Floor
            case 2: // Floor2
            case 3: // Floor3
                drawFloorTile(ctx, x, y, size, tile.variant, frame);
                break;
            case 10: // Wall
            case 11: // Wall2
                drawWallTile(ctx, x, y, size, tile.variant);
                break;
            case 20: // Door
                drawDoorTile(ctx, x, y, size);
                break;
            case 30: // Table
                drawTableTile(ctx, x, y, size);
                break;
            case 31: // Chair
                drawChairTile(ctx, x, y, size);
                break;
            case 32: // Counter
                drawCounterTile(ctx, x, y, size);
                break;
            case 33: // Oven
                drawOvenTile(ctx, x, y, size, frame);
                break;
            case 34: // Bed
                drawBedTile(ctx, x, y, size);
                break;
            case 35: // Fireplace (animated)
                drawFireplaceTile(ctx, x, y, size, frame);
                break;
        }

        ctx.restore();
    }

    function drawFloorTile(ctx, x, y, size, variant, frame) {
        // Base floor color
        ctx.fillStyle = '#1a2a1a';
        ctx.fillRect(x, y, size, size);

        // Add floor pattern based on variant
        ctx.fillStyle = 'rgba(0, 0, 0, 0.15)';

        if (variant === 0) {
            // Dot pattern
            for (let i = 0; i < 3; i++) {
                for (let j = 0; j < 3; j++) {
                    if ((i + j) % 2 === 0) {
                        ctx.fillRect(x + 4 + i * 8, y + 4 + j * 8, 2, 2);
                    }
                }
            }
        } else if (variant === 1) {
            // Cross pattern
            ctx.fillRect(x + size / 2 - 1, y + 2, 2, size - 4);
            ctx.fillRect(x + 2, y + size / 2 - 1, size - 4, 2);
        } else {
            // Ring pattern
            ctx.fillRect(x + 2, y + 2, size - 4, size - 4);
            ctx.fillStyle = '#1a2a1a';
            ctx.fillRect(x + 6, y + 6, size - 12, size - 12);
        }
    }

    function drawWallTile(ctx, x, y, size, variant) {
        // Base wall
        ctx.fillStyle = '#2d3a2d';
        ctx.fillRect(x, y, size, size);

        // Brick pattern
        ctx.fillStyle = '#1e2a1f';
        ctx.fillRect(x + 1, y + size * 0.3, size - 2, size * 0.15);
        ctx.fillRect(x + 1, y + size * 0.6, size - 2, size * 0.15);
        ctx.fillRect(x + 1, y + size * 0.85, size - 2, size * 0.1);
    }

    function drawDoorTile(ctx, x, y, size) {
        // Door frame
        ctx.fillStyle = '#3a2a1a';
        ctx.fillRect(x + size * 0.1, y, size * 0.8, size);

        // Door opening
        ctx.fillStyle = '#1a1a1a';
        ctx.fillRect(x + size * 0.15, y + size * 0.1, size * 0.7, size * 0.8);

        // Door handle
        ctx.fillStyle = '#8b7355';
        ctx.fillRect(x + size * 0.7, y + size * 0.5, size * 0.08, size * 0.2);
    }

    function drawTableTile(ctx, x, y, size) {
        // Table surface
        ctx.fillStyle = '#5c4a3a';
        ctx.fillRect(x + 2, y + size * 0.6, size - 4, size * 0.35);

        // Table legs
        ctx.fillStyle = '#3a2a1a';
        ctx.fillRect(x + 4, y + size * 0.6, 3, size * 0.4);
        ctx.fillRect(x + size - 7, y + size * 0.6, 3, size * 0.4);
    }

    function drawChairTile(ctx, x, y, size) {
        // Chair back
        ctx.fillStyle = '#6b5344';
        ctx.fillRect(x + size * 0.2, y + size * 0.2, size * 0.6, size * 0.4);

        // Seat
        ctx.fillStyle = '#5c4a3a';
        ctx.fillRect(x + size * 0.1, y + size * 0.5, size * 0.8, size * 0.3);

        // Legs
        ctx.fillStyle = '#3a2a1a';
        ctx.fillRect(x + size * 0.2, y + size * 0.7, 3, size * 0.25);
        ctx.fillRect(x + size * 0.6, y + size * 0.7, 3, size * 0.25);
    }

    function drawCounterTile(ctx, x, y, size) {
        // Counter surface
        ctx.fillStyle = '#6b5344';
        ctx.fillRect(x, y + size * 0.5, size, size * 0.4);

        // Front edge
        ctx.fillStyle = '#4a3a2a';
        ctx.fillRect(x, y + size * 0.85, size, size * 0.15);
    }

    function drawOvenTile(ctx, x, y, size, frame) {
        // Oven body
        ctx.fillStyle = '#3a2a1a';
        ctx.fillRect(x + size * 0.15, y + size * 0.2, size * 0.7, size * 0.7);

        // Oven door
        ctx.fillStyle = '#1a1a1a';
        ctx.fillRect(x + size * 0.2, y + size * 0.3, size * 0.6, size * 0.5);

        // Animated glow
        const glow = 0.7 + Math.sin(frame * 0.2) * 0.3;
        ctx.fillStyle = `rgba(255, 100, 50, ${glow})`;
        ctx.fillRect(x + size * 0.25, y + size * 0.4, size * 0.5, size * 0.3);
    }

    function drawBedTile(ctx, x, y, size) {
        // Bed frame
        ctx.fillStyle = '#4a3a2a';
        ctx.fillRect(x + 2, y + size * 0.4, size - 4, size * 0.55);

        // Pillow
        ctx.fillStyle = '#6b5344';
        ctx.fillRect(x + 4, y + size * 0.45, size * 0.5, size * 0.2);

        // Blanket
        ctx.fillStyle = '#5c4a3a';
        ctx.fillRect(x + 4, y + size * 0.65, size - 8, size * 0.25);
    }

    function drawFireplaceTile(ctx, x, y, size, frame) {
        // Fireplace structure
        ctx.fillStyle = '#2a1a1a';
        ctx.fillRect(x + size * 0.1, y + size * 0.3, size * 0.8, size * 0.65);

        // Fire chamber
        ctx.fillStyle = '#1a0a0a';
        ctx.fillRect(x + size * 0.15, y + size * 0.4, size * 0.7, size * 0.45);

        // Animated fire flicker
        const flicker = 0.5 + Math.sin(frame * 0.5 + Math.random() * 0.5) * 0.5;
        const fireHeight = 0.4 * flicker;

        ctx.fillStyle = `rgba(255, 150, 50, ${flicker})`;
        ctx.fillRect(x + size * 0.2, y + size * (0.75 - fireHeight), size * 0.6, size * fireHeight);

        // Glow effect
        ctx.fillStyle = `rgba(255, 200, 100, ${flicker * 0.3})`;
        ctx.fillRect(x + size * 0.15, y + size * 0.5, size * 0.7, size * 0.5);
    }

    /**
     * Request animation frame
     */
    function requestAnimationFrame(dotNetRef) {
        requestAnimationFrame((timestamp) => {
            if (dotNetRef) {
                dotNetRef.invokeMethodAsync('OnAnimationFrame', timestamp);
            }
        });
    }

    // Export public API
    window.realmsOfIdle.sceneRenderer = {
        initScene,
        renderScene,
        requestAnimationFrame
    };

    console.log('Realms of Idle Scene Renderer initialized');
})();
