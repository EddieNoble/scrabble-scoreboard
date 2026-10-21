using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ScrabbleScoreboad.Consumer
{
    public static class ScrabblePipelineHealth
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
                queue: "pipeline-health",
                durable: true,
                exclusive: false,
                autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, args) =>
            {
                var body = args.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Received: {message}");

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: "pipeline-health",
                autoAck: true,
                consumer: consumer);

            Console.WriteLine("Listening...");
            Console.ReadLine();
        }
    }
}