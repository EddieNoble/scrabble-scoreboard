using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ScrabbleScoreboard.Studies.Domain.Shared;
using System.Text;
using System.Text.Json;

namespace ScrabbleScoreboad.Consumer
{
    public static class Choreography
    {
        public static async Task Demo1() {
            await StartValidationService();
            await StartProcessingService();
            await StartCompletionService();

            Console.WriteLine("Choreography running...");
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
                exchange: RabbitConstants.ProcessedExchange,
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

                Console.ResetColor();

                var processed = new MessageProcessed
                {
                    CorrelationId = validated!.CorrelationId
                };

                var processedJson =
                    JsonSerializer.Serialize(processed);

                var processedBody =
                    Encoding.UTF8.GetBytes(processedJson);

                await channel.BasicPublishAsync(
                    exchange: RabbitConstants.ProcessedExchange,
                    routingKey: string.Empty,
                    body: processedBody);

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine(
                    $"[Processing] Published MessageProcessed: {processed.CorrelationId}");

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

        private static async Task StartCompletionService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: RabbitConstants.ProcessedExchange,
                type: ExchangeType.Fanout);

            await channel.QueueDeclareAsync(
                queue: RabbitConstants.CompletionQueue,
                durable: true,
                exclusive: false,
                autoDelete: false);

            await channel.QueueBindAsync(
                queue: RabbitConstants.CompletionQueue,
                exchange: RabbitConstants.ProcessedExchange,
                routingKey: string.Empty);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                var processed =
                    JsonSerializer.Deserialize<MessageProcessed>(json);

                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine(
                    $"[Completion] Received MessageProcessed: {processed?.CorrelationId}");

                Console.WriteLine(
                    $"[Completion] Workflow Complete");

                Console.ResetColor();

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: RabbitConstants.CompletionQueue,
        autoAck: true,
        consumer: consumer);

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Completion Service Started");

            Console.ResetColor();
        }
    }
}
