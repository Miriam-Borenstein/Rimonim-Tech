using MeterSystem.Api.src.DTOs;
using MeterSystem.Api.src.Messaging;
using MeterSystem.Shared.src.Messaging;

namespace MeterSystem.Api.src.Features.Readings;

public class ReadingsHandler
{

    private readonly IMessagePublisher _publisher;
    private readonly ILogger<ReadingsHandler> _logger;

    public ReadingsHandler(IMessagePublisher publisher, ILogger<ReadingsHandler> logger)
    {
        _publisher = publisher;
        _logger = logger;
    }

    internal async Task Handle(ReadingRequest request) {

        _logger.LogInformation("Received POST request");
        var message = new ReadingMessage(
            request.meter_number,
            request.Readings.Select(r => new ReadingItem(r.Key, r.Value)).ToList()
            );

        await  _publisher.PublishAsync(message);
        _logger.LogInformation("Publishing message to RabbitMQ");

    }
}
