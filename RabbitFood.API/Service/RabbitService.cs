using RabbitMQ.Client;
using System.Text;

namespace rabbit_food_api.Service;

public class RabbitService : IRabbitService
{
    public async Task SendMessageAsync(string message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            UserName = "carrot",
            Password = "P@ssword!"
        };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
    }
}
