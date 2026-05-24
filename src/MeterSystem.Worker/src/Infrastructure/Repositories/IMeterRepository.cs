namespace MeterSystem.Worker.src.Infrastructure.Repositories;

public interface IMeterRepository
{
    Task<long> GetOrCreateMeterAsync(long meterNumber);
}
