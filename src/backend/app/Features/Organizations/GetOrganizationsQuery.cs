using app.Domain;
using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.Organizations;

public class GetOrganizationsQuery : PaginatedQuery<OrganizationId> { }

public class GetOrganizationsQueryHandler(IOrganizationRepository repository) : IQueryHandler<GetOrganizationsQuery, IEnumerable<Organization>>
{
    public async Task<IEnumerable<Organization>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAll(request.LastId, request.PageSize, request.SearchTerm, cancellationToken);
    }
}