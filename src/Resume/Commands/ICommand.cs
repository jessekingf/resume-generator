namespace Resume.Commands;

/// <summary>
/// A program command.
/// </summary>
internal interface ICommand
{
    /// <summary>
    /// Executes this command.
    /// </summary>
    void Execute();
}
