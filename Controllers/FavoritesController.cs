using img_video_app_back.Models;
using img_video_app_back.Models.Dtos;
using img_video_app_back.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace img_video_app_back.Controllers;

[Authorize]
[ApiController]
[Route("api/ImageVideo/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly ILogger<FavoritesController> _logger;
    private readonly IUserRepository _repository;
    public FavoritesController(ILogger<FavoritesController> logger, IUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(List<Favorite>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<Favorite>>> GetPhotos(int userId)
    {
        try
        {
            var favorites = await _repository.GetFavoritesByUserId(userId);

            return Ok(favorites);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = ex.Message
            });
        }
    }   

    [HttpPost("{userId}")]
    [ProducesResponseType(typeof(FavoriteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FavoriteDto>> SaveFavorite(int userId, [FromBody] FavoriteDto favorite)
    {
        try
        {
            var savedFavorite = await _repository.SaveFavorite(favorite);

            return Ok(savedFavorite);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = ex.Message
            });
        }
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<Favorite>>> DeleteFavorite(int userId, [FromBody] FavoriteDto favorite)
    {
        try
        {
            await _repository.DeleteFavorite(favorite);

            return Ok();
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