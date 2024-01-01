// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Resume.Core;

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Common.Extensions;
using Common.IO;
using Common.Markdown;
using Common.PDF;
using Common.Serialization;
using Common.Text;
using Resume.Core.Model;

/// <summary>
/// Handles generating and converting resumes.
/// </summary>
public class ResumeController
{
    /// <summary>
    /// The filename of the default markdown template.
    /// </summary>
    private const string DefaultMarkdownTemplateName = "ResumeMarkdownTemplate.liquid";

    /// <summary>
    /// The filename of the default HTML template.
    /// </summary>
    private const string DefaultHtmlTemplateName = "ResumeHtmlTemplate.html";

    /// <summary>
    /// The default CSS file to use for generating the resume.
    /// </summary>
    private const string DefaultCssFileName = "Resume.css";

    /// <summary>
    /// The serializer to load the resume data with.
    /// </summary>
    private readonly ISerializer serializer;

    /// <summary>
    /// Handles converting Markdown.
    /// </summary>
    private readonly IMarkdownConverter markdownConverter;

    /// <summary>
    /// PDF document generator.
    /// </summary>
    private readonly IPdfGenerator pdfGenerator;

    /// <summary>
    /// Used to read and save documents from the file system.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResumeController"/> class.
    /// </summary>
    public ResumeController()
        : this(new JsonSerializer(), new MarkdownConverter(), new ChromiumPdfGenerator(), new FileSystem())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResumeController"/> class.
    /// </summary>
    /// <param name="serializer">The serializer to load the resume data with.</param>
    /// <param name="markdownConverter">Handles converting Markdown.</param>
    /// <param name="pdfGenerator">PDF document generator.</param>
    /// <param name="fileSystem">Used to read and save documents from the file system.</param>
    public ResumeController(
        ISerializer serializer,
        IMarkdownConverter markdownConverter,
        IPdfGenerator pdfGenerator,
        IFileSystem fileSystem)
    {
        this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        this.markdownConverter = markdownConverter ?? throw new ArgumentNullException(nameof(markdownConverter));
        this.pdfGenerator = pdfGenerator ?? throw new ArgumentNullException(nameof(pdfGenerator));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    /// <summary>
    /// Generates resumes from the provided JSON file.
    /// </summary>
    /// <param name="resumeJsonPath">The path to the JSON document containing the resume data.</param>
    /// <param name="outputPath">The output path to save the generated resumes to.</param>
    /// <remarks>Overwrites the output if the files already exist.</remarks>
    public void GenerateResume(string resumeJsonPath, string outputPath)
    {
        // The resume template is not provided; so create and use the default template.
        string templateText = Assembly.GetExecutingAssembly().ReadResourceFile(DefaultMarkdownTemplateName);
        LiquidTemplate template = new(templateText);

        // Register the model types with the template.
        template.RegisterType(typeof(Resume));
        template.RegisterType(typeof(EducationProgram));
        template.RegisterType(typeof(Job));
        template.RegisterType(typeof(Skill));
        template.RegisterType(typeof(Address));

        // Generate the resume with the default template.
        this.GenerateResume(resumeJsonPath, outputPath, template);
    }

    /// <summary>
    /// Generates resumes from the provided JSON file.
    /// </summary>
    /// <param name="resumeJsonPath">The path to the JSON document containing the resume data.</param>
    /// <param name="outputPath">The output path to save the generated resumes to.</param>
    /// <param name="markdownTemplate">The template to use to render the markdown resume.</param>
    /// <remarks>Overwrites the output if the files already exist.</remarks>
    public void GenerateResume(string resumeJsonPath, string outputPath, ITemplate markdownTemplate)
    {
        if (string.IsNullOrEmpty(resumeJsonPath))
        {
            throw new ArgumentException("Path cannot be null or empty", nameof(resumeJsonPath));
        }

        if (string.IsNullOrEmpty(outputPath))
        {
            throw new ArgumentException("Path cannot be null or empty", nameof(outputPath));
        }

        if (markdownTemplate == null)
        {
            throw new ArgumentNullException(nameof(markdownTemplate));
        }

        if (!this.fileSystem.FileExists(resumeJsonPath))
        {
            throw new FileNotFoundException("The resume JSON file does not exist.", resumeJsonPath);
        }

        if (!this.fileSystem.DirectoryExists(outputPath))
        {
            throw new DirectoryNotFoundException($"The output directory does not exist: {outputPath}");
        }

        // Load the resume data from the JSON file.
        string fileName = Path.GetFileNameWithoutExtension(resumeJsonPath);
        Resume resume = this.serializer.Deserialize<Resume>(this.fileSystem.ReadAllText(resumeJsonPath));

        // Generate the markdown resume.
        string markdownPath = Path.Combine(outputPath, $"{fileName}.md");
        string markdown = this.GenerateMarkDownResume(resume, markdownTemplate);
        this.fileSystem.WriteAllText(markdownPath, markdown);

        // Generate the HTML resume from the markdown resume.
        string htmlPath = Path.Combine(outputPath, $"{fileName}.html");
        this.GenerateHtmlResume(markdown, resume.Name, htmlPath);

        // Generate the PDF resume from the HTML resume.
        string pdfPath = Path.Combine(outputPath, $"{fileName}.pdf");
        this.pdfGenerator.FromHtml(htmlPath, pdfPath);
    }

    /// <summary>
    /// Generates a markdown resume.
    /// </summary>
    /// <param name="resume">The resume data to generate the markdown with.</param>
    /// <param name="markdownTemplate">The template to use to render the markdown resume.</param>
    /// <returns>The generated markdown syntax.</returns>
    private string GenerateMarkDownResume(Resume resume, ITemplate markdownTemplate)
    {
        TemplateContext templateContext = new();
        templateContext.SetValue("r", resume);

        return markdownTemplate.Render(templateContext);
    }

    /// <summary>
    /// Generates an HTML document from markdown.
    /// </summary>
    /// <param name="markdown">The markdown to generate the HTML from.</param>
    /// <param name="title">The HTML title to use in the generated document.</param>
    /// <param name="path">The path to save the HTML file.</param>
    /// <param name="externalCss">True to have the CSS in an external document.</param>
    private void GenerateHtmlResume(string markdown, string title, string path, bool externalCss = false)
    {
        // Load the HTML template.
        Assembly assembly = Assembly.GetExecutingAssembly();
        string templateText = assembly.ReadResourceFile(DefaultHtmlTemplateName);
        string cssContent = assembly.ReadResourceFile(DefaultCssFileName);

        // Convert the markdown to HTML.
        string htmlContent = this.markdownConverter.ToHtml(markdown);

        // Set the styles.
        string styleElement;
        if (externalCss)
        {
            // Save the HTML file with the default standalone CSS file.
            styleElement = $"<link rel=\"stylesheet\" href=\"{DefaultCssFileName}\" />";
            this.fileSystem.WriteAllText(
                Path.Combine(Path.GetDirectoryName(path), DefaultCssFileName),
                cssContent);
        }
        else
        {
            // Embed the styles in the HTML.
            string styles = cssContent.TrimEnd(Environment.NewLine.ToCharArray());
            styleElement = $"<style>{Environment.NewLine}{styles}{Environment.NewLine}</style>";
        }

        // Save the final HTML file.
        string html = string.Format(CultureInfo.InvariantCulture, templateText, title, styleElement, htmlContent);
        this.fileSystem.WriteAllText(path, html);
    }
}
