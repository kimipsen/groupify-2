namespace app.Database.Options;

public enum DatabaseType
{
    PostgreSQL,
    SQLite
}

public class DatabaseSettings
{
    public const string SectionName = nameof(DatabaseSettings);
    public DatabaseType Type { get; set; }
}
