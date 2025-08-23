namespace Bsv.Database.Migrator;

public class BsvMigratorConfiguration
{
    public required string DatabaseType { get; set; }

    public required string MigrationFolder { get; set; }

    public required string ConnectionString { get; set; }
}
