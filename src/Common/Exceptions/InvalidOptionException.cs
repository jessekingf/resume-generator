namespace Common.Extensions;

using System;

/// <summary>
/// An exception thrown when an invalid program option was specified.
/// </summary>
/// <seealso cref="System.Exception" />
public class InvalidOptionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidOptionException"/> class.
    /// </summary>
    public InvalidOptionException()
        : this("An invalid program option was specified.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidOptionException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidOptionException(string message)
        : this(message, null, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidOptionException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The inner exception.</param>
    public InvalidOptionException(string message, Exception innerException)
        : this(message, null, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidOptionException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="optionName">The name of the invalid program option that caused the exception.</param>
    public InvalidOptionException(string message, string optionName)
        : this(message, optionName, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidOptionException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="optionName">The name of the invalid program option that caused the exception.</param>
    /// <param name="innerException">The inner exception.</param>
    public InvalidOptionException(string message, string optionName, Exception innerException)
        : base(message, innerException)
    {
        this.OptionName = optionName;
    }

    /// <summary>
    /// Gets the name of the program option that caused the exception.
    /// </summary>
    public string OptionName
    {
        get;
    }
}
