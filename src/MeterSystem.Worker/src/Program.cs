using MeterSystem.Shared.src.Messaging;
using MeterSystem.Shared.src.Messaging.RabbitMq;
using MeterSystem.Worker.src;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ReadingHandler>();

builder.Services.Configure<RabbitMqOptions>(
    builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddSingleton<RabbitMqConsumer>();

builder.Services.AddHostedService<MeterWorker>();



var host = builder.Build();
host.Run();
