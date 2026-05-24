

namespace MeterSystem.Shared.src.Messaging.RabbitMq;

public record ReadingItem(DateTime Timestamp, double Value);

public record ReadingMessage(long MeterNumber, List<ReadingItem> Readings);
