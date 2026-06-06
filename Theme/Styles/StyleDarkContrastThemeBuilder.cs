// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Genova.Theme.Styles;

/// <summary>
/// Generates CSS rules for the dark high-contrast theme based on the provided <see cref="StyleTheme"/>.
/// Includes both explicit theme overrides and system-preference fallbacks for dark mode + high contrast.
/// </summary>
internal sealed class StyleDarkContrastThemeBuilder : StyleThemeBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleDarkContrastThemeBuilder"/> class.
    /// </summary>
    /// <param name="theme">The high-contrast dark theme definition. Must not be null.</param>
    public StyleDarkContrastThemeBuilder(StyleTheme theme)
        : base(theme)
    {
    }

    /// <summary>
    /// Appends all CSS rules associated with the dark high-contrast theme.
    /// Includes <c>data-theme="dark"</c> + <c>data-contrast="high"</c>, and
    /// <c>@media(prefers-color-scheme: dark)</c> fallbacks when no theme override is active.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append CSS output to.</param>
    protected override void AppendSelectors(StringBuilder builder)
    {
        builder.AppendLine("/* DARK HIGH-CONTRAST THEME */");

        AppendRule(builder, "html[data-contrast=\"high\"][data-theme=\"dark\"] body", b =>
        {
            AppendIfNotNull(b, ForegroundColor, Theme.Foreground);
            AppendIfNotNull(b, BackgroundColor, Theme.Background);
        });

        builder.AppendLine("@media (prefers-color-scheme: dark) {");

        AppendRule(builder, true, "html[data-contrast=\"high\"]:not([data-theme]) body", b =>
        {
            AppendIfNotNull(b, true, BackgroundColor, Theme.Background);
            AppendIfNotNull(b, true, ForegroundColor, Theme.Foreground);
        });

        if (!string.IsNullOrEmpty(Theme.LinkColor))
        {
            AppendRule(builder, "html[data-contrast=\"high\"] a", b =>
            {
                AppendIfNotNull(b, ForegroundColor, Theme.LinkColor);
            });

            AppendRule(builder, "html[data-contrast=\"high\"][data-theme=\"dark\"] a", b =>
            {
                AppendIfNotNull(b, ForegroundColor, Theme.LinkColor);
            });
        }

        if (!string.IsNullOrEmpty(Theme.VisitedLinkColor))
        {
            AppendRule(builder, "html[data-contrast=\"high\"] a:visited", b =>
            {
                AppendIfNotNull(b, ForegroundColor, Theme.VisitedLinkColor);
            });

            AppendRule(builder, "html[data-contrast=\"high\"][data-theme=\"dark\"] a:visited", b =>
            {
                AppendIfNotNull(b, ForegroundColor, Theme.VisitedLinkColor);
            });
        }

        builder.AppendLine("}");
    }
}
