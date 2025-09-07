using Bsv.Database.Migrator.Abstract;
using Npgsql;

namespace Bsv.Database.Migrator.Implementation.Migrators;

public class PostrgeSqlDatabaseMigrator : IDatabaseMigrator
{
    private const string _tableName = "bsv_migrations";

    public Task ExecuteMigrationAsync(string connectionString, string sqlQuery, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    public async Task InitMigrationTableAsync(string connectionString, CancellationToken cancellation)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellation);

        string createTableSql = $"""
            CREATE TABLE IF NOT EXISTS {_tableName} (
                id INT PRIMARY KEY,
                file_name VARCHAR(255) UNIQUE NOT NULL,
                hash VARCHAR(50) UNIQUE NOT NULL,
                execute_date_time TIMESTAMPTZ NOT NULL
            );
            """;

        await using var cmd = new NpgsqlCommand(createTableSql, connection);
        await cmd.ExecuteNonQueryAsync(cancellation);
    }
}
