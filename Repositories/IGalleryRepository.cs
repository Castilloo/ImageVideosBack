using img_video_app_back.Models;
using img_video_app_back.Models.Dtos;

namespace img_video_app_back.Repositories;

public interface IGalleryRepository
{
    Task<List<PhotoDto>> GetPhotosDefault(int page, int perPage);
    Task<List<VideoDto>> GetVideosDefault(int page, int perPage);
    Task<List<PhotoDto>> GetPhotosByKeyword(string keyword);
    Task<List<VideoDto>> GetVideosByKeyword(string keyword);
}