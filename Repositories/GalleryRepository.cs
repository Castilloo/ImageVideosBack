using img_video_app_back.Adapters;
using img_video_app_back.Config;
using img_video_app_back.Models;
using img_video_app_back.Models.Dtos;
using img_video_app_back.Models.Http;
using Microsoft.Extensions.Options;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;

namespace img_video_app_back.Repositories;

public class GalleryRepository : IGalleryRepository
{
    private readonly PexelsClient _client;
    private readonly IHttpClientFactory _http;
    public GalleryRepository(PexelsClient client, IHttpClientFactory http)
    {
        _client = client;
        _http = http;
    }
    public async Task<List<PhotoDto>> GetPhotosDefault(int page, int perPage)
    {
        try
        {
            var result = (page == 0 && perPage == 0) ?
                await _client.CuratedPhotosAsync(1, 12)
                : await _client.CuratedPhotosAsync(page, perPage);

            return result?.photos?
                .Select(p => ToPhotoDto(p))
                .ToList() ?? new List<PhotoDto>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error de respuesta de la API", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new Exception("Tiempo de respuesta de API expiró", ex);
        }
    }

    public async Task<List<VideoDto>> GetVideosDefault(int page, int perPage)
    {
        try
        {
            var result = (page == 0 && perPage == 0)
                ? await _client.PopularVideosAsync(1, 12)
                : await _client.PopularVideosAsync(page, perPage);
            return result?.videos?
                .Select(p => ToVideoDto(new PexelsVideoAdapter(p)))
                .ToList() ?? new List<VideoDto>();
        }
        catch (HttpRequestException)
        {
            throw;
        }
        catch (TaskCanceledException)
        {
            throw;
        }
    }

    public async Task<List<PhotoDto>> GetPhotosByKeyword(string keyword)
    {
        try
        {
            var result = await _client.SearchPhotosAsync(keyword, "landscape", "medium");

            return result?.photos?
                .Select(p => ToPhotoDto(p))
                .ToList() ?? new List<PhotoDto>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error de respuesta de la API", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new Exception("Tiempo de respuesta de API expiró", ex);
        }
    }

    public async Task<List<VideoDto>> GetVideosByKeyword(string keyword)
    {
        try
        {
            var client = _http.CreateClient("Pexels");
            Console.WriteLine($"Realizando solicitud a: {client.BaseAddress}?query={keyword}");
            var result = await client.GetFromJsonAsync<HttpPexelsResponse>($"?query={keyword}?orientation=landscape&size=medium");
            // var result = await _client.SearchVideosAsync(keyword, "landscape", "medium");
            Console.WriteLine(result.Videos);

            return result?.Videos?
                .Select(p => ToVideoDto(new HttpVideoAdapter(p)))
                .ToList() ?? new List<VideoDto>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error de respuesta de la API", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new Exception("Tiempo de respuesta de API expiró", ex);
        }
    }

    public async Task<List<object>> GetPhotosAndVideos()
    {
        const string keyword = "nature";
        try
        {
            var photos = await GetPhotosByKeyword(keyword);
            var videos = await GetVideosByKeyword(keyword);

            var random = new Random();

            photos = ListaRandom(photos);

            videos = ListaRandom(videos);

            var combined = new List<object>();
            for (int i = 0; i < photos.Count; i++)
            {
                combined.Add(photos[i]);
                combined.Add(videos[i]);
            }

            return combined;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error de respuesta de la API", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new Exception("Tiempo de respuesta de API expiró", ex);
        }
    }

    private static PhotoDto ToPhotoDto(Photo apiPhoto)
    {
        return new PhotoDto
        {
            Id = apiPhoto.id,
            Photographer = apiPhoto.photographer,
            PhotographerId = apiPhoto.photographerId,
            Alt = apiPhoto.alt,
            Src = new Src
            {
                Original = apiPhoto.source.original,
                Medium = apiPhoto.source.medium,
                Small = apiPhoto.source.small,
                Portrait = apiPhoto.source.portrait
            }
        };
    }

    private static VideoDto ToVideoDto(IApiVideo apiVideo)
    {
        var file = apiVideo.VideoFiles?
        .FirstOrDefault(v => v.Quality == "hd" && v.FileType == "video/mp4")
        ?? apiVideo.VideoFiles?.FirstOrDefault();

        return new VideoDto
        {
            Id = apiVideo.Id,
            Image = apiVideo.Image,
            Duration = (int)apiVideo.Duration,
            VideoFile = file == null ? new VideoFileDto() : new VideoFileDto
            {
                Id = file.Id,
                Quality = file.Quality,
                FileType = file.FileType,
                Width = (int)file.Width,
                Height = (int)file.Height,
                Fps = file.Fps,
                Link = file.Link.AbsoluteUri,
            }
        };
    }

    private static List<V> ListaRandom<V>(List<V> lista)
    {
        var random = new Random();

        return lista
            .OrderBy(x => random.Next())
            .Take(2)
            .ToList();
    }
}