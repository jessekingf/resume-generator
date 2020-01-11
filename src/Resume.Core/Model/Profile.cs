// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Resume.Core.Model
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains information on a profile.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Gets or sets the network.
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        [JsonPropertyName("url")]
        public Uri Url { get; set; }
    }
}
