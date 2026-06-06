// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.Theme.Styles;

namespace Genova.Theme.UnitTests.Styles;

public class StyleBuilder_Tests
{
    [Fact]
    public void Constructor_should_throw_ArgumentNullException_when_options_is_null()
    {
        // Act
        Action act = () => _ = new StyleBuilder(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("options");
    }

    [Fact]
    public void BuildEssential_includes_essentials()
    {
        // Arrange
        StyleOptions styleOptions = new() { Commentary = true };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildEssential();

        // Assert
        css.Should().Contain("/* =====================  TYPOGRAPHY  ===================== */");
        css.Should().Contain("/* =====================  FLUID CONTENT  ===================== */");
        css.Should().Contain("/* =====================  WIDTH CONTAINER  ===================== */");
        css.Should().Contain("/* =====================  SKIP LINKS  ===================== */");
        css.Should().Contain("/* =====================  GLOBAL LINK STYLE  ===================== */");
        css.Should().Contain("/* =====================  MISCELLANEOUS  ===================== */");
    }

    [Fact]
    public void BuildPlain_includes_fundamentals()
    {
        // Arrange
        StyleOptions styleOptions = new() { Commentary = true };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("/* Headings */");
        css.Should().Contain("/* Forms */");
        css.Should().Contain("/* Code */");
        css.Should().Contain("/* Horizontal Rule */");
        css.Should().Contain("/* Horizontal Links */");
        css.Should().Contain("/* Visually hidden utility */");
    }

    [Fact]
    public void BuildPlain_without_columns()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseLayoutColumns = false
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().NotContain("[class^=\"layout-cols\"]");
        css.Should().NotContain("layout-cols-1-1-1-1");
    }

    [Fact]
    public void BuildPlain_with_columns()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseLayoutColumns = true,
            ColumnGap = 1.5m,
            MediumBreakpoint = 800,
            SmallBreakpoint = 500
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("[class^=\"layout-cols\"]");
        css.Should().Contain("layout-cols-1-1-1-1");
    }

    [Fact]
    public void BuildPlain_without_header_menu_button()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseHeaderMenuButton = false
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().NotContain("#menu-toggle");
    }

    [Fact]
    public void BuildPlain_with_header_menu_button()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseHeaderMenuButton = true
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("#menu-toggle");
    }

    [Fact]
    public void BuildPlain_without_sticky_footer()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseStickyFooter = false
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().NotContain("/* Sticky footer flex trick */");
    }

    [Fact]
    public void BuildPlain_with_sticky_footer()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseStickyFooter = true,
            Commentary = true,
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("/* Sticky footer flex trick */");
    }

    [Fact]
    public void BuildPlain_without_bumping()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseBumping = false
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().NotContain("/* Bumping utility classes */");
    }

    [Fact]
    public void BuildPlain_with_bumping()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseBumping = true,
            Commentary = true,
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("/* Bumping utility classes */");
    }

    [Fact]
    public void BuildPlain_without_external_link_icons()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseExternalLinkIcon = false
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().NotContain("/* External links */");
    }

    [Fact]
    public void BuildPlain_with_external_link_icons()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            UseExternalLinkIcon = true,
            Commentary = true,
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("/* External links */");
    }

    [Fact]
    public void BuildPlain_with_BodyFontStack()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            BodyFontStack = FontStack.Industrial
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain($"font-family: {FontStack.Industrial}");
    }

    [Fact]
    public void BuildPlain_with_HeadingFontStack()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            HeadingFontStack = FontStack.Antique,
            HeadingFontWeight = FontWeight.Bold
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain($"font-family: {FontStack.Antique}");
        css.Should().Contain($"font-weight: {FontWeight.Bold}");
    }

    [Fact]
    public void BuildPlain_with_ArticleFontStack()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            ArticleFontStack = FontStack.NeoGrotesque
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain($"font-family: {FontStack.NeoGrotesque}");
    }

    [Fact]
    public void BuildPlain_with_AsideFontStack()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            AsideFontStack = FontStack.ClassicalHumanist
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain($"font-family: {FontStack.ClassicalHumanist}");
    }

    [Fact]
    public void BuildPlain_only_light_theme()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            LightTheme = new StyleTheme(),
            Commentary = true,
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("/* LIGHT THEME */");
        css.Should().NotContain("/* LIGHT HIGH-CONTRAST THEME */");
        css.Should().NotContain("/* DARK THEME */");
        css.Should().NotContain("/* DARK HIGH-CONTRAST THEME */");
    }

    [Fact]
    public void BuildPlain_only_dark_theme()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            DarkTheme = new StyleTheme(),
            Commentary = true,
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().NotContain("/* LIGHT THEME */");
        css.Should().NotContain("/* LIGHT HIGH-CONTRAST THEME */");
        css.Should().Contain("/* DARK THEME */");
        css.Should().NotContain("/* DARK HIGH-CONTRAST THEME */");
    }

    [Fact]
    public void BuildPlain_only_light_and_dark_themes()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            LightTheme = new StyleTheme(),
            DarkTheme = new StyleTheme(),
            Commentary = true,
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("/* LIGHT THEME */");
        css.Should().NotContain("/* LIGHT HIGH-CONTRAST THEME */");
        css.Should().Contain("/* DARK THEME */");
        css.Should().NotContain("/* DARK HIGH-CONTRAST THEME */");
    }

    [Fact]
    public void BuildPlain_only_light_and_light_high_contrast_themes()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            LightTheme = new StyleTheme(),
            LightContrastTheme = new StyleTheme(),
            Commentary = true,
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("/* LIGHT THEME */");
        css.Should().Contain("/* LIGHT HIGH-CONTRAST THEME */");
        css.Should().NotContain("/* DARK THEME */");
        css.Should().NotContain("/* DARK HIGH-CONTRAST THEME */");
    }

    [Fact]
    public void BuildPlain_only_light_and_dark_and_light_high_contrast_themes()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            LightTheme = new StyleTheme(),
            LightContrastTheme = new StyleTheme(),
            DarkTheme = new StyleTheme(),
            Commentary = true,
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("/* LIGHT THEME */");
        css.Should().Contain("/* LIGHT HIGH-CONTRAST THEME */");
        css.Should().Contain("/* DARK THEME */");
        css.Should().NotContain("/* DARK HIGH-CONTRAST THEME */");
    }

    [Fact]
    public void BuildPlain_all_themes()
    {
        // Arrange
        StyleOptions styleOptions = new()
        {
            LightTheme = new StyleTheme(),
            LightContrastTheme = new StyleTheme(),
            DarkTheme = new StyleTheme(),
            DarkContrastTheme = new StyleTheme(),
            Commentary = true,
        };
        StyleBuilder styleBuilder = new(styleOptions);

        // Act
        string css = styleBuilder.BuildPlain();

        // Assert
        css.Should().Contain("/* LIGHT THEME */");
        css.Should().Contain("/* LIGHT HIGH-CONTRAST THEME */");
        css.Should().Contain("/* DARK THEME */");
        css.Should().Contain("/* DARK HIGH-CONTRAST THEME */");
    }
}
