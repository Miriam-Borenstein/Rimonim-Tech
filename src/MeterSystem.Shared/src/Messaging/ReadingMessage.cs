namespace MeterSystem.Shared.src.Messaging;

public record ReadingItem(DateTime Timestamp, double Value);

public record ReadingMessage(long MeterNumber, List<ReadingItem> Readings);
