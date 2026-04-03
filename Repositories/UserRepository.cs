using img_video_app_back.Models;
using img_video_app_back.Models.Dtos;
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
                    ?? throw new Exception("User not found");
            }
            catch (Exception ex)
            {
                throw new Exception("Error get user ", ex);
            }
        }

        public async Task<List<FavoriteDto>> GetFavoritesByUserId(int id)
        {
            try
            {
                return await _context.Favorites.Where(f => f.UserId == id).Select(f => new FavoriteDto
                {
                    Id = f.Id,
                    UserId = f.UserId,
                    ItemId = f.ItemId,
                    Type = f.Type,
                    Url = f.Url
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error get user ", ex);
            }
        }

        public async Task<FavoriteDto> SaveFavorite(FavoriteDto favorite)
        {
            try
            {
                await _context.Favorites.AddAsync(new Favorite
                {
                    UserId = favorite.UserId,
                    ItemId = favorite.ItemId,
                    Type = favorite.Type,
                    Url = favorite.Url
                });
                await _context.SaveChangesAsync();
                return favorite;
            }
            catch (Exception ex)
            {
                throw new Exception("Error save favorite ", ex);
            }
        }

        public async Task DeleteFavorite(FavoriteDto favorite)
        {
            try
            {
                var favoriteValue = (await GetFavoritesByUserId(favorite.UserId)).FirstOrDefault();

                if (favoriteValue == null) {return;}  

                _context.Favorites.Remove(new Favorite
                {
                    Id = favoriteValue.Id,
                    UserId = favorite.UserId,
                    ItemId = favorite.ItemId,
                    Type = favorite.Type,
                    Url = favorite.Url
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error save favorite ", ex);
            }
        }
    }

}