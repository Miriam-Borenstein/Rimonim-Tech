namespace MeterSystem.Worker.src;

public class MeterWorker : BackgroundService
{
    private readonly RabbitMqConsumer _consumer;

    public MeterWorker(RabbitMqConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _consumer.Start(cancellationToken);

        await Task.Delay(Timeout.Infinite, cancellationToken);

    }
}
