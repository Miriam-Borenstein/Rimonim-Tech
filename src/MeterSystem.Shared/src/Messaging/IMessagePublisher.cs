namespace MeterSystem.Shared.src.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default);
}
