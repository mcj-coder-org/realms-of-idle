// Theme Manager for Realms of Idle
// Handles theme switching and persistence

if (!window.realmsOfIdle) {
  window.realmsOfIdle = {};
}

(function () {
  'use strict';

  const THEME_STORAGE_KEY = 'realms-of-idle-theme';
  const THEME_ATTRIBUTE = 'data-theme';

  /**
   * Get the current theme from DOM or storage
   */
  function getTheme() {
    // First check DOM attribute
    const currentTheme = document.documentElement.getAttribute(THEME_ATTRIBUTE);
    if (currentTheme) {
      return currentTheme;
    }

    // Then check localStorage
    const storedTheme = localStorage.getItem(THEME_STORAGE_KEY);
    if (storedTheme) {
      return storedTheme;
    }

    // Default to color theme
    return 'color';
  }

  /**
   * Set the theme
   */
  function setTheme(theme) {
    document.documentElement.setAttribute(THEME_ATTRIBUTE, theme);
    localStorage.setItem(THEME_STORAGE_KEY, theme);
  }

  /**
   * Get theme enum value from CSS theme name
   */
  function themeNameToEnum(themeName) {
    switch (themeName) {
      case 'green':
        return 1; // Theme.Green
      case 'amber':
        return 2; // Theme.Amber
      default:
        return 0; // Theme.Color
    }
  }

  /**
   * Get CSS theme name from theme enum value
   */
  function enumToThemeName(themeEnum) {
    switch (themeEnum) {
      case 1:
        return 'green';
      case 2:
        return 'amber';
      default:
        return 'color';
    }
  }

  /**
   * Initialize theme from storage
   */
  function initTheme() {
    const theme = getTheme();
    document.documentElement.setAttribute(THEME_ATTRIBUTE, theme);
    return themeNameToEnum(theme);
  }

  /**
   * Change to the next theme in the cycle
   */
  function cycleTheme() {
    const themes = ['color', 'green', 'amber'];
    const currentTheme = getTheme();
    const currentIndex = themes.indexOf(currentTheme);
    const nextIndex = (currentIndex + 1) % themes.length;
    const nextTheme = themes[nextIndex];

    setTheme(nextTheme);
    return themeNameToEnum(nextTheme);
  }

  /**
   * Set a specific theme
   */
  function changeTheme(themeEnum) {
    const themeName = enumToThemeName(themeEnum);
    setTheme(themeName);
  }

  // Export public API
  window.realmsOfIdle.themeManager = {
    initTheme,
    getTheme: () => themeNameToEnum(getTheme()),
    setTheme: changeTheme,
    cycleTheme,
  };

  // Auto-initialize on load
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initTheme);
  } else {
    initTheme();
  }

  console.log('Realms of Idle Theme Manager initialized');
})();
