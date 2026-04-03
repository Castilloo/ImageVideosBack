using System;
using System.Collections.Generic;

namespace img_video_app_back.Models;

public partial class Favorite
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ItemId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Url { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
