using app.Domain;
using app.Domain.Types;
using app.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace app.Features.People;

public class PersonRepository(IDbContextFactory contextFactory) : IPersonRepository
{
    public async Task<Person?> GetById(PersonId id, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        return await context.People.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
