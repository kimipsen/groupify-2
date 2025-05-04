using app.Database.Options;
using app.Models;
using app.Models.Types;
using Microsoft.EntityFrameworkCore;

namespace app.Database;

public class AppDbContext : DbContext
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
            b.Property(o => o.Id).HasConversion(new OrganizationId.EfCoreValueConverter());

            b.Property(o => o.Name).HasConversion(new Name.EfCoreValueConverter());
        });
        modelBuilder.Entity<Person>(b => {
            b.Property(p => p.Id).HasConversion(new PersonId.EfCoreValueConverter());

            b.Property(p => p.Name).HasConversion(new Name.EfCoreValueConverter());
        });

        // Add any additional configurations here
    }

    // Define your DbSets here, e.g.,
    // public DbSet<Person> Persons { get; set; }
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<Person> People { get; set; } = null!;
}