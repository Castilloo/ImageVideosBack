using Newtonsoft.Json;

namespace img_video_app_back.Models.Dtos;

public class NewBaseType
{
    [JsonProperty("src")]
    public Src Src { get; set; } = null!;
}

public partial class PhotoDto : NewBaseType
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("photographer")]
    public string Photographer { get; set; } = "";

    [JsonProperty("photographer_id")]
    public string PhotographerId { get; set; } = "";

    [JsonProperty("alt")]
    public string Alt { get; set; } = "";
}
