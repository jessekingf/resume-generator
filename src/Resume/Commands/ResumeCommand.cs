namespace Resume.Commands;

using Common.IO;
using Resume.Core;

/// <summary>
/// The command to generate the resumes.
/// </summary>
/// <seealso cref="ICommand"/>
internal class ResumeCommand : ICommand
{
    private readonly ResumeController resumeController;
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResumeCommand"/> class.
    /// </summary>
    /// <param name="resumeController">The controller for generating resumes.</param>
    /// <param name="fileSystem">Provides access to the file system.</param>
    public ResumeCommand(ResumeController resumeController, IFileSystem fileSystem)
    {
        this.resumeController = resumeController ?? throw new ArgumentNullException(nameof(resumeController));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    /// <summary>
    /// Gets or sets the path to the JSON file containing the resume data.
    /// </summary>
    public required string ResumeJsonPath
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the directory path to output the resume files.
    /// </summary>
    public required string OutputPath
    {
        get;
        set;
    }

    /// <inheritdoc/>
    public void Execute()
    {
        string resumeJsonPath = this.fileSystem.GetFullPath(this.ResumeJsonPath);
        string outputPath = this.fileSystem.GetFullPath(this.OutputPath);
        this.resumeController.GenerateResume(resumeJsonPath, outputPath);
    }
}
