using img_video_app_back.Config;
using img_video_app_back.Repositories;
using Microsoft.Extensions.Options;
using PexelsDotNetSDK.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.Configure<ApiKeyOptions>(builder.Configuration.GetSection("Pexels"));
builder.Services.AddSingleton(sp =>
{
    var apiKeyOptions = sp.GetRequiredService<IOptions<ApiKeyOptions>>().Value;
    return new PexelsClient(apiKeyOptions.ApiKey);
});
builder.Services.AddHttpClient("Pexels", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Pexels:SearchVideosUrl"] ?? "");
    client.Timeout = TimeSpan.FromSeconds(10);
    client.DefaultRequestHeaders.Add("Authorization", builder.Configuration["Pexels:ApiKey"]);
});
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();

var app = builder.Build();

app.UseCors("FrontendPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();