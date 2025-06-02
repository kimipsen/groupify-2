using app.Domain;
using app.Domain.Types;

namespace app.Features.Organizations;

public interface IOrganizationRepository
{
    Task<Organization?> GetById(OrganizationId id, CancellationToken cancellationToken);
    Task<Organization> CreateOrganization(Name name, CancellationToken cancellationToken);
    Task DeleteOrganization(OrganizationId id, CancellationToken cancellationToken);
    Task<List<Organization>> GetAll(OrganizationId? lastOrganizationId, int pageSize, SearchTerm searchTerm, CancellationToken cancellationToken);
    Task UpdateOrganization(OrganizationId id, Name name, CancellationToken cancellationToken);
}
