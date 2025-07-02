using app.Domain;
using app.Domain.Types;

namespace app.Features.People;

public interface IPersonRepository
{
    Task<Person?> GetById(PersonId id, CancellationToken cancellationToken);
    Task<Person> CreatePerson(Name name, CancellationToken cancellationToken);
    Task DeletePerson(PersonId id, CancellationToken cancellationToken);
    Task<List<Person>> GetAll(PersonId lastPersonId, int pageSize, SearchTerm searchTerm, CancellationToken cancellationToken);
    Task UpdatePerson(PersonId id, Name name, CancellationToken cancellationToken);
}
