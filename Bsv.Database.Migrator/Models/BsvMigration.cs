namespace Bsv.Database.Migrator.Models;

internal class BsvMigration
{
    public int Id { get; set; }

    public required string FileName { get; set; }

    public required string Hash { get; set; }

    public DateTime ExecuteDateTime { get; set; }
}
