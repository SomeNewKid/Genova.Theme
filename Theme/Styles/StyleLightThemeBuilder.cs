// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Common.Utilities;

namespace Genova.Theme.Styles;

/// <summary>
/// Generates CSS rules for the light theme based on the provided <see cref="StyleTheme"/>.
/// Emits default styles for the page, and styles under the <c>html[data-theme="light"]</c> attribute selector.
/// </summary>
internal sealed class StyleLightThemeBuilder : StyleThemeBuilder
{
    private readonly bool _multipleThemes;

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleLightThemeBuilder"/> class.
    /// </summary>
    /// <param name="theme">The light theme definition to emit CSS for. Must not be null.</param>
    /// <param name="multipleThemes">A flag to indicate whether multiple themes are supported.</param>
    public StyleLightThemeBuilder(StyleTheme theme, bool multipleThemes)
        : base(theme)
    {
        _multipleThemes = multipleThemes;
    }

    /// <summary>
    /// Appends all CSS rules associated with the light theme.
    /// This includes the base <c>body</c> styles, and selectors scoped under <c>data-theme="light"</c>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append CSS output to.</param>
    protected override void AppendSelectors(StringBuilder builder)
    {
        string background = Theme.Background ?? "#eee";
        string codeBack = ColorHelper.ShiftTowardBlack(background, 0.05);
        string codeBorder = ColorHelper.ShiftTowardBlack(background, 0.1);
        string formBack = ColorHelper.ShiftTowardBlack(background, 0.075);
        string formBorder = ColorHelper.ShiftTowardBlack(background, 0.15);
        string formBackDim = ColorHelper.ShiftTowardBlack(background, 0.025);
        string formBorderDim = ColorHelper.ShiftTowardBlack(background, 0.05);

        builder.AppendLine("/* LIGHT THEME */");

        AppendRule(builder, "body", b =>
        {
            AppendIfNotNull(b, ForegroundColor, Theme.Foreground);
            AppendIfNotNull(b, BackgroundColor, Theme.Background);
        });

        if (!_multipleThemes)
        {
            return;
        }

        AppendRule(builder, "html[data-theme=\"light\"] body,\nhtml[data-theme=\"dark\"] .light-theme", b =>
        {
            AppendIfNotNull(b, ForegroundColor, Theme.Foreground);
            AppendIfNotNull(b, BackgroundColor, Theme.Background);
        });

        AppendRule(builder, "html[data-theme=\"light\"] a,\nhtml[data-theme=\"dark\"] .light-theme a", b =>
        {
            AppendIfNotNull(b, ForegroundColor, Theme.LinkColor);
        });

        if (!string.IsNullOrEmpty(Theme.VisitedLinkColor))
        {
            AppendRule(builder, "html[data-theme=\"light\"] a:visited,\nhtml[data-theme=\"dark\"] .light-theme a:visited", b =>
            {
                AppendIfNotNull(b, ForegroundColor, Theme.VisitedLinkColor);
            });
        }

        if (!string.IsNullOrEmpty(Theme.Background))
        {
            AppendRule(builder, ":where(code, pre),\nhtml[data-theme=\"light\"] :where(code, pre)", b =>
            {
                AppendIfNotNull(b, BackgroundColor, codeBack);
                AppendIfNotNull(b, BorderColor, codeBorder);
            });

            AppendRule(builder, ":where(input, select, textarea, button, datalist, option),\nhtml[data-theme=\"light\"] :where(input, select, textarea, button, datalist, option)", b =>
            {
                AppendIfNotNull(b, BackgroundColor, formBack);
                AppendIfNotNull(b, BorderColor, formBorder);
            });

            AppendRule(builder, ":where(input:disabled, select:disabled, textarea:disabled, button:disabled, datalist:disabled, option:disabled),\nhtml[data-theme=\"light\"] :where(input:disabled, select:disabled, textarea:disabled, button:disabled, datalist:disabled, option:disabled)", b =>
            {
                AppendIfNotNull(b, BackgroundColor, formBackDim);
                AppendIfNotNull(b, BorderColor, formBorderDim);
                AppendIfNotNull(b, "opacity", ".5");
                AppendIfNotNull(b, "cursor", "not-allowed");
            });
        }

        builder.AppendLine("@media (prefers-color-scheme: dark) {");

        AppendRule(builder, true, "html:not([data-theme]) .light-theme", b =>
        {
            AppendIfNotNull(b, true, ForegroundColor, Theme.Foreground);
            AppendIfNotNull(b, true, BackgroundColor, Theme.Background);
        });

        AppendRule(builder, true, "html:not([data-theme]) .light-theme a", b =>
        {
            AppendIfNotNull(b, true, ForegroundColor, Theme.LinkColor);
        });

        if (!string.IsNullOrEmpty(Theme.VisitedLinkColor))
        {
            AppendRule(builder, true, "html:not([data-theme]) .light-theme a:visited", b =>
            {
                AppendIfNotNull(b, true, ForegroundColor, Theme.VisitedLinkColor);
            });
        }

        builder.AppendLine("}");
    }
}
