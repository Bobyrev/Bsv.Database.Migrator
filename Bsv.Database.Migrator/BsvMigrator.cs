using Bsv.Database.Migrator.Abstract;
using Bsv.Database.Migrator.Enums;
using Bsv.Database.Migrator.Exceptions;

namespace Bsv.Database.Migrator;

internal class BsvMigrator(
    IDatabaseMigratorFactory databaseMigratorFactory)
{
    public async Task StartMigrationsAsync(BsvMigratorConfiguration configuration, CancellationToken cancellation)
    {
        InitialValidate(configuration);

        var databaseType = Enum.Parse<DatabaseType>(configuration.DatabaseType);
        var databaseMigrator = databaseMigratorFactory.Get(databaseType);
        var connectionString = configuration.ConnectionString;

        await databaseMigrator.InitMigrationTableAsync(connectionString, cancellation);

        var sqlFiles = Directory
            .EnumerateFiles(configuration.MigrationFolder, "*.sql")
            .OrderBy(x => x);

        foreach (var sqlFile in sqlFiles)
        {
            if (!cancellation.IsCancellationRequested)
            {
                var sqlQuery = await File.ReadAllTextAsync(sqlFile, cancellation);

                if (string.IsNullOrEmpty(sqlQuery))
                    throw new EmptySqlQueryException($"Not found sql query in {sqlFile}");

                await databaseMigrator.ExecuteMigrationAsync(connectionString, sqlQuery, cancellation);
            }
            else 
            {
                throw new OperationCanceledException($"Canceled on file: {sqlFile}");
            }
        }
    }

    private void InitialValidate(BsvMigratorConfiguration configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException($"Configuration instance is null");

        var databaseType = configuration.DatabaseType;
        var migrationFolder = configuration.MigrationFolder;

        if (string.IsNullOrEmpty(configuration.ConnectionString))
            throw new ArgumentNullException("ConnectionString is empty in configuraion");

        if (string.IsNullOrEmpty(databaseType))
            throw new ArgumentNullException("DatabaseType is empty in configuraion");

        if (string.IsNullOrEmpty(migrationFolder))
            throw new ArgumentNullException("MigrationFolder is empty in configuraion");

        if (!Enum.TryParse<DatabaseType>(databaseType.Trim(), out var _))
            throw new ArgumentException($"Incorrect DatabaseType. Allowed values: {Enum.GetNames<DatabaseType>()}");

        if (!Directory.Exists(migrationFolder))
            throw new DirectoryNotFoundException($"MigrationFolder is not found: {migrationFolder}");
    }
}
