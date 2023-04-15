using System.Text.Json.Serialization;

namespace ClassLibrary.Models;

public class Animal
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("LegCount")]
    public int LegCount { get; set; }
}
