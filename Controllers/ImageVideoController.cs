using img_video_app_back.Models;
using img_video_app_back.Models.Dtos;
using img_video_app_back.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace img_video_app_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageVideoController : ControllerBase
{
    private readonly ILogger<ImageVideoController> _logger;
    private readonly IGalleryRepository _repository;
    public ImageVideoController(ILogger<ImageVideoController> logger, IGalleryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("photos")]
    [ProducesResponseType(typeof(List<PhotoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<PhotoDto>>> GetPhotos(int page, int perPage)
    {
        try
        {
            var photos = await _repository.GetPhotosDefault(page, perPage);

            // _logger.LogInformation("Fotos recibidas", photos.Count);

            if (!photos.Any()) return NoContent();

            return Ok(photos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet("videos")]
    [ProducesResponseType(typeof(List<VideoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<VideoDto>>> GetVideos(int page, int perPage)
    {
        try
        {
            var videos = await _repository.GetVideosDefault(page, perPage);

            // _logger.LogInformation("Videos recibidos", videos.Count);

            if (!videos.Any()) return NoContent();

            return Ok(videos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet("photos/{query}")]
    [ProducesResponseType(typeof(List<PhotoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<PhotoDto>>> GetPhotos(string query)
    {
        try
        {
            var photos = await _repository.GetPhotosByKeyword(query);

            // _logger.LogInformation("Fotos recibidas", photos.Count);

            if (!photos.Any()) return NoContent();

            return Ok(photos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet("videos/{query}")]
    [ProducesResponseType(typeof(List<VideoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<VideoDto>>> GetVideos(string query)
    {
        try
        {
            var videos = await _repository.GetVideosByKeyword(query);

            _logger.LogInformation("Videos recibidos", videos.Count);

            if (!videos.Any()) return NoContent();

            return Ok(videos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = ex.Message
            });
        }
    }
}