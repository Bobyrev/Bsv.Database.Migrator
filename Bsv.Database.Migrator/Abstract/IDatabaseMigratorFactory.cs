using Bsv.Database.Migrator.Enums;

namespace Bsv.Database.Migrator.Abstract;

internal interface IDatabaseMigratorFactory
{
    IDatabaseMigrator Get(DatabaseType databaseType);
}
