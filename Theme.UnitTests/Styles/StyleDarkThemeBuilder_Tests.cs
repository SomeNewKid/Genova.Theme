// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.Theme.Styles;

namespace Genova.Theme.UnitTests.Styles;

public class StyleDarkThemeBuilder_Tests
{
    [Fact]
    public void Constructor_should_throw_ArgumentNullException_when_options_is_null()
    {
        // Act
        Action act = () => _ = new StyleDarkThemeBuilder(null!, false, false);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("theme");
    }

    [Fact]
    public void Build_generates_expected_CSS()
    {
        // Arrange
        var theme = new StyleTheme
        {
            Foreground = "#cccccc",
            Background = "#333333",
            LinkColor = "#66b2ff",
            VisitedLinkColor = "#80c1ff",
        };

        var builder = new StyleDarkThemeBuilder(theme, true, false);

        string expectedCss = """
                /* DARK THEME */
                @media (prefers-color-scheme: dark) {
                  html:not([data-theme]) body {
                    color: #cccccc;
                    background-color: #333333;
                  }
                """;

        // Act
        string actualCss = builder.Build().Trim();

        // Assert
        Assert.StartsWith(expectedCss, actualCss);
    }
}
