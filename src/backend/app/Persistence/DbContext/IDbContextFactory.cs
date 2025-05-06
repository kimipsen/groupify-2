namespace app.Persistence.DbContext;

public interface IDbContextFactory
{
    AppDbContext CreateDbContext();
}