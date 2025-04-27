using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitFood.Shared.Services;

public class RabbitService : IRabbitService
{
    private readonly ConnectionFactory _factory = new()
    {
            HostName = "rabbitmq",
            UserName = "carrot",
            Password = "P@ssword!"
        };

    public async Task SendMessageAsync(string message)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
    }

    public async Task ListenForMessagesAsync(string queueName, AsyncEventHandler<BasicDeliverEventArgs> listener)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += listener;

        await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);
    }
}
