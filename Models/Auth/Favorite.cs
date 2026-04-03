
namespace img_video_app_back.Models.Auth;

public class Favorite
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    // Relación con usuario
    public User? User { get; set; }
}