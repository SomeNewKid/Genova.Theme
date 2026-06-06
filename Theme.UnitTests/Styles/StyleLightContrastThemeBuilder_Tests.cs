// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.Theme.Styles;

namespace Genova.Theme.UnitTests.Styles;

public class StyleLightContrastThemeBuilder_Tests
{
    [Fact]
    public void Constructor_should_throw_ArgumentNullException_when_options_is_null()
    {
        // Act
        Action act = () => _ = new StyleLightContrastThemeBuilder(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("theme");
    }

    [Fact]
    public void Build_generates_expected_CSS()
    {
        // Arrange
        var theme = new StyleTheme
        {
            Foreground = "#000000",
            Background = "#ffffff",
            LinkColor = "#0ff",
            VisitedLinkColor = "#7ff",
        };

        var builder = new StyleLightContrastThemeBuilder(theme);

        string expectedCss = """
                /* LIGHT HIGH-CONTRAST THEME */
                html[data-contrast="high"] body {
                  color: #000000;
                  background-color: #ffffff;
                }
                """;

        // Act
        string actualCss = builder.Build().Trim();

        // Assert
        Assert.StartsWith(expectedCss, actualCss);
    }
}
