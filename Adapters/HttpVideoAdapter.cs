using img_video_app_back.Models.Http;

namespace img_video_app_back.Adapters;

public class HttpVideoAdapter : IApiVideo
{
    private readonly VideoResponse _video;
    public HttpVideoAdapter(VideoResponse video) => _video = video;
    public IEnumerable<VideoFile> VideoFiles => _video.VideoFiles?
        .Select(v => new VideoFile
        {
            Id = v.Id,
            Quality = v.Quality,
            FileType = v.FileType,
            Width = v.Width,
            Height = v.Height,
            Fps = v.Fps,
            Link = v.Link,
        }) ?? [];
    public string Image => _video.Image.AbsoluteUri;
    public long Duration => _video.Duration;
    public long Id => _video.Id;
}