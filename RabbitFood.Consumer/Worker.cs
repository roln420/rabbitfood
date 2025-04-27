using System.Text;
using RabbitFood.Shared.Services;

namespace RabbitFood.Consumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IRabbitService _rabbitService;

    public Worker(ILogger<Worker> logger, IRabbitService rabbitService)
    {
        _logger = logger;
        _rabbitService = rabbitService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _rabbitService.ListenForMessagesAsync("hello", (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" [x] Received {message}");
                    return Task.CompletedTask;
                });
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
