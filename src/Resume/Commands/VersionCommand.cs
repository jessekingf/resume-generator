namespace Resume.Commands;

using System.Reflection;

/// <summary>
/// Command to display the application version.
/// </summary>
/// <seealso cref="ICommand" />
internal class VersionCommand : ICommand
{
    /// <inheritdoc/>
    public void Execute()
    {
        Version? version = Assembly.GetEntryAssembly()?.GetName()?.Version;
        if (version == null)
        {
            throw new InvalidOperationException("The application version was not found.");
        }

        Console.WriteLine(version.ToString());
    }
}
