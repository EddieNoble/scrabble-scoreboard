using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using ScrabbleScoreboard.Domain.Events;
using ScrabbleScoreboard.Player.Api.Infrastructure.Publishers.Interfaces;

namespace ScrabbleScoreboard.Player.Api.Infrastructure.Publishers;

public class PlayerCreatedPublisher : IPlayerCreatedPublisher
{
    public async Task PublishAsync(PlayerCreatedEvent playerCreated)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: QueueNames.PlayerCreated,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var message = JsonSerializer.Serialize(playerCreated);

        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "scrabble-demo",
            body: body,
            cancellationToken: CancellationToken.None);

        Console.WriteLine($"Sent: {message}");
    }
}