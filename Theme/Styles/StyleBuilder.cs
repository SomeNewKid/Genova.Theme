// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Common.Attributes;

namespace Genova.Theme.Styles;

/// <summary>
/// Provides functionality to build content for CSS style sheets.
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for use by websites.")]
public sealed class StyleBuilder
{
    private readonly StyleOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleBuilder"/> class with the specified style options.
    /// </summary>
    /// <param name="options">The options to use when building CSS style sheets.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="options"/> is <c>null</c>.</exception>
    public StyleBuilder(StyleOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _options = options;
    }

    /// <summary>
    /// Builds the content for the `essential` style sheet.
    /// </summary>
    /// <returns>The essential CSS rules.</returns>
    public string BuildEssential()
    {
        string linkColor =
            _options.LightTheme?.LinkColor ??
            _options.DarkTheme?.LinkColor ??
            "#0000ee";
        string visitedLinkColor =
            _options.LightTheme?.VisitedLinkColor ??
            _options.DarkTheme?.VisitedLinkColor ??
            "#551a8b";

        string essential =
            Comment("/* box-sizing reset: keeps padding from vandalising our math */", true) +
            """
            *, *::before, *::after {
              box-sizing: border-box;
            }

            """ +
            Comment("/* =====================  TYPOGRAPHY  ===================== */", true) +
            Comment("/* 18px root: readable on handhelds, not huge on desktop */", true) +
            """
            html {
              font-size: 18px;
            }

            body {
              line-height: 1.8;
            }

            h1, h2, h3, h4, h5, h6 {
              line-height: 1.2;
            }

            p, fieldset, .skip-links {
              margin-bottom: 1.5rem;
            }

            dt {
              margin-top: 1rem;
            }

            """ +
            Comment("/* Nudge lists closer to preceding paragraph */", true) +
            """
            p + ul, p + ol {
              margin-top: -1.25rem;
            }

            """ +
            Comment("/* =====================  FLUID CONTENT  ===================== */", true) +
            """
            img, picture, video, canvas, svg, table, pre {
              display: block;
              max-width: 100%;
              height: auto;
            }

            table, pre {
              overflow-x: auto;
            }

            """ +
            Comment("/* =====================  WIDTH CONTAINER  ===================== */", true) +
            """
            .layout-container {
              position: relative;
              padding: 0 1rem;
            }

            """ +
            Comment("/* 60ch ≈ 55–60 glyphs = happy eyeballs. */", true) +
            """
            .layout-container,
            .reading-area {
              max-width: 60ch;
              margin: 0 auto;
            }

            @media print {
              .layout-container {
                max-width: none;
              }
            }

            """ +
            Comment("/* =====================  SKIP LINKS  ===================== */", true) +
            Comment("/* Off‑screen until focused, then full‑width overlay */", true) +
            """
            .skip-links {
              position: absolute;
              top: 0;
              left: 0;
              width: 100%;
              transform: translateY(-100%);
              z-index: 1000;
            }

            .skip-links:focus-within {
              transform: translateY(0);
            }

            """ +
            Comment("/* reveal on focus */", true) +
            """
            .skip-link {
              display: block;
              width: 100%;
              text-align: center;
              font-weight: bold;
              white-space: nowrap;
              border-bottom: 1px solid currentColor;
            }

            """ +
            Comment("/* =====================  GLOBAL LINK STYLE  ===================== */", true) +
            Comment("/* Thin underline that fattens on hover/focus – GOV.UK homage. */", true) +
            """
            a {
              color: {link-color};
              text-decoration: none;
              border-bottom: 1px solid currentColor;
            }

            a:has(> abbr[title]) {
              border-bottom: none;
            }

            a.naked {
              border-bottom-color: transparent;
            }

            a:visited {
              color: {visited-link-color};
            }

            a:hover, a:hover:has(> abbr[title]),
            a:focus, a:focus:has(> abbr[title]),
            a:active, a:active:has(> abbr[title]) {
              border-bottom: 3px solid currentColor;
            }
        
            """
            .Replace("{link-color}", linkColor)
            .Replace("{visited-link-color}", visitedLinkColor) +
            Comment("/* Focus outline – loud & proud */", true) +
            """
            a:focus, button:focus {
              background-color: #ffdd00 !important;
              color: #0b0c0c !important;
              outline: 2px solid #ffdd00;
              text-decoration: none;
            }

            input:focus,
            button:focus,
            textarea:focus,
            select:focus {
              outline: 2px solid #ffdd00;
              outline-offset: 0;
              box-shadow: inset 0 0 0 2px #0b0c0c;
            }

            input[type="file"]:focus {
              outline: 2px solid #ffdd00;
            }
        
            abbr[title] {
              text-decoration: none;
              border-bottom: 1px dotted currentColor;
            }

            """ +
            Comment("/* =====================  MISCELLANEOUS  ===================== */", true) +
            """
            body > header {
              padding-top: 2.5rem;
            }

            label {
              white-space: nowrap;
            }
            """
            .Trim();

        return _options.Condense ? StyleHelper.Condense(essential) ?? "" : essential;
    }

