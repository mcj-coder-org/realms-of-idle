// Tab Visibility API handler for offline progress detection
// Calls .NET TabVisibilityHandler via JSInterop

let dotNetRef = null;
let hiddenAt = null;

export function initialize(dotNetReference) {
    dotNetRef = dotNetReference;

    document.addEventListener("visibilitychange", onVisibilityChange);
}

function onVisibilityChange() {
    if (!dotNetRef) return;

    if (document.hidden) {
        hiddenAt = Date.now();
        dotNetRef.invokeMethodAsync("NotifyTabHidden");
    } else if (hiddenAt !== null) {
        const elapsedSeconds = (Date.now() - hiddenAt) / 1000;
        hiddenAt = null;
        dotNetRef.invokeMethodAsync("NotifyTabVisible", elapsedSeconds);
    }
}

export function dispose() {
    document.removeEventListener("visibilitychange", onVisibilityChange);
    dotNetRef = null;
    hiddenAt = null;
}
