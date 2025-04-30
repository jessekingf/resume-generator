namespace Resume.Core.Renderers;

using System.Text;
using Resume.Core.Model;

/// <summary>
/// Provides common functionality for rendering text-based resumes.
/// </summary>
public abstract class ResumeTextRenderer : IResumeTextRenderer
{
    private readonly StringBuilder builder = new();

    /// <summary>
    /// Gets the current text for the resume being built.
    /// </summary>
    protected string ResumeText
    {
        get
        {
            return this.builder.ToString();
        }
    }

    /// <inheritdoc/>
    public abstract string Render(Resume resume);

    /// <summary>
    /// Appends a text to the resume without a line-break.
    /// </summary>
    /// <param name="text">The text to add to the resume.</param>
    protected virtual void Append(string? text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            this.builder.Append(text);
        }
    }

    /// <summary>
    /// Appends a line to the resume.
    /// </summary>
    /// <param name="line">The line to add to the resume.</param>
    protected virtual void AppendLine(string? line)
    {
        if (!string.IsNullOrEmpty(line))
        {
            this.builder.AppendLine(line);
        }
    }

    /// <summary>
    /// Appends a blank line to the resume.
    /// </summary>
    protected virtual void AppendLine()
    {
        this.builder.AppendLine();
    }

    /// <summary>
    /// Clears the current resume being built.
    /// </summary>
    protected virtual void Clear()
    {
        this.builder.Clear();
    }

    /// <summary>
    /// Formats the date to a string for display in the resume.
    /// </summary>
    /// <param name="date">The date to format.</param>
    /// <returns>The formatted date.</returns>
    protected virtual string FormatDate(DateTime date)
    {
        return date.ToString("MMM yyyy");
    }

    /// <summary>
    /// Formats the date range for display in the resume.
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date, or null if present.</param>
    /// <returns>The formatted date range.</returns>
    protected virtual string FormatDateRange(DateTime startDate, DateTime? endDate)
    {
        return $"{this.FormatDate(startDate)} – {(endDate.HasValue ? this.FormatDate(endDate.Value) : "Present")}";
    }
}
