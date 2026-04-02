using Newtonsoft.Json;

namespace img_video_app_back.Models.Dtos;

public class VideoDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; } = ""; 

    [JsonProperty("duration")]
    public int Duration { get; set; }

    [JsonProperty("video_file")]
    public VideoFileDto VideoFile { get; set; } = null!;
}