using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ScrabbleScoreboard.Domain.Events;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace ScrabbleScoreboard.Player.Api.Infrastructure.Consumers;

public class PlayerCreatedConsumer
{
    public async Task ConsumeAsync()
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

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, args) =>
                    {
                        var body = args.Body.ToArray();

                        var message = Encoding.UTF8.GetString(body);

                        Debug.WriteLine($"Received: {message}");

                        var createdPlayer = JsonSerializer.Deserialize<PlayerCreatedEvent>(message);

                        Debug.WriteLine($"Created Player: {createdPlayer?.Name} {createdPlayer?.EmailAddress} at {createdPlayer?.OccurredAtUtc} ");

                        await Task.CompletedTask;
                    };

        await channel.BasicConsumeAsync(
            queue: QueueNames.PlayerCreated,
            autoAck: true,
            consumer: consumer);
    }

    public async Task<PlayerCreatedEvent?> GetNextMessageAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        await using var connection =
            await factory.CreateConnectionAsync();

        await using var channel =
            await connection.CreateChannelAsync();

        var result = await channel.BasicGetAsync(
            QueueNames.PlayerCreated,
            autoAck: false);

        if (result == null)
        {
            return null;
        }

        var json = Encoding.UTF8.GetString(
            result.Body.ToArray());

        var player = JsonSerializer.Deserialize<PlayerCreatedEvent>(
            json);

        await channel.BasicAckAsync(
            result.DeliveryTag,
            false);

        return player;
    }

    public async Task<uint> CurrentQueueLengthAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        await using var connection =
            await factory.CreateConnectionAsync();

        await using var channel =
            await connection.CreateChannelAsync();

        var queueInfo =
            await channel.QueueDeclarePassiveAsync(
                QueueNames.PlayerCreated);

        return queueInfo.MessageCount;
    }

}