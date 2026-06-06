# Genova.Theme

Provides reusable script and stylesheet generation helpers for applying accessible themes, typography, layout utilities, and small progressive-enhancement behaviours to Genova websites.

## Installation

Add a reference to the package, or build the project:

```bash
dotnet build
```

## Usage

```csharp
using Genova.Theme.Scripts;
using Genova.Theme.Styles;

var styleOptions = new StyleOptions
{
    Commentary = true,
    UseHeaderMenuButton = true,
    UseExternalLinkIcon = true,
    UseStickyFooter = true,
    UseBumping = true,

    BodyFontStack = FontStack.SystemUI,
    HeadingFontStack = FontStack.SystemUI,
    HeadingFontWeight = FontWeight.Bold,
    ArticleFontStack = FontStack.Transitional,

    LightTheme = new StyleTheme
    {
        Foreground = "#2f3439",
        Background = "#ebe9e5",
        LinkColor = "#0f4780",
        VisitedLinkColor = "#8a008a",
    },

    DarkTheme = new StyleTheme
    {
        Foreground = "#e8dfd0",
        Background = "#2f3439",
        LinkColor = "#8acaf4",
        VisitedLinkColor = "#ffa3ff",
    },
};

var styleBuilder = new StyleBuilder(styleOptions);

string essentialCss = styleBuilder.BuildEssential();
string stylesheet = styleBuilder.BuildPlain();

var scriptOptions = new ScriptOptions
{
    Commentary = true,
    IncludeHeaderMenuButton = true,
    IncludeThemeSwitcher = true,
};

var scriptBuilder = new ScriptBuilder(scriptOptions);
string script = scriptBuilder.BuildPlain();
```

## Features

* Essential CSS generation for inline critical styles
* Full stylesheet generation for reusable site styling
* Light, dark, light high-contrast, and dark high-contrast theme support
* Configurable theme colors, link colors, font stacks, and heading weights
* Optional layout helpers, sticky footer styles, bumping utilities, external-link icons, and header menu styles
* Optional JavaScript for header menu controls, theme switching, and theme toggling
* CSS condensation helper for compact generated output

## Notes

* Part of the Genova multi-tenant ASP.NET Core platform.
* This is a class library module, not a standalone application.
* The package generates plain CSS and JavaScript strings that can be returned from endpoints, embedded in pages, or written to files by a consuming application.
* `BuildEssential()` is intended for minimal inline CSS, while `BuildPlain()` returns the broader generated stylesheet.

## License

GNU General Public License v3.0
