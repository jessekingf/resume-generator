namespace Resume.Core.Model;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Contains details on a job position.
/// </summary>
public class Job
{
    /// <summary>
    /// Gets or sets the company name.
    /// </summary>
    [JsonPropertyName("company")]
    public string Company { get; set; }

    /// <summary>
    /// Gets or sets the position within the company.
    /// </summary>
    [JsonPropertyName("position")]
    public string Position { get; set; }

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

    /// <summary>
    /// Gets or sets the job summary.
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// Gets or sets the address of the job position.
    /// </summary>
    [JsonPropertyName("location")]
    public Address Location { get; set; }

    /// <summary>
    /// Gets or sets the job highlights.
    /// </summary>
    [JsonPropertyName("highlights")]
    public IList<string> Highlights { get; set; }
}
