using app.Persistence.Configuration;

namespace app.Persistence.DbContext;

public class DbContextFactory : IDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public DbContextFactory(IServiceProvider serviceProvider, IOptions<DatabaseSettings> databaseSettings)
    {
        _serviceProvider = serviceProvider;
        _databaseSettings = databaseSettings;
    }

    public AppDbContext CreateDbContext()
    {
        return _databaseSettings.Value.Type switch
        {
            DatabaseType.PostgreSQL => _serviceProvider.GetRequiredService<PostgreSqlDbContext>(),
            DatabaseType.SQLite => _serviceProvider.GetRequiredService<SqliteDbContext>(),
            _ => throw new Exception("Unsupported database type")
        };
    }
}
