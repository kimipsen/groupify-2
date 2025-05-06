using app.Domain;
using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.People;

public class GetPersonQuery
{
    public PersonId Id { get; init; }
}

public class GetPersonQueryHandler(IPersonRepository repository) : IQueryHandler<GetPersonQuery, Person?>
{
    public async Task<Person?> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetById(request.Id, cancellationToken);
    }
}