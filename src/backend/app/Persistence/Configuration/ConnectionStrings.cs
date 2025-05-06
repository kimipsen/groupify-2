namespace app.Persistence.Configuration;

public class ConnectionStrings
{
    public const string SectionName = nameof(ConnectionStrings);
    public string PostgreSQLConnection { get; set; } = string.Empty;
    public string SQLiteConnection { get; set; } = string.Empty;
}
