namespace MeterSystem.Api.src.DTOs;

public record ReadingRequest(long meter_number, Dictionary<DateTime, double> Readings);
