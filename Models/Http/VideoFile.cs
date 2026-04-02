using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace img_video_app_back.Models.Http;

public partial class VideoFile
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("quality")]
    public string Quality { get; set; } = "";

    [JsonPropertyName("file_type")]
    public string FileType { get; set; } = "";

    [JsonPropertyName("width")]
    public long Width { get; set; }

    [JsonPropertyName("height")]
    public long? Height { get; set; }

    [JsonPropertyName("fps")]
    public decimal? Fps { get; set; }

    [JsonPropertyName("link")]
    public Uri Link { get; set; } = null!;

    // [JsonPropertyName("size")]
    // public long Size { get; set; }
}