namespace Bsv.Database.Migrator.Abstract;

internal interface IDatabaseMigrator
{
    Task ExecuteMigrationAsync(string connectionString, string sqlQuery, CancellationToken cancellation);

    Task InitMigrationTableAsync(string connectionString, CancellationToken cancellation);
}
