using img_video_app_back.Models;
using img_video_app_back.Models.Dtos;
using img_video_app_back.Repositories;
using img_video_app_back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace img_video_app_back.Controllers;

[ApiController]
[Route("api/image-video/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly JWTService _jwt;
    private readonly IUserRepository _userRepository;

    public AuthController(JWTService jwt, IUserRepository userRepository, ILogger<AuthController> logger)
    {
        _jwt = jwt;
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (dto == null) return BadRequest(new { message = "Datos inválidos" });

        var user = await _userRepository.GetByEmail(dto.Email ?? "");
        if (user == null) return BadRequest(new { message = "Credenciales inválidas" });

        if (string.IsNullOrWhiteSpace(dto.Password))
            return Unauthorized(new { message = "Credenciales inválidas" });

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Credenciales inválidas" });

        var jwt = _jwt.Generate(user.Id);

        Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

        return Ok(new { message = "success", token = jwt });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (dto == null) return BadRequest("Datos inválidos");

        try
        {
            // Verificar si el usuario ya existe
            var existingUser = await _userRepository.GetByEmail(dto.Email);
            if (existingUser.Email != null)
                return BadRequest("El email ya está registrado");

            // Crear usuario con contraseña segura (BCrypt)
            var user = new User
            {
                Username = dto.Name ?? "",
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            var newUser = await _userRepository.Create(user);

            var token = _jwt.Generate(newUser.Id);

            return Created("", new { User = newUser, Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest("Error: " + ex);
        }
    }

}