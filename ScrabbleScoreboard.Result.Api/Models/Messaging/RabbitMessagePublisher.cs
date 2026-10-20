using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using ScrabbleScoreboard.Result.Api.Models.Messaging.Contracts;
using ScrabbleScoreboard.Result.Api.Models.Messaging.Interfaces;
using System.Text;
using System.Text.Json;

namespace ScrabbleScoreboard.Result.Api.Models.Messaging
{
    public class RabbitMessagePublisher : IMessagePublisher
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
    }
}
