using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ScrabbleScoreboard.Studies.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace ScrabbleScoreboad.Consumer
{
    public static class Compensation
    {
        public static async Task Demo1()
        {
            await StartValidationService();
            await StartProcessingService();
            await StartCompensationService();

            Console.WriteLine("Compensation demo running...");
            Console.ReadLine();
        }

        private static async Task StartValidationService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: RabbitConstants.SubmittedExchange,
                type: ExchangeType.Fanout);

            await channel.ExchangeDeclareAsync(
                exchange: RabbitConstants.ValidatedExchange,
                type: ExchangeType.Fanout);

            await channel.QueueDeclareAsync(
                queue: RabbitConstants.ValidationQueue,
                durable: true,
                exclusive: false,
                autoDelete: false);

            await channel.QueueBindAsync(
                queue: RabbitConstants.ValidationQueue,
                exchange: RabbitConstants.SubmittedExchange,
                routingKey: string.Empty);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                var submitted =
                    JsonSerializer.Deserialize<MessageSubmitted>(json);

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(
                    $"[Validation] Received MessageSubmitted: {submitted?.CorrelationId}");

                Console.ResetColor();

                var validated = new MessageValidated
                {
                    CorrelationId = submitted!.CorrelationId
                };

                var validatedJson =
                    JsonSerializer.Serialize(validated);

                var validatedBody =
                    Encoding.UTF8.GetBytes(validatedJson);

                await channel.BasicPublishAsync(
                    exchange: RabbitConstants.ValidatedExchange,
                    routingKey: string.Empty,
                    body: validatedBody);

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(
                    $"[Validation] Published MessageValidated: {validated.CorrelationId}");

                Console.ResetColor();
            };

            await channel.BasicConsumeAsync(
                queue: RabbitConstants.ValidationQueue,
                autoAck: true,
                consumer: consumer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Validation Service Started");
            Console.ResetColor();
        }

        private static async Task StartProcessingService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: RabbitConstants.ValidatedExchange,
                type: ExchangeType.Fanout);

            await channel.ExchangeDeclareAsync(
                exchange: RabbitConstants.RejectedExchange,
                type: ExchangeType.Fanout);

            await channel.QueueDeclareAsync(
                queue: RabbitConstants.ProcessingQueue,
                durable: true,
                exclusive: false,
                autoDelete: false);

            await channel.QueueBindAsync(
                queue: RabbitConstants.ProcessingQueue,
                exchange: RabbitConstants.ValidatedExchange,
                routingKey: string.Empty);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                var validated =
                    JsonSerializer.Deserialize<MessageValidated>(json);

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine(
                    $"[Processing] Received MessageValidated: {validated?.CorrelationId}");

                Console.WriteLine(
                    "[Processing] Simulating Failure");

                Console.ResetColor();

                var rejected = new MessageRejected
                {
                    CorrelationId = validated!.CorrelationId,
                    Reason = "Simulated Failure"
                };

                var rejectedJson =
                    JsonSerializer.Serialize(rejected);

                var rejectedBody =
                    Encoding.UTF8.GetBytes(rejectedJson);

                await channel.BasicPublishAsync(
                    exchange: RabbitConstants.RejectedExchange,
                    routingKey: string.Empty,
                    body: rejectedBody);

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(
                    $"[Processing] Published MessageRejected: {rejected.CorrelationId}");

                Console.ResetColor();
            };

            await channel.BasicConsumeAsync(
                queue: RabbitConstants.ProcessingQueue,
                autoAck: true,
                consumer: consumer);

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Processing Service Started");

            Console.ResetColor();
        }

        private static async Task StartCompensationService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection =
                await factory.CreateConnectionAsync();

            var channel =
                await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: RabbitConstants.RejectedExchange,
                type: ExchangeType.Fanout);

            await channel.QueueDeclareAsync(
                queue: RabbitConstants.CompensationQueue,
                durable: true,
                exclusive: false,
                autoDelete: false);

            await channel.QueueBindAsync(
                queue: RabbitConstants.CompensationQueue,
                exchange: RabbitConstants.RejectedExchange,
                routingKey: string.Empty);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json =
                    Encoding.UTF8.GetString(ea.Body.ToArray());

                var rejected =
                    JsonSerializer.Deserialize<MessageRejected>(json);

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(
                    $"[Compensation] Received MessageRejected: {rejected?.CorrelationId}");

                Console.WriteLine(
                    $"[Compensation] Reason: {rejected?.Reason}");

                Console.WriteLine(
                    "[Compensation] Compensation Complete");

                Console.ResetColor();

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: RabbitConstants.CompensationQueue,
                autoAck: true,
                consumer: consumer);

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(
                "Compensation Service Started");

            Console.ResetColor();
        }
    }
}