    /// <summary>
    /// Builds the content for the `plain` style sheet.
    /// </summary>
    /// <returns>The content for the `plain` style sheet.</returns>
    public string BuildPlain()
    {
        StringBuilder builder = new();

        builder.AppendLine("""
        body {
          margin: 0;
          overflow-wrap: break-word;
          -webkit-font-smoothing: antialiased;
        }
        """);
        builder.AppendLine();

        builder.AppendLine(Comment("/* Headings */"));
        builder.AppendLine("""
        h1 { font-size: 2.222rem; }
        h2 { font-size: 1.667rem; }
        h3 { font-size: 1.333rem; }
        h4 { font-size: 1.167rem; }
        h5 { font-size: 1.0rem; } 
        h6 { font-size: 0.889rem; }

        p, li {
          text-wrap: pretty;
        }

        h1, h2, h3, h4, h5, h6 {
          text-wrap: balance;
        }
        """);
        builder.AppendLine();

        GenerateFonts(builder);

        builder.AppendLine(Comment("/* Forms */"));
        builder.AppendLine("""
        input, button, textarea, select {
          font: inherit;
        }

        input, select, textarea, img, picture, video, canvas, svg, table, pre {
          vertical-align: baseline;
        }

        label:has(+ textarea) {
          vertical-align: top;
        }

        select {
          padding: 0.25em;
          line-height: 1.2;
          vertical-align: baseline;
        }

        input, textarea, select, button {
          border: 1px solid #ccc;
          color: inherit;
        }

        input[type="range"] {
          background-color: transparent;
        }        
        """);
        builder.AppendLine();

        builder.AppendLine(Comment("/* Code */"));
        builder.AppendLine("""
        :where(code, pre) {
          border: 1px solid #ccc;
          font-family: ui-monospace, SFMono-Regular, Consolas, monospace;
          font-size: 0.9em;
        }

        code {
          padding: 0.1em 0.3em;
          white-space: nowrap;
        }

        pre > code {
          all: unset;
        }

        pre {
          padding: 1em;
          overflow-x: auto;
          line-height: 1.4;
        } 

        /* Horizontal Rule */

        hr {
          border: 0;
          border-bottom: 1px solid currentColor;
          height: 0;
          margin: 1rem 0;
        }
        """);
        builder.AppendLine();

        builder.AppendLine(Comment("/* Horizontal Rule */"));
        builder.AppendLine("""
        hr {
          border: 0;
          border-bottom: 1px solid currentColor;
          height: 0;
          margin: 1rem 0;
        }
        """);
        builder.AppendLine();

        builder.AppendLine(Comment("/* Horizontal Links */"));
        builder.AppendLine("""
        .horizontal-links {
          display: flex;
          flex-wrap: wrap;
          gap: 1.2rem;
          padding: 0;
          list-style: none;
          align-items: center;
        }

        .horizontal-links li {
          margin: 0;
          line-height: 1;
        }

        .horizontal-links a {
          white-space: nowrap;
        }            
        """);
        builder.AppendLine();

        builder.AppendLine(Comment("/* Visually hidden utility */"));
        builder.AppendLine("""
        .visually-hidden {
          position: absolute !important;
          height: 1px;
          width: 1px;
          overflow: hidden;
          clip-path: inset(50%);
          white-space: nowrap;
          border: 0;
          padding: 0;
          margin: 0;
          clip: rect(1px,1px,1px,1px);
        }
        """);
        builder.AppendLine();

        if (_options.UseLayoutColumns)
        {
            GenerateColumns(builder);
            builder.AppendLine();
        }

        GenerateOptions(builder);

        GenerateThemes(builder);

        return builder.ToString();
    }

