using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace img_video_app_back.Models.Http;

public partial class VideoResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("width")]
    public long Width { get; set; }

    [JsonPropertyName("height")]
    public long Height { get; set; }

    [JsonPropertyName("duration")]
    public long Duration { get; set; }

    [JsonPropertyName("full_res")]
    public object FullRes { get; set; } = null!;

    [JsonPropertyName("tags")]
    public List<object> Tags { get; set; } = null!;

    [JsonPropertyName("url")]
    public Uri Url { get; set; } = null!;

    [JsonPropertyName("image")]
    public Uri Image { get; set; } = null!;

    [JsonPropertyName("avg_color")]
    public object AvgColor { get; set; } = null!;

    [JsonPropertyName("user")]
    public User User { get; set; } = null!;

    [JsonPropertyName("video_files")]
    public List<VideoFile> VideoFiles { get; set; } = null!;

    [JsonPropertyName("video_pictures")]
    public List<VideoPicture> VideoPictures { get; set; } = null!;
}