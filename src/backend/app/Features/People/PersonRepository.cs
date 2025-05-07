using app.Domain;
using app.Domain.Types;
using app.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace app.Features.People;

public class PersonRepository(IDbContextFactory contextFactory) : IPersonRepository
{
    public async Task<Person> CreatePerson(Name name, CancellationToken cancellationToken)
    {
        Person person = new()
        {
            Id = GuidFactory<PersonId>.NewSequential(),
            Name = name
        };
        using var context = contextFactory.CreateDbContext();
        context.People.Add(person);
        await context.SaveChangesAsync(cancellationToken);
        return person;
    }

    public Task DeletePerson(PersonId id, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        var person = context.People.Find(id);
        if (person != null)
        {
            context.People.Remove(person);
            return context.SaveChangesAsync(cancellationToken);
        }
        return Task.CompletedTask;
    }

    public Task<List<Person>> GetAll(PersonId? lastPersonId, int pageSize, string? searchTerm, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        var lastIdOrEmpty = lastPersonId ?? PersonId.Empty;
        return context.People
            .OrderBy(x => x.Id)
            .Where(x => string.IsNullOrEmpty(searchTerm) || x.Name.Value.Contains(searchTerm))
            .Where(x => x.Id.Value> lastIdOrEmpty.Value)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Person?> GetById(PersonId id, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        return await context.People.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
