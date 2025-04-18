namespace Resume.Core.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a job skill.
/// </summary>
public class Skill
{
    /// <summary>
    /// Gets or sets the name of the skill.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the keywords for the skill.
    /// </summary>
    [JsonPropertyName("keywords")]
    public IList<string> Keywords { get; set; } = [];
}
