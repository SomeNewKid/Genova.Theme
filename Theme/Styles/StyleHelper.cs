// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text.RegularExpressions;
using Genova.Common.Attributes;

namespace Genova.Theme.Styles;

/// <summary>
/// Provides helper methods for working with CSS styles.
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for use by modules and websites.")]
public static partial class StyleHelper
{
    private const char OpenBrace = '{';
    private const char CloseBrace = '}';
    private const char SemiColon = ';';
    private const char NewLine = '\n';

    /// <summary>
    /// Condenses a CSS string by removing blank lines, condensing rules to single lines,
    /// removing comments, and formatting <c>@media</c> rules with proper indentation.
    /// </summary>
    /// <param name="css">The CSS string to condense. May be null.</param>
    /// <returns>
    /// The condensed CSS string, or null if <paramref name="css"/> is null.
    /// Returns an empty string if <paramref name="css"/> is whitespace.
    /// Returns the original string if <paramref name="css"/> is empty.
    /// </returns>
    public static string? Condense(string? css)
    {
        if (css is null || css.Length == 0)
        {
            return css;
        }

        if (string.IsNullOrWhiteSpace(css))
        {
            return string.Empty;
        }

        css = RemoveComments(css);
        string[] lines = css.Split(NewLine);
        List<string> result = [];
        List<string> ruleBuffer = [];
        int braceDepth = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].TrimEnd();
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            // Handle @media line
            if (IsMediaOpen(line))
            {
                result.Add(line.Trim());
                braceDepth++;
                continue;
            }

            // Handle selector lines (may span multiple lines)
            if (line.EndsWith(OpenBrace))
            {
                // Combine previous selector lines if present
                if (ruleBuffer.Count > 0)
                {
                    ruleBuffer.Add(line.Trim());
                    string selector = string.Join(" ", ruleBuffer);
                    int braceIndex = selector.IndexOf(OpenBrace);
                    if (braceIndex > 0 && selector[braceIndex - 1] != ' ')
                    {
                        selector = selector.Insert(braceIndex, " ");
                    }

                    ruleBuffer.Clear();
                    ruleBuffer.Add(selector);
                }
                else
                {
                    ruleBuffer.Add(line.Trim());
                }

                braceDepth++;
                continue;
            }

            // If this is a selector line (ends with comma), buffer it for multi-line selector
            if (line.EndsWith(","))
            {
                ruleBuffer.Add(line.Trim());
                continue;
            }

            // Handle closing brace
            if (IsClosingBrace(line))
            {
                // Flush rule buffer if present
                if (ruleBuffer.Count > 0)
                {
                    result.Add(CondenseRuleBuffer(ruleBuffer, braceDepth));
                    ruleBuffer.Clear();
                    braceDepth = Math.Max(0, braceDepth - 1);
                }
                else
                {
                    braceDepth = Math.Max(0, braceDepth - 1);
                    result.Add(new string(' ', braceDepth * 2) + CloseBrace);
                }

                continue;
            }

            // Otherwise, it's a property line
            ruleBuffer.Add(line.Trim());

            // If line ends with '}', flush buffer and adjust depth
            if (line.Trim().EndsWith(CloseBrace))
            {
                result.Add(CondenseRuleBuffer(ruleBuffer, braceDepth));
                ruleBuffer.Clear();
                braceDepth = Math.Max(0, braceDepth - 1);
            }
        }

        // Flush any remaining rule buffer
        if (ruleBuffer.Count > 0)
        {
            result.Add(CondenseRuleBuffer(ruleBuffer, braceDepth));
        }

        // Remove any double blank lines
        return string.Join(NewLine, result.Where(l => !string.IsNullOrWhiteSpace(l)));
    }

    private static string RemoveComments(string css)
        => CommentRegex().Replace(css, string.Empty);

    private static bool IsMediaOpen(string line)
        => line.TrimStart().StartsWith("@media");

    private static bool IsClosingBrace(string line)
        => line.Trim() == CloseBrace.ToString();

    /// <summary>
    /// Helper to condense a rule buffer into a single line, with correct spacing.
    /// </summary>
    private static string CondenseRuleBuffer(List<string> ruleBuffer, int braceDepth)
    {
        if (ruleBuffer.Count == 0)
        {
            return string.Empty;
        }

        string selector = ruleBuffer[0];
        List<string> properties = ruleBuffer.Skip(1)
            .Select(p => p.Trim())
            .Where(p => p.Length > 0)
            .Select(p => p.EndsWith(SemiColon) ? p : p + SemiColon)
            .ToList();

        string joinedProperties = string.Join(" ", properties)
            .Replace($"{SemiColon}  ", $"{SemiColon} ");

        string condensed = selector;
        if (properties.Count > 0)
        {
            condensed += " " + joinedProperties;
        }

        if (!condensed.TrimEnd().EndsWith(CloseBrace))
        {
            condensed += " " + CloseBrace;
        }

        // Indent according to braceDepth (2 spaces per level, but not for top-level)
        int indent = Math.Max(0, (braceDepth - 1) * 2);
        return (indent > 0 ? new string(' ', indent) : "") + condensed.Trim();
    }

    [GeneratedRegex(@"/\*.*?\*/", RegexOptions.Singleline)]
    private static partial Regex CommentRegex();
}
