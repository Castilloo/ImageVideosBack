using System.Text.Json.Serialization;

namespace img_video_app_back.Models.Http;

public partial class User
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("url")]
    public Uri Url { get; set; } = null!;
}