    private static void AppendBaseColumnStyles(StringBuilder builder, decimal columnGap, int largeBreakpoint)
    {
        builder.AppendLine($".layout-container {{ max-width: {largeBreakpoint}px; }}");
        builder.AppendLine();
        builder.AppendLine("[class^=\"layout-cols\"] {");
        builder.AppendLine("  display: flex;");
        builder.AppendLine("  flex-wrap: wrap;");
        builder.AppendLine($"  gap: {columnGap}rem;");
        builder.AppendLine("}");
        builder.AppendLine();
    }

    private static void AppendLayoutRules(StringBuilder builder, Dictionary<string, int[]> layouts, decimal columnGap)
    {
        foreach (KeyValuePair<string, int[]> layout in layouts)
        {
            string className = layout.Key;
            int[] ratios = layout.Value;
            int total = ratios.Sum();
            int gapCount = ratios.Length - 1;

            for (int i = 0; i < ratios.Length; i++)
            {
                int ratio = ratios[i];
                builder.AppendLine($".{className} > :nth-child({i + 1}) {{ flex: 1 1 calc((100% * {ratio} / {total}) - ({gapCount} * {columnGap}rem * {ratio} / {total})); }}");
            }
        }

        builder.AppendLine();
    }

    private static void AppendMediumBreakpointRules(StringBuilder builder, Dictionary<string, int[]> layouts, decimal columnGap, int mediumBreakpoint)
    {
        builder.AppendLine($"@media (max-width: {mediumBreakpoint}px) {{");

        string[] collapsingAtMedium = ["layout-cols-2-1", "layout-cols-3-1", "layout-cols-1-2", "layout-cols-1-3"];

        foreach (KeyValuePair<string, int[]> layout in layouts)
        {
            string className = layout.Key;
            int count = layout.Value.Length;

            if (className == "layout-cols-1-1-1-1")
            {
                for (int i = 0; i < 4; i++)
                {
                    builder.AppendLine($"  .{className} > :nth-child({i + 1}) {{ flex: 1 1 calc(50% - {columnGap / 2}rem); }}");
                }
            }
            else if (collapsingAtMedium.Contains(className))
            {
                for (int i = 0; i < count; i++)
                {
                    builder.AppendLine($"  .{className} > :nth-child({i + 1}) {{ flex: 0 0 100%; }}");
                }
            }
        }

        builder.AppendLine("}");
        builder.AppendLine();
    }

    private static void AppendSmallBreakpointRules(StringBuilder builder, Dictionary<string, int[]> layouts, int smallBreakpoint)
    {
        builder.AppendLine($"@media (max-width: {smallBreakpoint}px) {{");

        foreach (KeyValuePair<string, int[]> layout in layouts)
        {
            string className = layout.Key;
            if (className == "layout-cols-1")
            {
                continue;
            }

            for (int i = 0; i < layout.Value.Length; i++)
            {
                builder.AppendLine($"  .{className} > :nth-child({i + 1}) {{ flex: 0 0 100%; }}");
            }
        }

        builder.AppendLine("}");
    }

