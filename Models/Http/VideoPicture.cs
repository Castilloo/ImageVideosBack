using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace img_video_app_back.Models.Http;

public partial class VideoPicture
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("nr")]
    public long Nr { get; set; }

    [JsonPropertyName("picture")]
    public Uri Picture { get; set; } = null!;
}