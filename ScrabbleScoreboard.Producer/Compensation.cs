using RabbitMQ.Client;
using ScrabbleScoreboard.Studies.Domain.Shared;
using System.Text;
using System.Text.Json;

namespace ScrabbleScoreboard.Producer
{
    public static class Compensation
    {
        public static async Task Demo1()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            await using var connection = await factory.CreateConnectionAsync();

            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: RabbitConstants.SubmittedExchange,
                type: ExchangeType.Fanout);

            var message = new MessageSubmitted
            {
                CorrelationId = Guid.NewGuid()
            };

            var json = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: RabbitConstants.SubmittedExchange,
                routingKey: string.Empty,
                body: body);


            Console.WriteLine($"Sent: {message}");
        }

    }
}
