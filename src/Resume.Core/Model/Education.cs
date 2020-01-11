// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Resume.Core.Model
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains details about education.
    /// </summary>
    public class Education
    {
        /// <summary>
        /// Gets or sets the institution.
        /// </summary>
        [JsonPropertyName("institution")]
        public string Institution { get; set; }

        /// <summary>
        /// Gets or sets the area of education.
        /// </summary>
        [JsonPropertyName("area")]
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the study type.
        /// </summary>
        [JsonPropertyName("studyType")]
        public string StudyType { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
    }
}
