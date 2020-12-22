// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Serialization
{
    using System;

    /// <summary>
    /// Provides functionality to serialize objects to JSON and to deserialize JSON into objects.
    /// </summary>
    public class JsonSerializer : ISerializer
    {
        /// <summary>
        /// Parses JSON text into an instance of <typeparamref name="T" />.
        /// </summary>
        /// <param name='json'>The JSON text to parse.</param>
        /// <returns>
        /// An instance of <typeparamref name="T" /> parsed from the JSON text.
        /// </returns>
        /// <typeparam name="T">The type of object being deserialized.</typeparam>
        public T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("The value cannot be null or empty.", nameof(json));
            }

            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Converts an instance of <typeparamref name="T" /> to JSON text.
        /// </summary>
        /// <param name='value'>The <typeparamref name="T" /> to serialize.</param>
        /// <returns>The JSON text representation the instance of <typeparamref name="T" />.</returns>
        /// <typeparam name="T">The type of object being serialized.</typeparam>
        public string Serialize<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return System.Text.Json.JsonSerializer.Serialize<T>(value);
        }
    }
}
