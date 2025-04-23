namespace rabbit_food_api.Service;

public interface IRabbitService
{
    Task SendMessageAsync(string message);
}
