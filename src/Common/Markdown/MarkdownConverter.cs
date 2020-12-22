// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Markdown
{
    using System;
    using Markdig;

    /// <summary>
    /// Provides functionality for converting Markdown into different formats.
    /// </summary>
    public class MarkdownConverter : IMarkdownConverter
    {
        /// <summary>
        /// Converts Markdown to HTML.
        /// </summary>
        /// <param name="markdown">The Markdown to convert to HTML.</param>
        /// <returns>The HTML converted from the Markdown.</returns>
        public string ToHtml(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(markdown));
            }

            MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
                .ConfigureNewLine(Environment.NewLine)
                .Build();

            return Markdown.ToHtml(markdown, pipeline)
                .TrimEnd(Environment.NewLine.ToCharArray());
        }
    }
}
