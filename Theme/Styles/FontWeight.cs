// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Attributes;

namespace Genova.Theme.Styles;

/// <summary>
/// Represents a typographic font weight for CSS output.
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for use by websites.")]
public sealed class FontWeight
{
    /// <summary>
    /// A light font weight, typically mapped to 200.
    /// </summary>
    public static readonly FontWeight Light = new("200");

    /// <summary>
    /// The normal font weight, typically mapped to 400.
    /// </summary>
    public static readonly FontWeight Normal = new("normal");

    /// <summary>
    /// A bold font weight, typically mapped to 700.
    /// </summary>
    public static readonly FontWeight Bold = new("bold");

    /// <summary>
    /// A heavy font weight, typically mapped to 900.
    /// </summary>
    public static readonly FontWeight Heavy = new("900");

    private readonly string _value;

    private FontWeight(string value) => _value = value;

    /// <summary>
    /// Returns the CSS-compatible string representation of the font weight.
    /// </summary>
    /// <returns>A string such as "normal", "bold", or "200".</returns>
    public override string ToString() => _value;
}
