using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace img_video_app_back.Models.Http
{
    public partial class HttpPexelsResponse
    {
        [JsonPropertyName("page")]
        public long Page { get; set; }

        [JsonPropertyName("per_page")]
        public long PerPage { get; set; }

        [JsonPropertyName("videos")]
        public List<VideoResponse> Videos { get; set; } = null!;

        [JsonPropertyName("total_results")]
        public long TotalResults { get; set; }

        [JsonPropertyName("next_page")]
        public string NextPage { get; set; } = "";

        [JsonPropertyName("url")]
        public string Url { get; set; } = "";
    }
}