// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Genova.Theme.Styles;

/// <summary>
/// Represents an abstract base class for generating CSS rules from a <see cref="StyleTheme"/>.
/// Derived classes are responsible for defining how and where the rules are emitted.
/// </summary>
internal abstract class StyleThemeBuilder
{
    /// <summary>
    /// The CSS property name for the background color.
    /// </summary>
    protected const string BackgroundColor = "background-color";

    /// <summary>
    /// The CSS property name for the border color.
    /// </summary>
    protected const string BorderColor = "border-color";

    /// <summary>
    /// The CSS property name for the foreground color (text color).
    /// </summary>
    protected const string ForegroundColor = "color";

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleThemeBuilder"/> class.
    /// </summary>
    /// <param name="theme">The style theme to emit CSS for. Must not be null.</param>
    protected StyleThemeBuilder(StyleTheme theme)
    {
        ArgumentNullException.ThrowIfNull(theme);
        Theme = theme;
    }

    /// <summary>
    /// Gets the <see cref="StyleTheme"/> instance being rendered.
    /// Subclasses use this to access the theme values.
    /// </summary>
    protected StyleTheme Theme { get; }

    /// <summary>
    /// Builds the CSS rules for the theme and returns them as a string.
    /// </summary>
    /// <returns>The constructed CSS rules as a string.</returns>
    public string Build()
    {
        StringBuilder builder = new();
        AppendSelectors(builder);
        return builder.ToString();
    }

    /// <summary>
    /// Appends a single CSS property if its value is not null.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    /// <param name="property">The CSS property name (e.g. "color").</param>
    /// <param name="value">The value to assign. If null, nothing is emitted.</param>
    protected static void AppendIfNotNull(StringBuilder builder, string property, string? value)
    {
        AppendIfNotNull(builder, false, property, value);
    }

    /// <summary>
    /// Appends a single CSS property if its value is not null.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    /// <param name="indented">A value indicating whether to increase the indentation.</param>
    /// <param name="property">The CSS property name (e.g. "color").</param>
    /// <param name="value">The value to assign. If null, nothing is emitted.</param>
    protected static void AppendIfNotNull(StringBuilder builder, bool indented, string property, string? value)
    {
        if (value is not null)
        {
            string indentation = indented ? "    " : "  ";
            builder.AppendLine($"{indentation}{property}: {value};");
        }
    }

    /// <summary>
    /// Appends a CSS rule block using the given selector and rule content.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    /// <param name="selector">The CSS selector string (e.g. "body").</param>
    /// <param name="declarations">An action that emits individual declarations within the rule block.</param>
    protected static void AppendRule(StringBuilder builder, string selector, Action<StringBuilder> declarations)
    {
        AppendRule(builder, false, selector, declarations);
    }

    /// <summary>
    /// Appends a CSS rule block using the given selector and rule content.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    /// <param name="indented">A value indicating whether to increase the indentation.</param>
    /// <param name="selector">The CSS selector string (e.g. "body").</param>
    /// <param name="declarations">An action that emits individual declarations within the rule block.</param>
    protected static void AppendRule(StringBuilder builder, bool indented, string selector, Action<StringBuilder> declarations)
    {
        string indentation = indented ? "  " : "";
        builder.AppendLine($"{indentation}{selector} {{");
        declarations(builder);
        builder.AppendLine($"{indentation}}}");
    }

    /// <summary>
    /// Appends all theme-specific CSS selectors and rules to the provided builder.
    /// Must be implemented by derived classes to control selector structure and grouping.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append CSS output to.</param>
    protected abstract void AppendSelectors(StringBuilder builder);
}
