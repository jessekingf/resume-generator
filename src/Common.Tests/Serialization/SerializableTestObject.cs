// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Tests.Serialization
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Serializable object for testing.
    /// </summary>
    public class SerializableTestObject
    {
        /// <summary>
        /// Gets or sets the first property for serialization testing.
        /// </summary>
        [JsonPropertyName("propertyOne")]
        public string PropertyOne
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the second property for serialization testing.
        /// </summary>
        [JsonPropertyName("propertyTwo")]
        public string PropertyTwo
        {
            get;
            set;
        }
    }
}
