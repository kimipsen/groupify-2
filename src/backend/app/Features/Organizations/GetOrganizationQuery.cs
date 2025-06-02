using app.Domain;
using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.Organizations;

public class GetOrganizationQuery
{
    public OrganizationId Id { get; init; } = OrganizationId.Empty;
}

public class GetOrganizationQueryHandler(IOrganizationRepository repository) : IQueryHandler<GetOrganizationQuery, Organization?>
{
    public async Task<Organization?> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetById(request.Id, cancellationToken);
    }
}