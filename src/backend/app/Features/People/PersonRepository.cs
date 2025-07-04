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

    public async Task DeletePerson(PersonId id, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        var person = context.People.Find(id);
        if (person is not null)
        {
            context.People.Remove(person);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Person>> GetAll(PersonId lastPersonId, int pageSize, SearchTerm searchTerm, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        return await context.People
            .OrderBy(x => x.Id)
            .Where(x => searchTerm.Equals(SearchTerm.Empty) || ((string)x.Name).Contains(searchTerm.Value))
            .Where(x => x.Id > lastPersonId)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Person?> GetById(PersonId id, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        return await context.People.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task UpdatePerson(PersonId id, Name name, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        var person = await context.People.FindAsync(id, cancellationToken);
        if (person != null)
        {
            person.Name = name;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
