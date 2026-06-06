// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Genova.Theme.Styles;

/// <summary>
/// Generates CSS rules for the light high-contrast theme based on the provided <see cref="StyleTheme"/>.
/// Emits contrast-specific overrides that apply when <c>data-contrast="high"</c> is set,
/// and optionally narrows to the light theme using <c>data-theme="light"</c>.
/// </summary>
internal sealed class StyleLightContrastThemeBuilder : StyleThemeBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleLightContrastThemeBuilder"/> class.
    /// </summary>
    /// <param name="theme">The high-contrast light theme definition. Must not be null.</param>
    public StyleLightContrastThemeBuilder(StyleTheme theme)
        : base(theme)
    {
    }

    /// <summary>
    /// Appends all CSS rules associated with the light high-contrast theme.
    /// Includes general <c>data-contrast="high"</c> styles and scoped overrides for <c>data-theme="light"</c>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append CSS output to.</param>
    protected override void AppendSelectors(StringBuilder builder)
    {
        builder.AppendLine("/* LIGHT HIGH-CONTRAST THEME */");

        AppendRule(builder, "html[data-contrast=\"high\"] body", b =>
        {
            AppendIfNotNull(b, ForegroundColor, Theme.Foreground);
            AppendIfNotNull(b, BackgroundColor, Theme.Background);
        });

        if (!string.IsNullOrEmpty(Theme.LinkColor))
        {
            AppendRule(builder, "html[data-contrast=\"high\"] a", b =>
            {
                AppendIfNotNull(b, ForegroundColor, Theme.LinkColor);
            });

            AppendRule(builder, "html[data-contrast=\"high\"][data-theme=\"light\"] a", b =>
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

            AppendRule(builder, "html[data-contrast=\"high\"][data-theme=\"light\"] a:visited", b =>
            {
                AppendIfNotNull(b, ForegroundColor, Theme.VisitedLinkColor);
            });
        }
    }
}
