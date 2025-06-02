using app.Domain;
using app.Domain.Types;
using app.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace app.Features.Organizations;

public class OrganizationRepository(IDbContextFactory contextFactory) : IOrganizationRepository
{
    public async Task<Organization> CreateOrganization(Name name, CancellationToken cancellationToken)
    {
        Organization organization = new()
        {
            Id = GuidFactory<OrganizationId>.NewSequential(),
            Name = name
        };
        using var context = contextFactory.CreateDbContext();
        context.Organizations.Add(organization);
        await context.SaveChangesAsync(cancellationToken);
        return organization;
    }

    public async Task DeleteOrganization(OrganizationId id, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        var organization = await context.Organizations.FindAsync(new object[] { id }, cancellationToken);
        if (organization is not null)
        {
            context.Organizations.Remove(organization);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Organization>> GetAll(OrganizationId? lastOrganizationId, int pageSize, SearchTerm searchTerm, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        var lastIdOrEmpty = lastOrganizationId ?? OrganizationId.Empty;
        return await context.Organizations
            .OrderBy(x => x.Id)
            .Where(x => string.IsNullOrEmpty(searchTerm.Value) || ((string)x.Name).Contains(searchTerm.Value))
            .Where(x => x.Id > lastIdOrEmpty)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Organization?> GetById(OrganizationId id, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        return await context.Organizations.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task UpdateOrganization(OrganizationId id, Name name, CancellationToken cancellationToken)
    {
        using var context = contextFactory.CreateDbContext();
        var organization = await context.Organizations.FindAsync(new object[] { id }, cancellationToken);
        if (organization != null)
        {
            organization.Name = name;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
