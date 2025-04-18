namespace Common.Serialization;

/// <summary>
/// Provides functionality to serialize and deserialize objects.
/// </summary>
public interface ISerializer
{
    /// <summary>
    /// Parses text into an instance of <typeparamref name="T" />.
    /// </summary>
    /// <param name='text'>The text to parse.</param>
    /// <returns>
    /// An instance of <typeparamref name="T" /> parsed from the text.
    /// </returns>
    /// <typeparam name="T">The type of object being deserialized.</typeparam>
    T? Deserialize<T>(string text);

    /// <summary>
    /// Converts an instance of <typeparamref name="T" /> to text.
    /// </summary>
    /// <param name='value'>The <typeparamref name="T" /> to serialize.</param>
    /// <returns>The text representation the instance of <typeparamref name="T" />.</returns>
    /// <typeparam name="T">The type of object being serialized.</typeparam>
    string Serialize<T>(T value);
}
