using app.Domain;
using app.Domain.Types;
using app.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace app.Persistence.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IOptions<ConnectionStrings> _connectionStrings;

    public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<DatabaseSettings> databaseSettings, IOptions<ConnectionStrings> connectionStrings)
        : base(options)
    {
        _databaseSettings = databaseSettings;
        _connectionStrings = connectionStrings;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var fromEnvironment = Environment.GetEnvironmentVariable("DATABASE__TYPE");
        var gottenFromEnv = Enum.TryParse<DatabaseType>(fromEnvironment, true, out var databaseTypeFromEnv);
        var databaseType = gottenFromEnv ? databaseTypeFromEnv : _databaseSettings.Value.Type;

        if (databaseType == DatabaseType.PostgreSQL)
        {
            var connectionString = _connectionStrings.Value.PostgreSQLConnection;
            optionsBuilder.UseNpgsql(connectionString, options =>
                options.MigrationsHistoryTable("__EFMigrationsHistory_PostgreSQL"));
        }
        else if (databaseType == DatabaseType.SQLite)
        {
            var connectionString = _connectionStrings.Value.SQLiteConnection;
            optionsBuilder.UseSqlite(connectionString, options =>
                options.MigrationsHistoryTable("__EFMigrationsHistory_SQLite"));
        }
        else
        {
            throw new Exception("Unsupported database type");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entities here
        modelBuilder.Entity<Organization>(b => {
            b.HasKey(o => o.Id);
            b.Property(o => o.Id).HasVogenConversion();

            b.Property(o => o.Name).HasVogenConversion();
        });
        modelBuilder.Entity<Person>(b => {
            b.HasKey(p => p.Id);
            b.Property(p => p.Id).HasVogenConversion();

            b.Property(p => p.Name).HasVogenConversion();
        });

        // Add any additional configurations here
    }

    // Define your DbSets here, e.g.,
    // public DbSet<Person> Persons { get; set; }
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<Person> People { get; set; } = null!;
}

public class PostgreSqlDbContext : AppDbContext
{
    public PostgreSqlDbContext(DbContextOptions<AppDbContext> options, IOptions<DatabaseSettings> databaseSettings, IOptions<ConnectionStrings> connectionStrings)
        : base(options, databaseSettings, connectionStrings)
    {
    }
}

public class SqliteDbContext : AppDbContext
{
    public SqliteDbContext(DbContextOptions<AppDbContext> options, IOptions<DatabaseSettings> databaseSettings, IOptions<ConnectionStrings> connectionStrings)
        : base(options, databaseSettings, connectionStrings)
    {
    }
}