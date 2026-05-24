using Npgsql;

namespace MeterSystem.Worker.src.Infrastructure.Data;

public class DbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Postgres");
    }

    public NpgsqlConnection Create()
        => new NpgsqlConnection(_connectionString);
}
