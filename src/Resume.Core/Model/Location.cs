// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Resume.Core.Model
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains address information.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets or sets the street address.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        [JsonPropertyName("region")]
        public string Region { get; set; }
    }
}
