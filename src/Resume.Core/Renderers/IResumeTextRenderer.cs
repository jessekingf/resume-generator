namespace Resume.Core.Renderers.Markdown;

using Resume.Core.Model;

/// <summary>
/// Provides functionality for rendering text-based resumes.
/// </summary>
public interface IResumeTextRenderer
{
    /// <summary>
    /// Renders the resume.
    /// </summary>
    /// <param name="resume">The resume model.</param>
    /// <returns>The rendered resume.</returns>
    string Render(Resume resume);
}
