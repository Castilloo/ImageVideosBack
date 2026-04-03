using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace img_video_app_back.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    [JsonIgnore]
    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
