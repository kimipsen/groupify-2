using app.Models.Types;

namespace app.Models;

public class Person
{
    public PersonId Id { get; set; } = GuidFactory<PersonId>.NewSequential();
    public Name Name { get; set; } = Name.From("Default person");
    public List<Organization> Organizations { get; set; } = [];
}
