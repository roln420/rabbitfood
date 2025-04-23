namespace RabbitFood.Shared.Services;

public interface IRabbitService
{
    Task SendMessageAsync(string message);
}
