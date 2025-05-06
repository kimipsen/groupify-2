using app.Persistence.Configuration;
using app.Persistence.DbContext;

namespace app.Setup;

public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddWithValidation<ConnectionStrings, ConnectionStringsValidator>(ConnectionStrings.SectionName);
        services.AddWithValidation<DatabaseSettings, DatabaseSettingsValidator>(DatabaseSettings.SectionName);
        
        services.AddDbContext<AppDbContext>();
        services.AddDbContext<PostgreSqlDbContext>();
        services.AddDbContext<SqliteDbContext>();
        services.AddScoped<IDbContextFactory, DbContextFactory>();
        return services;
    }
}
