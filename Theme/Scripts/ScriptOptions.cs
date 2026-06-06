// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Attributes;

namespace Genova.Theme.Scripts;

/// <summary>
/// Holds options for building JavaScript resources.
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for use by websites.")]
public sealed class ScriptOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to include the comments in the generated script.
    /// </summary>
    public bool Commentary { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include the button which controls the header navigation.
    /// </summary>
    public bool IncludeHeaderMenuButton { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to use a hamburger button to control the header navigation.
    /// </summary>
    public bool HamburgerHeaderMenuButton { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include the theme switcher script.
    /// </summary>
    public bool IncludeThemeSwitcher { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include the theme toggler script.
    /// </summary>
    public bool IncludeThemeToggler { get; set; } = false;
}
