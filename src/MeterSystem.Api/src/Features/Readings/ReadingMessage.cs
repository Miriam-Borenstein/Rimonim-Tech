using MeterSystem.Api.src.DTOs;

namespace MeterSystem.Api.src.Features.Readings;

public record ReadingItem(DateTime Timestamp, double Value);

public record ReadingMessage(long MeterNumber, List<ReadingItem> Readings);
