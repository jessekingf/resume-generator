namespace Resume;

using Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Resume.Commands;
using Resume.Properties;

/// <summary>
/// Parses the command line arguments.
/// </summary>
internal class CommandParser
{
    private readonly IHost host;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandParser"/> class.
    /// </summary>
    /// <param name="host">The application host.</param>
    public CommandParser(IHost host)
    {
        this.host = host;
    }

    /// <summary>
    /// Parses the program options from the command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>The program options.</returns>
    public ICommand Parse(string[] args)
    {
        List<string> nonSwitchArgs = [];

        foreach (string arg in args ?? [])
        {
            if (string.IsNullOrEmpty(arg))
            {
                continue;
            }

            if (arg.StartsWith('-'))
            {
#pragma warning disable CA1308 // Normalize strings to uppercase
                switch (arg.ToLowerInvariant())
                {
                    case "--help":
                    case "-h":
                        return this.host.Services.GetRequiredService<HelpCommand>();
                    case "--version":
                    case "-v":
                        return this.host.Services.GetRequiredService<VersionCommand>();
                    default:
                        throw new InvalidOptionException(string.Format(Resources.ErrorInvalidArgument, arg));
                }
#pragma warning restore CA1308 // Normalize strings to uppercase
            }

            nonSwitchArgs.Add(arg);
        }

        return this.BuildResumeCommand(nonSwitchArgs);
    }

    private ResumeCommand BuildResumeCommand(List<string> args)
    {
        if (args.Count < 2)
        {
            throw new InvalidOptionException(Resources.ErrorInvalidArguments);
        }

        ResumeCommand resumeCommand = this.host.Services.GetRequiredService<ResumeCommand>();
        resumeCommand.ResumeJsonPath = args[0];
        resumeCommand.OutputPath = args[1];

        return resumeCommand;
    }
}
