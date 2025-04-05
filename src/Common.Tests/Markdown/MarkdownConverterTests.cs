namespace Common.Tests.Markdown;

using System;
using Common.Markdown;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// A test fixture for the <see cref="MarkdownConverter"/> class.
/// </summary>
[TestClass]
public class MarkdownConverterTests
{
    /// <summary>
    /// A good case test for the ToHtml method.
    /// </summary>
    [TestMethod]
    public void ToHTml_ValidMarkdown_Success()
    {
        //// TODO: Setup the test to cover all markdown syntax features.

        // Setup the test.
        string markdown =
@"# Heading1
            
A paragraph with **bold** and *italic* text.";

        string expectedHtml =
@"<h1>Heading1</h1>
<p>A paragraph with <strong>bold</strong> and <em>italic</em> text.</p>";

        // Run the test.
        MarkdownConverter target = new MarkdownConverter();
        string result = target.ToHtml(markdown);

        // Validate the results.
        Assert.AreEqual(expectedHtml, result);
    }

    /// <summary>
    /// A test for the ToHtml method with a null markdown parameter.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ToHTml_NullMarkdown_Throws()
    {
        // Setup the test.
        string markdown = null;

        try
        {
            // Run the test.
            MarkdownConverter target = new MarkdownConverter();
            target.ToHtml(markdown);
        }
        catch (ArgumentException ex)
        {
            // Validate the results.
            Assert.AreEqual("markdown", ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A test for the ToHtml method with an empty markdown parameter.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ToHTml_EmptyMarkdown_Throws()
    {
        // Setup the test.
        string markdown = string.Empty;

        try
        {
            // Run the test.
            MarkdownConverter target = new MarkdownConverter();
            target.ToHtml(markdown);
        }
        catch (ArgumentException ex)
        {
            // Validate the results.
            Assert.AreEqual("markdown", ex.ParamName);
            throw;
        }
    }
}
