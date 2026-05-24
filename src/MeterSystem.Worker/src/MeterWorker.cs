using System;
using System.Collections.Generic;
using System.Linq;
namespace MeterSystem.Worker.src;

public class MeterWorker : BackgroundService
{
    private readonly RabbitMqConsumer _consumer;

    public MeterWorker(RabbitMqConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Start();
        return Task.CompletedTask;
    }
}
