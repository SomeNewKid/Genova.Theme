// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Attributes;

namespace Genova.Theme.Styles;

/// <summary>
/// Holds options for building Cascading StyleSheets (CSS).
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for use by websites.")]
public sealed class StyleTheme
{
    /// <summary>
    /// Gets or sets the foreground color for the web page, typically used for text.
    /// </summary>
    public string? Foreground { get; set; }

    /// <summary>
    /// Gets or sets the background color for the web page, typically used for the body background.
    /// </summary>
    public string? Background { get; set; }

    /// <summary>
    /// Gets or sets the color of hyperlinks.
    /// </summary>
    public string? LinkColor { get; set; }

    /// <summary>
    /// Gets or sets the color of visited hyperlinks.
    /// </summary>
    public string? VisitedLinkColor { get; set; }
}
