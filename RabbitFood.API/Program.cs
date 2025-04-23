using rabbit_food_api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRabbitService, RabbitService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/sendmessage", async (IRabbitService service, int numToSend = 1) =>
{
    for(var i=1; i<=numToSend;i++)
        await service.SendMessageAsync($"Hello World! {i}");

    //return Results.NoContent;
})
.WithName("SendMessage")
.WithOpenApi();

app.Run();