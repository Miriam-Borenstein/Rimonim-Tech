using MeterSystem.Api.src.DTOs;
using MeterSystem.Shared.src.Messaging;

namespace MeterSystem.Api.src.Features.Readings;

public class ReadingsHandler
{

    private readonly IMessagePublisher _publisher;

    public ReadingsHandler(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }

    internal async Task Handle(ReadingRequest request) {

        var message = new ReadingMessage(
            request.meter_number,
            request.Readings.Select(r => new ReadingItem(r.Key, r.Value)).ToList()
            );

        await  _publisher.PublishAsync(message);

    }
}
