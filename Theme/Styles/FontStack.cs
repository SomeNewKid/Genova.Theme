// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Attributes;

namespace Genova.Theme.Styles;

/// <summary>
/// Represents a predefined system font stack for use in CSS.
/// Based on https://modernfontstacks.com/.
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for use by websites.")]
public sealed class FontStack
{
    /// <summary>
    /// System UI fonts are those native to the operating system interface. They are highly legible and
    /// easy to read at small sizes, contains many font weights, and is ideal for UI elements.
    /// </summary>
    public static readonly FontStack SystemUI = new("system-ui, sans-serif");

    /// <summary>
    /// Transitional typefaces are a mix between Old Style and Modern typefaces that was developed during The Enlightenment. One of the most famous examples of a Transitional typeface is Times New Roman, which was developed for the Times of London newspaper.
    /// </summary>
    public static readonly FontStack Transitional = new("Charter, 'Bitstream Charter', 'Sitka Text', Cambria, serif");

    /// <summary>
    /// Old Style typefaces are characterized by diagonal stress, low contrast between thick and thin strokes, and rounded serifs, and were developed in the Renaissance period. One of the most famous examples of an Old Style typeface is Garamond.
    /// </summary>
    public static readonly FontStack OldStyle = new ("'Iowan Old Style', 'Palatino Linotype', 'URW Palladio L', P052, serif");

    /// <summary>
    /// Humanist typefaces are characterized by their organic, calligraphic forms and low contrast between thick and thin strokes. These typefaces are inspired by the handwriting of the Renaissance period and are often considered to be more legible and easier to read than other sans-serif typefaces.
    /// </summary>
    public static readonly FontStack Humanist = new("Seravek, 'Gill Sans Nova', Ubuntu, Calibri, 'DejaVu Sans', source-sans-pro, sans-serif");

    /// <summary>
    /// Geometric Humanist typefaces are characterized by their clean, geometric forms and uniform stroke widths. These typefaces are often considered to be modern and sleek in appearance, and are often used for headlines and other display purposes. Futura is a famous example of this classification.
    /// </summary>
    public static readonly FontStack GeometricHumanist = new ("Avenir, Montserrat, Corbel, 'URW Gothic', source-sans-pro, sans-serif");

    /// <summary>
    /// Classical Humanist typefaces are characterized by how the strokes subtly widen as they reach the stroke terminals without ending in a serif. These typefaces are inspired by classical Roman capitals and the stone-carving on Renaissance-period tombstones.
    /// </summary>
    public static readonly FontStack ClassicalHumanist = new ("Optima, Candara, 'Noto Sans', source-sans-pro, sans-serif");

    /// <summary>
    /// Neo-Grotesque typefaces are a style of sans-serif that was developed in the late 19th and early 20th centuries and is characterized by its clean, geometric forms and uniform stroke widths. One of the most famous examples of a Neo-Grotesque typeface is Helvetica.
    /// </summary>
    public static readonly FontStack NeoGrotesque = new ("Inter, Roboto, 'Helvetica Neue', 'Arial Nova', 'Nimbus Sans', Arial, sans-serif");

    /// <summary>
    /// Monospace Slab Serif typefaces are characterized by their fixed-width letters, which have the same width regardless of their shape, and its simple, geometric forms. Used to emulate typewriter output for reports, tabular work and technical documentation.
    /// </summary>
    public static readonly FontStack MonospaceSlabSerif = new("'Nimbus Mono PS', 'Courier New', monospace");

    /// <summary>
    /// Monospace Code typefaces are specifically designed for use in programming and other technical applications. These typefaces are characterized by their monospaced design, which means that all letters and characters have the same width, and their clear, legible forms.
    /// </summary>
    public static readonly FontStack MonospaceCode = new ("ui-monospace, 'Cascadia Code', 'Source Code Pro', Menlo, Consolas, 'DejaVu Sans Mono', monospace");

    /// <summary>
    /// Industrial typefaces originated in the late 19th century and was heavily influenced by the advancements in technology and industry during that time. Industrial typefaces are characterized by their bold, sans-serif letterforms, simple and straightforward appearance, and the use of straight lines and geometric shapes.
    /// </summary>
    public static readonly FontStack Industrial = new("Bahnschrift, 'DIN Alternate', 'Franklin Gothic Medium', 'Nimbus Sans Narrow', sans-serif-condensed, sans-serif");

    /// <summary>
    /// Rounded typefaces are characterized by the rounded curved letterforms and give a softer, friendlier appearance. The rounded edges give the typeface a more organic and playful feel, making it suitable for use in informal or child-friendly designs. The rounded sans-serif style has been popular since the 1950s, and it continues to be widely used in advertising, branding, and other forms of graphic design.
    /// </summary>
    public static readonly FontStack RoundedSans = new ("ui-rounded, 'Hiragino Maru Gothic ProN', Quicksand, Comfortaa, Manjari, 'Arial Rounded MT', 'Arial Rounded MT Bold', Calibri, source-sans-pro, sans-serif");

    /// <summary>
    /// Slab Serif typefaces are characterized by the presence of thick, block-like serifs on the ends of each letterform. These serifs are usually unbracketed, meaning they do not have any curved or tapered transitions to the main stroke of the letter.
    /// </summary>
    public static readonly FontStack SlabSerif = new ("Rockwell, 'Rockwell Nova', 'Roboto Slab', 'DejaVu Serif', 'Sitka Small', serif");

    /// <summary>
    /// Antique typefaces, also known as Egyptians, are a subset of serif typefaces that were popular in the 19th century. They are characterized by their block-like serifs and thick uniform stroke weight.
    /// </summary>
    public static readonly FontStack Antique = new("Superclarendon, 'Bookman Old Style', 'URW Bookman', 'URW Bookman L', 'Georgia Pro', Georgia, serif");

    /// <summary>
    /// Didone typefaces, also known as Modern typefaces, are characterized by the high contrast between thick and thin strokes, vertical stress, and hairline serifs with no bracketing. The Didone style emerged in the late 18th century and gained popularity during the 19th century.
    /// </summary>
    public static readonly FontStack Didone = new("Didot, 'Bodoni MT', 'Noto Serif Display', 'URW Palladio L', P052, Sylfaen, serif");

    /// <summary>
    /// Handwritten typefaces are designed to mimic the look and feel of handwriting. Despite the vast array of handwriting styles, this font stack tend to adopt a more informal and everyday style of handwriting.
    /// </summary>
    public static readonly FontStack Handwritten = new("'Segoe Print', 'Bradley Hand', Chilanka, TSCu_Comic, casual, cursive");

    private readonly string _value;

    private FontStack(string value) => _value = value;

    /// <inheritdoc/>
    public override string ToString() => _value;
}
