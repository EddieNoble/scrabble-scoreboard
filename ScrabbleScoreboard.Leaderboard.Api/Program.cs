using Microsoft.EntityFrameworkCore;
using ScrabbleScoreboard.Leaderboard.Api.Data;
using ScrabbleScoreboard.Leaderboard.Api.Services;
using ScrabbleScoreboard.Leaderboard.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();

builder.Services.AddDbContext<PlayerDbContext>(
    options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("PlayersDb")));

builder.Services.AddDbContext<ResultsDbContext>(
    options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("ResultsDb")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AngularClient",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AngularClient");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