    private void GenerateFonts(StringBuilder builder)
    {
        if (_options.BodyFontStack is not null && _options.BodyFontSelectors is not null)
        {
            string bodySelectors = string.Join(", ", _options.BodyFontSelectors);
            builder.AppendLine($$"""
            {{bodySelectors}} {
              font-family: {{_options.BodyFontStack}};
            }
            """);
            builder.AppendLine();
        }

        if (_options.HeadingFontStack is not null ||
            _options.HeadingFontWeight is not null)
        {
            string headingSelectors = _options.HeadingFontSelectors is not null
                ? string.Join(", ", _options.HeadingFontSelectors)
                : "h1, h2, h3, h4, h5, h6";
            builder.AppendLine($"{headingSelectors} {{");

            if (_options.HeadingFontStack is not null)
            {
                builder.AppendLine($"  font-family: {_options.HeadingFontStack};");
            }

            if (_options.HeadingFontWeight is not null)
            {
                builder.AppendLine($"  font-weight: {_options.HeadingFontWeight};");
            }

            builder.AppendLine("}");
            builder.AppendLine();
        }

        if (_options.ArticleFontStack is not null && _options.ArticleFontSelectors is not null)
        {
            string articleSelectors = string.Join(", ", _options.ArticleFontSelectors);
            builder.AppendLine($$"""
            {{articleSelectors}} {
              font-family: {{_options.ArticleFontStack}};
            }
            """);
            builder.AppendLine();
        }

        if (_options.AsideFontStack is not null && _options.AsideFontSelectors is not null)
        {
            string asideSelectors = string.Join(", ", _options.AsideFontSelectors);
            builder.AppendLine($$"""
            {{asideSelectors}} {
              font-family: {{_options.AsideFontStack}};
            }
            """);
            builder.AppendLine();
        }
    }

    private void GenerateOptions(StringBuilder builder)
    {
        if (_options.UseHeaderMenuButton)
        {
            builder.AppendLine(Comment("/* Classic float to avoid flex jumpiness in some browsers. */"));
            builder.AppendLine("""
            #menu-toggle {
              float: right;
              min-width: 2em;
              text-align: center;
              cursor: pointer;
            }

            #menu-toggle .icon-close {
              display:none;
            }

            #menu-toggle[aria-expanded="true"] .icon-open {
              display:none;
            }

            #menu-toggle[aria-expanded="true"] .icon-close {
              display:inline;
            }            
            
            .masthead::after {
              content: "";
              display: table;
              clear: both;
            }
            """);
            builder.AppendLine();
        }

        if (_options.UseStickyFooter)
        {
            builder.AppendLine(Comment("/* Sticky footer flex trick */"));
            builder.AppendLine("""
            html, body {
              height: 100%
            }
        
            body {
              display: flex;
              flex-direction: column;
              min-height: 100vh
            }
        
            main {
              flex: 1 0 auto
            }
        
            footer {
              flex-shrink: 0;
              position: relative;
            }
            """);
            builder.AppendLine();
        }

        if (_options.UseBumping)
        {
            builder.AppendLine(Comment("/* Bumping utility classes */"));
            builder.AppendLine("""
            .bump-mt-0 { margin-top: 0; }
            .bump-mt-1 { margin-top: 1rem; }
            .bump-mt-2 { margin-top: 2rem; }
            .bump-mt-3 { margin-top: 3rem; }
            
            .bump-mb-0 { margin-bottom: 0; }
            .bump-mb-1 { margin-bottom: 1rem; }
            .bump-mb-2 { margin-bottom: 2rem; }
            .bump-mb-3 { margin-bottom: 3rem; }
            """);
            builder.AppendLine();
        }

        if (_options.UseExternalLinkIcon)
        {
            builder.AppendLine(Comment("/* External links */"));
            builder.AppendLine("""
            a[href^="http://"]::after,
            a[href^="https://"]::after {
              content: '';
              display: inline-block;
              vertical-align: -0.11111em;
              width: 0.8333em;
              height: 0.8333em;
              margin-left: -0.8333em;
              padding-left: 1.0833em;
              background: currentColor;
              mask-image: url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"><path fill="white" d="M10.5 1a.5.5 0 0 0 0 1h2.793L7.146 8.146a.5.5 0 1 0 .708.708L14 2.707V5.5a.5.5 0 0 0 1 0v-4a.5.5 0 0 0-.5-.5h-4z"/><path fill="white" d="M13 8a.5.5 0 0 1 .5.5v4A2.5 2.5 0 0 1 11 15H4A2.5 2.5 0 0 1 1.5 12.5V5A2.5 2.5 0 0 1 4 2.5h4a.5.5 0 0 1 0 1H4A1.5 1.5 0 0 0 2.5 5v7.5A1.5 1.5 0 0 0 4 14h7a1.5 1.5 0 0 0 1.5-1.5v-4a.5.5 0 0 1 .5-.5z"/></svg>');
              mask-size: contain;
              mask-repeat: no-repeat;
              mask-position: right center;
            }        
            """);
            builder.AppendLine();
        }
    }

