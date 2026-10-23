using Microsoft.EntityFrameworkCore;
using ScrabbleScoreboard.Result.Api.Models.Messaging;
using ScrabbleScoreboard.Result.Api.Models.Messaging.Interfaces;
using ScrabbleScoreboard.Result.Api.Persistence;
using ScrabbleScoreboard.Result.Api.Persistence.Repositories;
using ScrabbleScoreboard.Result.Api.Persistence.Repositories.Interfaces;
using ScrabbleScoreboard.Result.Api.Services;
using ScrabbleScoreboard.Result.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IMessagingAdminService, RabbitMessagingAdminService>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<ResultsDbContext>();

    await db.Database.MigrateAsync();
}

app.Run();
