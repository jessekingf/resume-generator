namespace Resume;

using System;
using System.Collections.Generic;
using System.IO;
using Common.Extensions;
using Common.IO;
using Mono.Options;
using Resume.Properties;

/// <summary>
/// Parses and validates the arguments/options passed into the application.
/// </summary>
internal class ProgramOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProgramOptions"/> class.
    /// </summary>
    /// <param name="args">The program arguments to parse and load.</param>
    /// <exception cref="InvalidOptionException">Thrown when an argument is invalid.</exception>
    public ProgramOptions(string[] args)
        : this(args, new FileSystem())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProgramOptions"/> class.
    /// </summary>
    /// <param name="args">The program arguments to parse and load.</param>
    /// <param name="fileSystem">The file system to use for checking input paths.</param>
    /// <exception cref="InvalidOptionException">Thrown when an argument is invalid.</exception>
    public ProgramOptions(string[] args, IFileSystem fileSystem)
    {
        if (fileSystem == null)
        {
            throw new ArgumentNullException(nameof(fileSystem));
        }

        // If there are no arguments just display the help.
        if (args == null || args.Length == 0)
        {
            this.DisplayHelp = true;
            return;
        }

        // Parse the arguments
        List<string> arguments = this.Parse(args);

        // Don't bother parsing out the command if the help or version flags were set.
        if (this.DisplayHelp || this.DisplayVersion)
        {
            return;
        }

        // If no command was specified just display the help.
        if (arguments.Count == 0)
        {
            this.DisplayHelp = true;
            return;
        }

        if (arguments.Count != 2)
        {
            throw new InvalidOptionException(Resources.ErrorInvalidArguments);
        }

        this.InputPath = Path.GetFullPath(args[0]);
        if (!fileSystem.FileExists(this.InputPath))
        {
            throw new InvalidOptionException(
                string.Format(Resources.ErrorInputPathNotFound, this.InputPath),
                this.InputPath);
        }

        this.OutputPath = Path.GetFullPath(args[1]);
        if (!fileSystem.DirectoryExists(this.OutputPath))
        {
            throw new InvalidOptionException(
                string.Format(Resources.ErrorOutputPathNotFound, this.OutputPath),
                this.OutputPath);
        }
    }

    /// <summary>
    /// Gets the resume data input path.
    /// </summary>
    public string InputPath
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the path to save the generated resumes to.
    /// </summary>
    public string OutputPath
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets a value indicating whether the help is to be displayed.
    /// </summary>
    public bool DisplayHelp
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets a value indicating whether the version is to be displayed.
    /// </summary>
    public bool DisplayVersion
    {
        get;
        private set;
    }

    /// <summary>
    /// Parses the program arguments.
    /// </summary>
    /// <param name="args">The program arguments to parse and load.</param>
    /// <returns>The input argument.</returns>
    private List<string> Parse(string[] args)
    {
        try
        {
            OptionSet optionSet = new OptionSet()
            {
                { "v|version", option => this.DisplayVersion = option != null },
                { "?|h|help", option => this.DisplayHelp = option != null },
            };
            return optionSet.Parse(args);
        }
        catch (OptionException optionException)
            when (!string.IsNullOrEmpty(optionException.OptionName))
        {
            throw new InvalidOptionException(
                string.Format(Resources.ErrorInvalidArgument, optionException.OptionName),
                optionException.OptionName,
                optionException);
        }
        catch (Exception ex)
        {
            throw new InvalidOptionException(Resources.ErrorInvalidArguments, ex);
        }
    }
}
