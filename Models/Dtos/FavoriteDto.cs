namespace img_video_app_back.Models.Dtos;

public class FavoriteDto
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string ItemId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Url { get; set; } = null!;
}