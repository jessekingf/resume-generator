namespace Common.Markdown;

/// <summary>
/// Provides functionality for converting Markdown into different formats.
/// </summary>
public interface IMarkdownConverter
{
    /// <summary>
    /// Converts Markdown to HTML.
    /// </summary>
    /// <param name="markdown">The Markdown to convert to HTML.</param>
    /// <returns>The HTML converted from the Markdown.</returns>
    string ToHtml(string markdown);
}
