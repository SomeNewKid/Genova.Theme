// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Common.Attributes;

namespace Genova.Theme.Styles;

/// <summary>
/// Wraps a style stream, allowing variable substitution for CSS-like syntax.
/// Reads the source stream, replaces variables, and exposes the result as a readable stream.
/// </summary>
// <notes>
// Example usage:
//     $background: #333;
//     $foreground: #ccc;
//
//     body {
//       color: $foreground;
//       background-color: $background;
//     }
//     a {
//       color: $LightTheme.LinkColor;
//     }
// </notes>
[CodeQuality(Public = true, Unsealed = true, Justification = "Intended for use by websites.")]
public class StyleStreamWrapper : Stream
{
    private readonly MemoryStream _innerStream;

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleStreamWrapper"/> class.
    /// Processes the input stream, substitutes variables, and stores the result in a memory stream.
    /// </summary>
    /// <param name="sourceStream">The source stream containing style data.</param>
    /// <param name="styleOptions">The options to use when building CSS style sheets.</param>
    public StyleStreamWrapper(Stream sourceStream, StyleOptions? styleOptions)
    {
        Dictionary<string, string> variables = new ()
        {
            ["LightTheme.ForegroundColor"] = styleOptions?.LightTheme?.Foreground ?? string.Empty,
            ["LightTheme.BackgroundColor"] = styleOptions?.LightTheme?.Background ?? string.Empty,
            ["LightTheme.LinkColor"] = styleOptions?.LightTheme?.LinkColor ?? string.Empty,
            ["LightTheme.VisitedLinkColor"] = styleOptions?.LightTheme?.VisitedLinkColor ?? string.Empty,
            ["DarkTheme.ForegroundColor"] = styleOptions?.DarkTheme?.Foreground ?? string.Empty,
            ["DarkTheme.BackgroundColor"] = styleOptions?.DarkTheme?.Background ?? string.Empty,
            ["DarkTheme.LinkColor"] = styleOptions?.DarkTheme?.LinkColor ?? string.Empty,
            ["DarkTheme.VisitedLinkColor"] = styleOptions?.DarkTheme?.VisitedLinkColor ?? string.Empty,
            ["LargeBreakpoint"] = styleOptions?.LargeBreakpoint.ToString() ?? "1140",
            ["MediumBreakpoint"] = styleOptions?.MediumBreakpoint.ToString() ?? "900",
            ["SmallBreakpoint"] = styleOptions?.SmallBreakpoint.ToString() ?? "600",
        };

        StringBuilder output = new ();

        using (StreamReader reader = new (sourceStream))
        {
            string? rawLine;
            while ((rawLine = reader.ReadLine()) != null)
            {
                string trimmedLine = rawLine.TrimStart();
                if (trimmedLine.StartsWith('$'))
                {
                    int index = trimmedLine.IndexOf(':');
                    if (index > 1 && index < trimmedLine.Length - 1)
                    {
                        string key = trimmedLine.Substring(1, index - 1).Trim();
                        string value = trimmedLine.Substring(index + 1).Trim().TrimEnd(';');
                        variables[key] = value;
                    }

                    continue;
                }

                string existingLine = rawLine;
                foreach (KeyValuePair<string, string> kvp in variables)
                {
                    existingLine = existingLine.Replace($"${kvp.Key}", kvp.Value);
                }

                output.AppendLine(existingLine);
            }
        }

        // Convert result to bytes and wrap in memory stream
        byte[] bytes = Encoding.UTF8.GetBytes(output.ToString());
        _innerStream = new MemoryStream(bytes);
    }

    /// <inheritdoc/>
    public override bool CanRead => _innerStream.CanRead;

    /// <inheritdoc/>
    public override bool CanSeek => _innerStream.CanSeek;

    /// <inheritdoc/>
    public override bool CanWrite => false;

    /// <inheritdoc/>
    public override long Length => _innerStream.Length;

    /// <inheritdoc/>
    public override long Position
    {
        get => _innerStream.Position;
        set => _innerStream.Position = value;
    }

    /// <summary>
    /// Flushes the underlying memory stream.
    /// </summary>
    public override void Flush() => _innerStream.Flush();

    /// <summary>
    /// Reads a sequence of bytes from the underlying memory stream and advances the position.
    /// </summary>
    /// <param name="buffer">The buffer to read data into.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>The total number of bytes read into the buffer.</returns>
    public override int Read(byte[] buffer, int offset, int count) => _innerStream.Read(buffer, offset, count);

    /// <summary>
    /// Sets the position within the underlying memory stream.
    /// </summary>
    /// <param name="offset">A byte offset relative to the origin parameter.</param>
    /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
    /// <returns>The new position within the stream.</returns>
    public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);

    /// <summary>
    /// Sets the length of the underlying memory stream.
    /// </summary>
    /// <param name="value">The desired length of the stream in bytes.</param>
    public override void SetLength(long value) => _innerStream.SetLength(value);

    /// <summary>
    /// Writing is not supported for <see cref="StyleStreamWrapper"/>.
    /// </summary>
    /// <param name="buffer">The buffer containing data to write.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes.</param>
    /// <param name="count">The number of bytes to write.</param>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="StyleStreamWrapper"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _innerStream.Dispose();
        }

        base.Dispose(disposing);
    }
}
