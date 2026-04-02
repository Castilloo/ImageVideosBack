
using PexelsDotNetSDK.Models;
using HttpModels = img_video_app_back.Models.Http;

namespace img_video_app_back.Adapters;

public class PexelsVideoAdapter : IApiVideo
{
    private readonly Video _video;
    public PexelsVideoAdapter(Video video) => _video = video;
    public IEnumerable<HttpModels.VideoFile> VideoFiles => _video.videoFiles?
        .Select(v => new HttpModels.VideoFile
        {
            Id = v.id,
            Quality = v.quality,
            FileType = v.fileType,
            Width = (long) v.width,
            Height = (long) v.height,
            Fps = (decimal) v.fps,
            Link = new Uri(v.link),
        }) ?? [];
    
    public string Image => _video.image;
    public long Duration => _video.duration;
    public long Id => _video.id;
}