namespace Resume;

using System;
using Common.Extensions;
using Microsoft.Extensions.Hosting;
using Resume.Commands;

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
            CommandParser commandParser = new(host);
            ICommand command = commandParser.Parse(args);

            command.Execute();
        }
        catch (InvalidOptionException ex)
        {
            Console.Error.WriteLine(ex.Message);
            new HelpCommand().Execute();
            Environment.Exit(1);
        }

        Environment.Exit(0);
    }
}
