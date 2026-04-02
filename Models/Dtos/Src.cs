using Newtonsoft.Json;

namespace img_video_app_back.Models.Dtos;

public partial class Src
{
    [JsonProperty("original")]
    public string Original { get; set; } = "";

    [JsonProperty("medium")]
    public string Medium { get; set; } = "";

    [JsonProperty("small")]
    public string Small { get; set; } = "";

    [JsonProperty("portrait")]
    public string Portrait { get; set; } = "";



}