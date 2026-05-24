using MeterSystem.Shared.src.Messaging;
using MeterSystem.Shared.src.Messaging.RabbitMq;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddHostedService<ReadingsConsumer>();
builder.Services.Configure<RabbitMqOptions>(
    builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();



var host = builder.Build();
host.Run();
