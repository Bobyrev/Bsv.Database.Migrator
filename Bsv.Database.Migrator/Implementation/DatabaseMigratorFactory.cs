using Bsv.Database.Migrator.Abstract;
using Bsv.Database.Migrator.Enums;
using Bsv.Database.Migrator.Implementation.Migrators;

namespace Bsv.Database.Migrator.Implementation;

internal class DatabaseMigratorFactory : IDatabaseMigratorFactory
{
    public IDatabaseMigrator Get(DatabaseType databaseType)
    {
        return databaseType switch
        {
            DatabaseType.PostrgeSql => new PostrgeSqlDatabaseMigrator(),
            _ => throw new NotImplementedException($"For type: {databaseType} not implement DatabaseMigrator"),
        };
    }
}
