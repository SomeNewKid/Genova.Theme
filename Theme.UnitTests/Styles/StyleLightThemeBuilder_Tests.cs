// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.Theme.Styles;

namespace Genova.Theme.UnitTests.Styles;

public class StyleLightThemeBuilder_Tests
{
    [Fact]
    public void Constructor_should_throw_ArgumentNullException_when_options_is_null()
    {
        // Act
        Action act = () => _ = new StyleLightThemeBuilder(null!, false);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("theme");
    }

    [Fact]
    public void Build_generates_expected_CSS()
    {
        // Arrange
        var theme = new StyleTheme
        {
            Foreground = "#666666",
            Background = "#e5e5e5",
            LinkColor = "#cc00cc",
            VisitedLinkColor = "#990099",
        };

        var builder = new StyleLightThemeBuilder(theme, false);

        string expectedCss = """
                /* LIGHT THEME */
                body {
                  color: #666666;
                  background-color: #e5e5e5;
                }
                """;

        // Act
        string actualCss = builder.Build().Trim();

        // Assert
        Assert.StartsWith(expectedCss, actualCss);
    }
}
