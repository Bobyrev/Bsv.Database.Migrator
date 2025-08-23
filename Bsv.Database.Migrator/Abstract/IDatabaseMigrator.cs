namespace Bsv.Database.Migrator.Abstract;

internal interface IDatabaseMigrator
{
    Task ExecuteMigrationAsync(string sqlQuery, CancellationToken cancellation);

    Task InitMigrationTableAsync(CancellationToken cancellation);
}
