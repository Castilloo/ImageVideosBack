using img_video_app_back.Models.Http;

namespace img_video_app_back.Adapters;

public interface IApiVideo
{
    public IEnumerable<VideoFile> VideoFiles { get; }
    public string Image { get; }
    public long Duration { get; }
    public long Id { get; }
}