using app.Domain.Types;

namespace app.Domain;

public class Organization
{
    public OrganizationId Id { get; set; } = GuidFactory<OrganizationId>.NewSequential();
    public Name Name { get; set; } = Name.Unspecified;
    public List<Person> People { get; set; } = [];
}
