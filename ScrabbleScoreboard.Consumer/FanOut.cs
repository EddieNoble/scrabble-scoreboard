using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ScrabbleScoreboad.Consumer
{
    public static class FanOut
    {
        private const string ExchangeName = "results.submitted";

        public static async Task Demo1()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = await factory.CreateConnectionAsync();

            await StartLeaderboardConsumer(connection);
            await StartStatisticsConsumer(connection);
            await StartNotificationConsumer(connection);

            Console.WriteLine("Consumers running...");
            Console.WriteLine("Press ENTER to exit.");

            Console.ReadLine();

            await connection.DisposeAsync();
        }

        private static async Task StartLeaderboardConsumer(IConnection connection)
        {
            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Fanout);

            await channel.QueueDeclareAsync(
                queue: "leaderboard",
                durable: true,
                exclusive: false,
                autoDelete: false);

            await channel.QueueBindAsync(
                queue: "leaderboard",
                exchange: ExchangeName,
                routingKey: string.Empty);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Leaderboard] {json}");
                Console.ResetColor();

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: "leaderboard",
                autoAck: true,
                consumer: consumer);
        }

        private static async Task StartStatisticsConsumer(IConnection connection)
        {
            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Fanout);

            await channel.QueueDeclareAsync(
                queue: "statistics",
                durable: true,
                exclusive: false,
                autoDelete: false);

            await channel.QueueBindAsync(
                queue: "statistics",
                exchange: ExchangeName,
                routingKey: string.Empty);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Statistics] {json}");
                Console.ResetColor();

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: "statistics",
                autoAck: true,
                consumer: consumer);
        }

        private static async Task StartNotificationConsumer(IConnection connection)
        {
            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Fanout);

            await channel.QueueDeclareAsync(
                queue: "notifications",
                durable: true,
                exclusive: false,
                autoDelete: false);

            await channel.QueueBindAsync(
                queue: "notifications",
                exchange: ExchangeName,
                routingKey: string.Empty);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[Notifications] {json}");
                Console.ResetColor();

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: "notifications",
                autoAck: true,
                consumer: consumer);
        }
    }
}