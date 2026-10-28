using Microsoft.EntityFrameworkCore;
using ScrabbleScoreboard.Player.Api.Persistence;
using ScrabbleScoreboard.Player.Api.Persistence.Repositories;
using ScrabbleScoreboard.Player.Api.Persistence.Repositories.Interfaces;
using ScrabbleScoreboard.Player.Api.Services;
using ScrabbleScoreboard.Player.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IPlayerService, PlayersService>();
builder.Services.AddSingleton<LiteDbContext>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

builder.Services.AddDbContext<PlayersDbContext>(
    options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("PlayersDb")));

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<PlayersDbContext>();

    await db.Database.MigrateAsync();

    await PlayerSeeder.SeedAsync(db);
}

app.Run();
