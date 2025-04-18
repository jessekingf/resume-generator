namespace Common.Extensions;

using System;
using System.IO;
using System.Reflection;

/// <summary>
/// Extension methods for the <see cref="Assembly"/> class.
/// </summary>
public static class AssemblyExtensions
{
    /// <summary>
    /// Reads the contents of an embedded resource file from the assembly.
    /// </summary>
    /// <param name="assembly">The assembly to read the embedded resource file from.</param>
    /// <param name="filename">
    /// Name of the embedded resource file to read.
    /// The resource name should not include the assembly name prefix.
    /// </param>
    /// <returns>The contents of the embedded resource file.</returns>
    public static string ReadResourceFile(this Assembly assembly, string filename)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        if (string.IsNullOrEmpty(filename))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(filename));
        }

        string? assemblyName = assembly.GetName().Name;
        if (string.IsNullOrEmpty(assemblyName))
        {
            throw new FileNotFoundException("Failed to get assembly for resource file '{filename}'");
        }

        using Stream? stream = assembly.GetManifestResourceStream($"{assemblyName}.{filename}");
        if (stream == null)
        {
            throw new FileNotFoundException($"The resource file '{filename}' could not be found in assembly '{assemblyName}'.", filename);
        }

        using StreamReader reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
