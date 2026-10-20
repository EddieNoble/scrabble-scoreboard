using RabbitMQ.Client;
using ScrabbleScoreboard.Result.Api.Models.Messaging.Contracts;
using ScrabbleScoreboard.Result.Api.Models.Messaging.Interfaces;
using System.Text;
using System.Text.Json;

namespace ScrabbleScoreboard.Result.Api.Models.Messaging
{
    public class RabbitMessagingAdminService : IMessagingAdminService
    {

        public async Task PublishHealthMessageAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection =
                await factory.CreateConnectionAsync();

            var channel =
                await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "pipeline-health",
                durable: true,
                exclusive: false,
                autoDelete: false);

            var message =
                new PipelineHealthMessage
                {
                    Message = "Are you OK?"
                };

            var json =
                JsonSerializer.Serialize(message);

            var body =
                Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: "pipeline-health",
                body: body);
        }

        public async Task<List<PipelineHealthMessage>> GetHealthMessagesAsync()
        {
            var messages = new List<PipelineHealthMessage>();

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection =
                await factory.CreateConnectionAsync();

            var channel =
                await connection.CreateChannelAsync();

            while (true)
            {
                var result =
                    await channel.BasicGetAsync(
                        queue: "pipeline-health",
                        autoAck: true);

                if (result is null)
                {
                    break;
                }

                var json =
                    Encoding.UTF8.GetString(
                        result.Body.ToArray());

                var message =
                    JsonSerializer.Deserialize<
                        PipelineHealthMessage>(json);

                if (message is not null)
                {
                    messages.Add(message);
                }
            }

            return messages;
        }

    }
}
