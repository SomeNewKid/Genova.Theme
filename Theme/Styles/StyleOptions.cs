// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Attributes;

namespace Genova.Theme.Styles;

/// <summary>
/// Holds options for building Cascading StyleSheets (CSS).
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for use by websites.")]
public sealed class StyleOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to include the comments in the generated stylesheet.
    /// </summary>
    public bool Commentary { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to condense the CSS rules to a single line.
    /// </summary>
    /// <remarks>
    /// Applies only to the essential CSS which appears in the &lt;head&gt; of the HTML document.
    /// </remarks>
    public bool Condense { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to include the button which controls the header navigation.
    /// </summary>
    public bool UseHeaderMenuButton { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether add an icon to external links.
    /// </summary>
    public bool UseExternalLinkIcon { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to use a sticky footer.
    /// </summary>
    public bool UseStickyFooter { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to use smooth scrolling.
    /// </summary>
    public bool UseSmoothScrolling { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to use bumping utility classes.
    /// </summary>
    public bool UseBumping { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to use a layout container.
    /// </summary>
    public bool UseLayoutContainer { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to use layout columns.
    /// </summary>
    public bool UseLayoutColumns { get; set; } = false;

    /// <summary>
    /// Gets or sets the grap between columns, when <see cref="UseLayoutColumns"/> is true.
    /// </summary>
    public decimal ColumnGap { get; set; } = 1.0m;

    /// <summary>
    /// Gets or sets the breakpoint (maximum width) for large screens.
    /// </summary>
    public int LargeBreakpoint { get; set; } = 1140;

    /// <summary>
    /// Gets or sets the breakpoint for medium screens, below which the layout may change.
    /// </summary>
    public int MediumBreakpoint { get; set; } = 900;

    /// <summary>
    /// Gets or sets the breakpoint for small screens, below which the layout will change to a single column.
    /// </summary>
    public int SmallBreakpoint { get; set; } = 600;

    /// <summary>
    /// Gets or sets the CSS selectors that determine which elements will be styled with the font stack
    /// defined by <see cref="BodyFontStack"/>. The default value is <c>body</c>.
    /// </summary>
    public IEnumerable<string> BodyFontSelectors { get; set; } = new[] { "body" };

    /// <summary>
    /// Gets or sets the font stack for the body text of the web page. If not set, a default font stack will be used.
    /// </summary>
    public FontStack? BodyFontStack { get; set; }

    /// <summary>
    /// Gets or sets the CSS selectors that determine which elements will be styled with the font stack
    /// defined by <see cref="HeadingFontStack"/>.
    /// The default value is <c>h1</c>, <c>h2</c>, <c>h3</c>, <c>h4</c>, <c>h5</c>, and <c>h6</c>.
    /// </summary>
    public IEnumerable<string> HeadingFontSelectors { get; set; } = new[] { "h1", "h2", "h3", "h4", "h5", "h6" };

    /// <summary>
    /// Gets or sets the font stack for headings in the web page. If not set, the body font stack will be used.
    /// </summary>
    public FontStack? HeadingFontStack { get; set; }

    /// <summary>
    /// Gets or sets the font weight for headings in the web page. If not set, the body font weight will be used.
    /// </summary>
    public FontWeight HeadingFontWeight { get; set; } = FontWeight.Bold;

    /// <summary>
    /// Gets or sets the CSS selector that determines which elements will be styled with the font stack
    /// defined by <see cref="ArticleFontStack"/>. The default value is <c>main article</c>.
    /// </summary>
    public IEnumerable<string> ArticleFontSelectors { get; set; } = new[] { "main article" };

    /// <summary>
    /// Gets or sets the font stack for the main articles of the web page. If not set, the body font stack will be used.
    /// </summary>
    public FontStack? ArticleFontStack { get; set; }

    /// <summary>
    /// Gets or sets the CSS selector that determines which elements will be styled with the font stack
    /// defined by <see cref="AsideFontStack"/>. The default value is <c>main aside</c>.
    /// </summary>
    public IEnumerable<string> AsideFontSelectors { get; set; } = new[] { "main aside" };

    /// <summary>
    /// Gets or sets the font stack for the main aside of the web page. If not set, the body font stack will be used.
    /// </summary>
    public FontStack? AsideFontStack { get; set; }

    /// <summary>
    /// Gets or sets the light theme for the web page, which is used by default.
    /// </summary>
    public StyleTheme? LightTheme { get; set; }

    /// <summary>
    /// Gets or sets the light high-constrast theme for the web page.
    /// </summary>
    public StyleTheme? LightContrastTheme { get; set; }

    /// <summary>
    /// Gets or sets the optional dark theme for the web page.
    /// </summary>
    public StyleTheme? DarkTheme { get; set; }

    /// <summary>
    /// Gets or sets the optional dark high-contrast theme for the web page.
    /// </summary>
    public StyleTheme? DarkContrastTheme { get; set; }
}
