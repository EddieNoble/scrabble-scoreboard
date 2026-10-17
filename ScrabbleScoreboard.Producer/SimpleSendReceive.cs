using RabbitMQ.Client;
using System;
using System.Text;

namespace ScrabbleScoreboard.Producer
{
    public static class SimpleSendReceive
    {
        public static async Task Demo1()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "scrabble-demo",
                durable: true,
                exclusive: false,
                autoDelete: false);

            var message = """
                  {
                      "eventType": "PlayerRegistered",
                      "playerId": "1"
                  }
                  """;

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "scrabble-demo",
                body: body,
                cancellationToken: CancellationToken.None);

            Console.WriteLine($"Sent: {message}");

        }
    }
}