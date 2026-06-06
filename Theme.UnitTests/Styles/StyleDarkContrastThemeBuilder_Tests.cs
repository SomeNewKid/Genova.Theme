// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.Theme.Styles;

namespace Genova.Theme.UnitTests.Styles;

public class StyleDarkContrastThemeBuilder_Tests
{
    [Fact]
    public void Constructor_should_throw_ArgumentNullException_when_options_is_null()
    {
        // Act
        Action act = () => _ = new StyleDarkContrastThemeBuilder(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("theme");
    }

    [Fact]
    public void Build_generates_expected_CSS()
    {
        // Arrange
        var theme = new StyleTheme
        {
            Foreground = "#ffffff",
            Background = "#000000",
            LinkColor = "#66b2ff",
            VisitedLinkColor = "#80c1ff",
        };

        var builder = new StyleDarkContrastThemeBuilder(theme);

        string expectedCss = """
                /* DARK HIGH-CONTRAST THEME */
                html[data-contrast="high"][data-theme="dark"] body {
                  color: #ffffff;
                  background-color: #000000;
                }
                """;

        // Act
        string actualCss = builder.Build().Trim();

        // Assert
        Assert.StartsWith(expectedCss, actualCss);
    }
}
