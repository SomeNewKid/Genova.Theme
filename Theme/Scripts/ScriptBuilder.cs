// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Common.Attributes;

namespace Genova.Theme.Scripts;

/// <summary>
/// Provides functionality to build content for JavaScript resources.
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for use by websites.")]
public sealed class ScriptBuilder
{
    private readonly ScriptOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptBuilder"/> class with the specified script options.
    /// </summary>
    /// <param name="options">The options to use when building JavaScript resources.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="options"/> is <c>null</c>.</exception>
    public ScriptBuilder(ScriptOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _options = options;
    }

    /// <summary>
    /// Builds the content for the plain JavaScript resource.
    /// </summary>
    /// <returns>An empty string.</returns>
    public string BuildPlain()
    {
        StringBuilder builder = new();

        if (_options.IncludeHeaderMenuButton)
        {
            builder.AppendLine(Comment("// Dynamically create \"Menu\" button, then hide & toggle the nav."));
            builder.AppendLine("""
                (function () {
                    const headerBar = document.querySelector('.masthead');
                    const nav = document.getElementById('site-navigation');
                    if (!headerBar || !nav) return;
                """);

            if (_options.HamburgerHeaderMenuButton)
            {
                builder.AppendLine("""
                    // Build the hamburger button
                    const btn = document.createElement('button');
                    btn.id = 'menu-toggle';
                    btn.setAttribute('aria-controls', 'site-navigation');
                    btn.setAttribute('aria-expanded', 'false');
                    btn.setAttribute('aria-label', 'Open menu');
                    btn.innerHTML = `
                      <span class="icon-open" aria-hidden="true">☰</span>
                      <span class="icon-close" aria-hidden="true">✕</span>
                    `;
                    """);
            }
            else
            {
                builder.AppendLine("""
                    // Build the button
                    const btn = document.createElement('button');
                    btn.id = 'menu-toggle';
                    btn.textContent = 'Menu';
                    btn.setAttribute('aria-controls', 'site-navigation');
                    btn.setAttribute('aria-expanded', 'false');
                    btn.setAttribute('aria-label', 'Open menu');
                    """);
            }

            builder.AppendLine("""
                    // Stick it at the end of the header bar
                    headerBar.appendChild(btn);

                    // Hide nav until user asks for it
                    nav.hidden = true;
                    nav.classList.remove('visually-hidden');

                    // Toggle
                    btn.addEventListener('click', () => {
                        const open = nav.hidden;
                        nav.hidden = !open;
                        btn.setAttribute('aria-expanded', String(open));
                        btn.setAttribute('aria-label', open ? 'Close menu' : 'Open menu');
                        (open ? nav : btn).focus();
                    });
                }());
                """);
            builder.AppendLine();
        }

        if (_options.IncludeThemeSwitcher)
        {
            builder.AppendLine(Comment("// Dynamically render theme/contrast switcher and handle preferences."));
            builder.AppendLine("""
                (function () {
                    document.documentElement.classList.add('js-enabled');

                    // Target the empty section
                    const readingOptions = document.getElementById('reading-options');
                    if (!readingOptions) return;

                    // Render the theme switcher HTML
                    readingOptions.innerHTML = `
                        <h2 class="visually-hidden">Reading options</h2>
                        <fieldset id="theme-options">
                            <legend>Choose theme</legend>
                            <label><input type="radio" name="theme" value="auto" checked> Auto</label>
                            <label><input type="radio" name="theme" value="light"> Light</label>
                            <label><input type="radio" name="theme" value="dark"> Dark</label>
                            <label><input type="checkbox" id="contrast-toggle"> High contrast</label>
                        </fieldset>
                    `;

                    const radios = readingOptions.querySelectorAll('#theme-options input[name="theme"]');
                    const contrastToggle = readingOptions.querySelector('#contrast-toggle');

                    const applyTheme = function (v) {
                        if (v === 'light' || v === 'dark') {
                            document.documentElement.dataset.theme = v;
                        } else {
                            delete document.documentElement.dataset.theme;
                        }
                    };
                    const applyContrast = function (on) {
                        if (on) {
                            document.documentElement.dataset.contrast = 'high';
                        } else {
                            delete document.documentElement.dataset.contrast;
                        }
                    };

                    // Restore saved prefs – fail silently if cookies disabled.
                    let storedTheme, storedContrast;
                    try {
                        storedTheme = localStorage.getItem('pow-theme');
                        storedContrast = localStorage.getItem('pow-contrast') === 'high';
                    } catch (e) { }
                    if (storedTheme) {
                        applyTheme(storedTheme);
                        const storedRadio = readingOptions.querySelector(`#theme-options input[value="${storedTheme}"]`);
                        if (storedRadio) storedRadio.checked = true;
                    }
                    if (storedContrast) {
                        applyContrast(true);
                        if (contrastToggle) contrastToggle.checked = true;
                    }

                    // Wire listeners
                    radios.forEach(function (r) {
                        r.addEventListener('change', function () {
                            applyTheme(r.value);
                            try { localStorage.setItem('pow-theme', r.value); } catch (e) { }
                        });
                    });
                    if (contrastToggle) {
                        contrastToggle.addEventListener('change', function () {
                            applyContrast(contrastToggle.checked);
                            try { localStorage.setItem('pow-contrast', contrastToggle.checked ? 'high' : ''); } catch (e) { }
                        });
                    }
                }());
                """);
            builder.AppendLine();
        }

        if (_options.IncludeThemeToggler)
        {
            builder.AppendLine(Comment("// Dynamically render theme toggle button and handle preferences."));
            builder.AppendLine("""
                (function () {
                    document.documentElement.classList.add('js-enabled');

                    const themeToggle = document.getElementById('theme-toggle');
                    if (!themeToggle) return;

                    // Helper to apply theme
                    const applyTheme = function (v) {
                        if (v === 'light' || v === 'dark') {
                            document.documentElement.dataset.theme = v;
                        } else {
                            delete document.documentElement.dataset.theme;
                        }
                    };

                    // Determine initial theme
                    let theme;
                    try {
                        theme = localStorage.getItem('pow-theme');
                    } catch (e) { }
                    if (!theme) {
                        theme = window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
                    }
                    applyTheme(theme);

                    // Create button
                    const btn = document.createElement('button');
                    btn.type = 'button';
                    btn.id = 'theme-toggle-btn';
                    btn.textContent = theme === 'dark' ? 'Dark' : 'Light';
                    btn.className = theme === 'dark' ? 'theme-is-dark' : 'theme-is-light';
                    themeToggle.appendChild(btn);

                    // Toggle logic
                    btn.addEventListener('click', function () {
                        theme = theme === 'dark' ? 'light' : 'dark';
                        applyTheme(theme);
                        btn.textContent = theme === 'dark' ? 'Dark' : 'Light';
                        btn.className = theme === 'dark' ? 'theme-is-dark' : 'theme-is-light';
                        try { localStorage.setItem('pow-theme', theme); } catch (e) { }
                    });
                }());                
                """);
            builder.AppendLine();
        }

        return builder.ToString();
    }

    private string Comment(string text)
    {
        return _options.Commentary ? text : string.Empty;
    }
}
