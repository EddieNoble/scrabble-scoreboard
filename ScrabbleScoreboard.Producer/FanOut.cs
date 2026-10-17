using RabbitMQ.Client;
using ScrabbleScoreboard.Studies.Domain.RabbitMQ.Fanout.Messages;
using System.Text;
using System.Text.Json;

namespace ScrabbleScoreboard.Producer
{
    public static class FanOut
    {
        public static async Task Demo1()
        {
            const string exchangeName = "results.submitted";

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: exchangeName,
                type: ExchangeType.Fanout);

            var result = new ResultSubmitted
            {
                ResultId = Guid.NewGuid(),
                PlayerName = "Karl Kruger",
                Score = 700,
                SubmittedAt = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(result);

            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: exchangeName,
                routingKey: string.Empty,
                body: body,
                cancellationToken: CancellationToken.None);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Published ResultSubmitted event");
            Console.ResetColor();
        }
    }
}
