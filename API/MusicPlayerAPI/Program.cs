using MusicPlayerAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using MusicPlayerAPI.Interfaces;
using MusicPlayerAPI.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MusicPlayerContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddCors();
builder.Services.AddScoped<IArtists, ArtistLogic>();
builder.Services.AddScoped<IAlbums, AlbumLogic>();
builder.Services.AddScoped<ISongs, SongLogic>();
builder.Services.AddScoped<IGenres, GenreLogic>();

var app = builder.Build();
//var services = scope.ServiceProvider;
try
{
    var context = app.Services.GetRequiredService<MusicPlayerContext>();
    DbInitializer.Initialize(context);
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration.");
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
