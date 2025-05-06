using app.Domain.Types;

namespace app.Domain;

public class Person
{
    public PersonId Id { get; set; } = GuidFactory<PersonId>.NewSequential();
    public Name Name { get; set; } = Name.Unspecified;
    public List<Organization> Organizations { get; set; } = [];
}
