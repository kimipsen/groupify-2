using app.Domain;
using app.Domain.Types;

namespace app.Features.People;

public interface IPersonRepository
{
    Task<Person?> GetById(PersonId id, CancellationToken cancellationToken);
}
