using img_video_app_back.Models;
using img_video_app_back.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace img_video_app_back.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly GalleryDbContext _context;
        public UserRepository(GalleryDbContext context) => _context = context;
        public async Task<User> Create(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error save user ", ex);
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.Email == email) 
                    ?? new User(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Error get user ", ex);
            }
            
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.Id == id)
                    ?? throw new Exception("User not found");;
            }
            catch (Exception ex)
            {
                throw new Exception("Error get user ", ex);
            }
        }
    }

}