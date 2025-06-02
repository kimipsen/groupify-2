using app.Domain.Types;

namespace app.Features.Organizations;

public class CreateOrganizationCommand
{
    public Name Name { get; init; } = Name.Unspecified;
}
