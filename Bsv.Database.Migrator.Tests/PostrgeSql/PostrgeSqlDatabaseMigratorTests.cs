using Bsv.Database.Migrator.Implementation.Migrators;
using Npgsql;

namespace Bsv.Database.Migrator.Tests.PostrgeSql;

internal class PostrgeSqlDatabaseMigratorTests
{
    private readonly PostrgeSqlDatabaseMigrator _migrator;
    private const string _connectionString = "Host=localhost;Username=net_user;Password=123456;Database=postgres;Port=5432;";
    public PostrgeSqlDatabaseMigratorTests()
    {
        _migrator = new();
    }

    [Test]
    public async Task Init_ShouldCreateTable()
    {
        // Act
        await _migrator.InitMigrationTableAsync(_connectionString, CancellationToken.None);

        // Arrange
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(CancellationToken.None);

        string createTableSql = "SELECT COUNT(1) FROM bsv_migrations;";

        await using var cmd = new NpgsqlCommand(createTableSql, connection);
        var result = await cmd.ExecuteScalarAsync(CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(0));
    }
}
