using img_video_app_back.Models;

namespace img_video_app_back.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmail(string email);
    Task<User> GetUserById(int id); 
    Task<User> Create(User user);
}