// tab-visibility.js
// Handles Page Visibility API for detecting tab hidden/visible state
// Place in: src/RealmsOfIdle.Client.Blazor/wwwroot/js/tab-visibility.js

let dotNetHelper = null;
let visibilityHandler = null;

export function initialize(dotNetRef) {
    dotNetHelper = dotNetRef;

    visibilityHandler = async function() {
        const isHidden = document.hidden;

        try {
            await dotNetHelper.invokeMethodAsync('OnVisibilityChange', isHidden);
        } catch (error) {
            console.error('Failed to notify visibility change:', error);
        }
    };

    // Listen for visibility change events
    document.addEventListener('visibilitychange', visibilityHandler);

    // Log initial state
    console.log(`Tab visibility initialized. Current state: ${document.hidden ? 'hidden' : 'visible'}`);
}

export function cleanup() {
    if (visibilityHandler) {
        document.removeEventListener('visibilitychange', visibilityHandler);
        visibilityHandler = null;
    }

    dotNetHelper = null;
}

// Get current visibility state (for testing)
export function isHidden() {
    return document.hidden;
}
