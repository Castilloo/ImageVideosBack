using Newtonsoft.Json;

namespace img_video_app_back.Models.Dtos;

public class VideoFileDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("quality")]
    public string Quality { get; set; } = "";

    [JsonProperty("file_type")]
    public string FileType { get; set; } = "";

    [JsonProperty("width")]
    public int? Width { get; set; }

    [JsonProperty("height")]
    public int? Height { get; set; }

    [JsonProperty("fps")]
    public decimal? Fps { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; } = "";
}

