using app.Domain;
using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.People;

public class GetPeopleQuery : PaginatedQuery<PersonId> {}

public class GetPeopleQueryHandler(IPersonRepository repository) : IQueryHandler<GetPeopleQuery, IEnumerable<Person>>
{
    public async Task<IEnumerable<Person>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAll(request.LastId, request.PageSize, request.SearchTerm, cancellationToken);
    }
}