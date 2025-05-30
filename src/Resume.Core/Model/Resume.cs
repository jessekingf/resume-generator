﻿namespace Resume.Core.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Personal, educational, and professional qualifications and experience,
/// as that prepared by an applicant for a job.
/// </summary>
public class Resume
{
    /// <summary>
    /// Gets or sets the full name of the applicant.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the label of the applicant.
    /// </summary>
    /// <remarks>For example the current job title of the applicant.</remarks>
    [JsonPropertyName("label")]
    public required string Label { get; set; }

    /// <summary>
    /// Gets or sets the email address of the applicant.
    /// </summary>
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the applicant.
    /// </summary>
    [JsonPropertyName("phone")]
    public required string Phone { get; set; }

    /// <summary>
    /// Gets or sets the address of the applicant.
    /// </summary>
    [JsonPropertyName("location")]
    public required Address Location { get; set; }

    /// <summary>
    /// Gets or sets the personal website of the applicant.
    /// </summary>
    [JsonPropertyName("website")]
    public string? Website { get; set; }

    /// <summary>
    /// Gets or sets the professional summary of the applicant.
    /// </summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    /// <summary>
    /// Gets or sets the professional highlights of the applicant.
    /// </summary>
    [JsonPropertyName("highlights")]
    public IList<string> Highlights { get; set; } = [];

    /// <summary>
    /// Gets or sets the job experience of the applicant.
    /// </summary>
    [JsonPropertyName("work")]
    public IList<Job> Work { get; set; } = [];

    /// <summary>
    /// Gets or sets the education details of the applicant.
    /// </summary>
    [JsonPropertyName("education")]
    public IList<EducationProgram> Education { get; set; } = [];

    /// <summary>
    /// Gets or sets the skills of the applicant.
    /// </summary>
    [JsonPropertyName("skills")]
    public IList<Skill> Skills { get; set; } = [];
}
