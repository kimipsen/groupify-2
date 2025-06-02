using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.Organizations;

public class UpdateOrganizationCommand
{
    public OrganizationId Id { get; set; }
    public Name Name { get; set; }
}

public class UpdateOrganizationCommandHandler(IOrganizationRepository repository) : ICommandHandler<UpdateOrganizationCommand>
{
    public async Task Handle(UpdateOrganizationCommand command, CancellationToken cancellationToken)
    {
        await repository.UpdateOrganization(command.Id, command.Name, cancellationToken);
    }
}