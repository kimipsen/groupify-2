using app.Models.Types;

namespace app.Models;

public class Organization
{
    public OrganizationId Id { get; set; } = GuidFactory<OrganizationId>.NewSequential();
    public Name Name { get; set; } = Name.From("Defualt Organization");
    public List<Person> People { get; set; } = [];
}
