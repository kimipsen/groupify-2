using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.Organizations;

public class DeleteOrganizationCommand
{
    public OrganizationId Id { get; set; }
}

public class DeleteOrganizationCommandHandler(IOrganizationRepository repository) : ICommandHandler<DeleteOrganizationCommand>
{
    public async Task Handle(DeleteOrganizationCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteOrganization(command.Id, cancellationToken);
    }
}