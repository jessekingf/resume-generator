namespace Resume.Commands;

using System;
using Resume.Properties;

/// <summary>
/// Command to display the application help.
/// </summary>
/// <seealso cref="ICommand" />
internal class HelpCommand : ICommand
{
    /// <inheritdoc/>
    public void Execute()
    {
        Console.WriteLine(Resources.ProgramHelp);
    }
}
