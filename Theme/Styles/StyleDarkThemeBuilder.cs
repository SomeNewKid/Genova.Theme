// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Common.Utilities;

namespace Genova.Theme.Styles;

/// <summary>
/// Generates CSS rules for the dark theme based on the provided <see cref="StyleTheme"/>.
/// Emits both automatic rules using <c>prefers-color-scheme: dark</c> and explicit rules under <c>data-theme="dark"</c>.
/// </summary>
internal sealed class StyleDarkThemeBuilder : StyleThemeBuilder
{
    private readonly bool _multipleThemes;
    private readonly bool _darkIsDefaut;

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleDarkThemeBuilder"/> class.
    /// </summary>
    /// <param name="theme">The dark theme definition to emit CSS for. Must not be null.</param>
    /// <param name="multipleThemes">A flag to indicate whether multiple themes are supported.</param>
    /// <param name="darkIsDefaut">A flag to indicate whether the dark theme is the default theme.</param>
    public StyleDarkThemeBuilder(StyleTheme theme, bool multipleThemes, bool darkIsDefaut)
        : base(theme)
    {
        _multipleThemes = multipleThemes;
        _darkIsDefaut = darkIsDefaut;
    }

    /// <summary>
    /// Appends all CSS rules associated with the dark theme.
    /// This includes <c>@media(prefers-color-scheme: dark)</c> and <c>html[data-theme="dark"]</c> blocks.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append CSS output to.</param>
    protected override void AppendSelectors(StringBuilder builder)
    {
        string background = Theme.Background ?? "#111";
        string codeBack = ColorHelper.ShiftTowardWhite(background, 0.075);
        string codeBorder = ColorHelper.ShiftTowardWhite(background, 0.15);
        string formBack = ColorHelper.ShiftTowardWhite(background, 0.15);
        string formBorder = ColorHelper.ShiftTowardWhite(background, 0.3);
        string formBackDim = ColorHelper.ShiftTowardWhite(background, 0.025);
        string formBorderDim = ColorHelper.ShiftTowardWhite(background, 0.05);

        builder.AppendLine("/* DARK THEME */");

        if (_darkIsDefaut)
        {
            AppendRule(builder, "body", b =>
            {
                AppendIfNotNull(b, ForegroundColor, Theme.Foreground);
                AppendIfNotNull(b, BackgroundColor, Theme.Background);
            });
        }

        if (!_multipleThemes)
        {
            return;
        }

        builder.AppendLine("@media (prefers-color-scheme: dark) {");

        AppendRule(builder, true, "html:not([data-theme]) body", b =>
        {
            AppendIfNotNull(b, true, ForegroundColor, Theme.Foreground);
            AppendIfNotNull(b, true, BackgroundColor, Theme.Background);
        });

        AppendRule(builder, true, "html:not([data-theme]) a", b =>
        {
            AppendIfNotNull(b, true, ForegroundColor, Theme.LinkColor);
        });

        if (!string.IsNullOrEmpty(Theme.VisitedLinkColor))
        {
            AppendRule(builder, true, "html:not([data-theme]) a:visited", b =>
            {
                AppendIfNotNull(b, true, ForegroundColor, Theme.VisitedLinkColor);
            });
        }

        if (!string.IsNullOrEmpty(Theme.Background))
        {
            AppendRule(builder, true, "html:not([data-theme]) :where(code, pre)", b =>
            {
                AppendIfNotNull(b, true, BackgroundColor, codeBack);
                AppendIfNotNull(b, true, BorderColor, codeBorder);
            });

            AppendRule(builder, true, "html:not([data-theme]) :where(input, select, textarea, button, datalist, option)", b =>
            {
                AppendIfNotNull(b, true, BackgroundColor, formBack);
                AppendIfNotNull(b, true, BorderColor, formBorder);
            });

            AppendRule(builder, true, "html:not([data-theme]) :where(input:disabled, select:disabled, textarea:disabled, button:disabled, datalist:disabled, option:disabled)", b =>
            {
                AppendIfNotNull(b, true, BackgroundColor, formBackDim);
                AppendIfNotNull(b, true, BorderColor, formBorderDim);
                AppendIfNotNull(b, "opacity", ".4");
                AppendIfNotNull(b, "cursor", "not-allowed");
            });
        }

        builder.AppendLine("}");

        AppendRule(builder, "html[data-theme=\"dark\"] body,\nhtml[data-theme=\"light\"] .dark-theme", b =>
        {
            AppendIfNotNull(b, ForegroundColor, Theme.Foreground);
            AppendIfNotNull(b, BackgroundColor, Theme.Background);
        });

        AppendRule(builder, "html[data-theme=\"dark\"] a,\nhtml[data-theme=\"light\"] .dark-theme a", b =>
        {
            AppendIfNotNull(b, ForegroundColor, Theme.LinkColor);
        });

        AppendRule(builder, "html[data-theme=\"dark\"] a:visited,\nhtml[data-theme=\"light\"] .dark-theme a:visited", b =>
        {
            AppendIfNotNull(b, ForegroundColor, Theme.VisitedLinkColor);
        });

        if (Theme.Background != null)
        {
            AppendRule(builder, "html[data-theme=\"dark\"] :where(code, pre)", b =>
            {
                AppendIfNotNull(b, BackgroundColor, codeBack);
                AppendIfNotNull(b, BorderColor, codeBorder);
            });

            AppendRule(builder, "html[data-theme=\"dark\"] :where(input, select, textarea, button, datalist, option)", b =>
            {
                AppendIfNotNull(b, BackgroundColor, formBack);
                AppendIfNotNull(b, BorderColor, formBorder);
            });

            AppendRule(builder, "html[data-theme=\"dark\"] :where(input:disabled, select:disabled, textarea:disabled, button:disabled, datalist:disabled, option:disabled)", b =>
            {
                AppendIfNotNull(b, BackgroundColor, formBackDim);
                AppendIfNotNull(b, BorderColor, formBorderDim);
                AppendIfNotNull(b, "opacity", ".4");
                AppendIfNotNull(b, "cursor", "not-allowed");
            });
        }

        builder.AppendLine("@media (prefers-color-scheme: light) {");

        AppendRule(builder, true, "html:not([data-theme]) .dark-theme", b =>
        {
            AppendIfNotNull(b, true, ForegroundColor, Theme.Foreground);
            AppendIfNotNull(b, true, BackgroundColor, Theme.Background);
        });

        AppendRule(builder, true, "html:not([data-theme]) .dark-theme a", b =>
        {
            AppendIfNotNull(b, true, ForegroundColor, Theme.LinkColor);
        });

        if (!string.IsNullOrEmpty(Theme.VisitedLinkColor))
        {
            AppendRule(builder, true, "html:not([data-theme]) .dark-theme a:visited", b =>
            {
                AppendIfNotNull(b, true, ForegroundColor, Theme.VisitedLinkColor);
            });
        }

        builder.AppendLine("}");
    }
}
