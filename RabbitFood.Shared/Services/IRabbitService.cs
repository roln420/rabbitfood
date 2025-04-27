using RabbitMQ.Client.Events;

namespace RabbitFood.Shared.Services;

public interface IRabbitService
{
    Task SendMessageAsync(string message);
    Task ListenForMessagesAsync(string queueName, AsyncEventHandler<BasicDeliverEventArgs> listener);
}
