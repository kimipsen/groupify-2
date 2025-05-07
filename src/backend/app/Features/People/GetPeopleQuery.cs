using app.Domain;
using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.People;

public class GetPeopleQuery
{
    public PersonId? LastPersonId { get; set; } = null;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
}

public class GetPeopleQueryHandler(IPersonRepository repository) : IQueryHandler<GetPeopleQuery, IEnumerable<Person>>
{
    public async Task<IEnumerable<Person>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAll(request.LastPersonId, request.PageSize, request.SearchTerm, cancellationToken);
    }
}