    private void GenerateThemes(StringBuilder builder)
    {
        StyleTheme? lightTheme = _options.LightTheme;
        StyleTheme? lightContrastTheme = _options.LightContrastTheme;
        StyleTheme? darkTheme = _options.DarkTheme;
        StyleTheme? darkContrastTheme = _options.DarkContrastTheme;

        bool multipleThemes =
            (lightTheme is not null && darkTheme is not null) ||
            (lightTheme is not null && lightContrastTheme is not null);

        // Optionally build the light theme
        if (lightTheme is not null)
        {
            var lightBuilder = new StyleLightThemeBuilder(lightTheme, multipleThemes);
            builder.AppendLine(lightBuilder.Build());
        }

        // Optionally build the dark theme
        if (darkTheme is not null)
        {
            bool darkIsDefaut = lightTheme is null;
            var darkBuilder = new StyleDarkThemeBuilder(darkTheme, multipleThemes, darkIsDefaut);
            builder.AppendLine(darkBuilder.Build());
        }

        // Optionally build the light high-contrast theme
        if (lightContrastTheme is not null)
        {
            var lightContrastBuilder = new StyleLightContrastThemeBuilder(lightContrastTheme);
            builder.AppendLine(lightContrastBuilder.Build());
        }

        // Optionally build the dark high-contrast theme
        if (darkTheme is not null && darkContrastTheme is not null)
        {
            var darkContrastBuilder = new StyleDarkContrastThemeBuilder(darkContrastTheme);
            builder.AppendLine(darkContrastBuilder.Build());
        }
    }

    private void GenerateColumns(StringBuilder builder)
    {
        int largeBreakpoint = _options.LargeBreakpoint;
        int mediumBreakpoint = _options.MediumBreakpoint;
        int smallBreakpoint = _options.SmallBreakpoint;
        decimal columnGap = _options.ColumnGap;

        Dictionary<string, int[]> layouts = new()
        {
            ["layout-cols-1-1-1-1"] = [1, 1, 1, 1],
            ["layout-cols-1-1-1"] = [1, 1, 1],
            ["layout-cols-1-1"] = [1, 1],
            ["layout-cols-2-1"] = [2, 1],
            ["layout-cols-3-1"] = [3, 1],
            ["layout-cols-1-2"] = [1, 2],
            ["layout-cols-1-3"] = [1, 3],
            ["layout-cols-1"] = [1],
        };

        AppendBaseColumnStyles(builder, columnGap, largeBreakpoint);
        AppendLayoutRules(builder, layouts, columnGap);
        AppendMediumBreakpointRules(builder, layouts, columnGap, mediumBreakpoint);
        AppendSmallBreakpointRules(builder, layouts, smallBreakpoint);
    }

    private string Comment(string text, bool newline = false)
    {
        if (!_options.Commentary)
        {
            return string.Empty;
        }

        if (!newline)
        {
            return text;
        }

        return $"{text}\n";
    }
}
