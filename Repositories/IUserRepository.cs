using img_video_app_back.Models;
using img_video_app_back.Models.Dtos;

namespace img_video_app_back.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmail(string email);
    Task<User> GetUserById(int id); 
    Task<User> Create(User user);
    Task<List<FavoriteDto>> GetFavoritesByUserId(int id);
    Task<FavoriteDto> SaveFavorite(FavoriteDto favorite);
    Task DeleteFavorite(FavoriteDto favorite);
}