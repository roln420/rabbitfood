using RabbitFood.Consumer;
using RabbitFood.Shared.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<IRabbitService, RabbitService>();

var host = builder.Build();
host.Run();
