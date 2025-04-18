namespace Resume;

using System;
using System.Reflection;
using Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Resume.Core;
using Resume.Properties;

/// <summary>
/// The entry class of the application.
/// </summary>`
internal class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The application arguments.</param>
    public static void Main(string[] args)
    {
        using IHost host = Startup.CreateHost(args);

        try
        {
            // Parse the command line arguments.
            ProgramOptions options = new ProgramOptions(args);

            // Process the options.
            ProcessOptions(host, options);
        }
        catch (InvalidOptionException ex)
        {
            // Log the error and display the help text.
            Console.Error.WriteLine(ex.Message);
            DisplayHelp();
            Environment.Exit(1);
        }

        Environment.Exit(0);
    }

    /// <summary>
    /// Processes and executes the specified program options.
    /// </summary>
    /// <param name="host">The application host.</param>
    /// <param name="options">The options parsed from the program arguments.</param>
    private static void ProcessOptions(IHost host, ProgramOptions options)
    {
        if (options.DisplayHelp)
        {
            DisplayHelp();
        }
        else if (options.DisplayVersion)
        {
            DisplayVersion();
        }
        else
        {
            GenerateResume(host, options.InputPath, options.OutputPath);
        }
    }

    /// <summary>
    /// Generates resumes from the provided JSON file.
    /// </summary>
    /// <param name="host">The application host.</param>
    /// <param name="resumeJsonPath">The path to the JSON document containing the resume data.</param>
    /// <param name="outputPath">The output path to save the generated resumes to.</param>
    private static void GenerateResume(IHost host, string resumeJsonPath, string outputPath)
    {
        ResumeController resumeController = host.Services.GetRequiredService<ResumeController>();
        resumeController.GenerateResume(resumeJsonPath, outputPath);
    }

    /// <summary>
    /// Displays the help text for this application.
    /// </summary>
    private static void DisplayHelp()
    {
        Console.WriteLine(Resources.ProgramHelp);
    }

    /// <summary>
    /// Gets the current application version.
    /// </summary>
    private static void DisplayVersion()
    {
        Version version = Assembly.GetEntryAssembly().GetName().Version;
        Console.WriteLine(version.ToString());
    }
}
