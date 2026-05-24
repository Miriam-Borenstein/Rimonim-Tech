
using Dapper;
using MeterSystem.Shared.src.Messaging;
using MeterSystem.Worker.src.Infrastructure.Data;

namespace MeterSystem.Worker.src.Infrastructure.Repositories;

internal class ReadingRepository : IReadingRepository
{
    private readonly DbConnectionFactory _db;

    public ReadingRepository(DbConnectionFactory db)
    {
        _db = db;
    }
    public async Task InsertReadingAsync(long meterId, ReadingItem reading)
    {
        using var connection = _db.Create();
        await connection.OpenAsync();

        await connection.ExecuteAsync(@"INSERT INTO meter_readings (meter_id, value_at, value) " +
                "VALUES (@meterId, @valueAt, @value) " +
                "ON CONFLICT(meter_id, value_at) DO NOTHING",
                new { meterId, valueAt = reading.Timestamp, value = reading.Value });
    }

}
