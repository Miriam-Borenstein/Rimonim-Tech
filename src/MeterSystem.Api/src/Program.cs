using Carter;
using MeterSystem.Api.src.Features.Readings;
using MeterSystem.Shared.src.Messaging;
using MeterSystem.Shared.src.Messaging.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();

builder.Services.AddScoped<ReadingsHandler>();

builder.Services.Configure<RabbitMqOptions>(
    builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapCarter();

app.Run();
