using Vogen;

namespace app.Models.Types;

[ValueObject<Guid>(Conversions.EfCoreValueConverter | Conversions.SystemTextJson)]
public partial struct PersonId {}

[ValueObject<Guid>(Conversions.EfCoreValueConverter | Conversions.SystemTextJson)]
public partial struct OrganizationId {}

[ValueObject<string>(Conversions.EfCoreValueConverter | Conversions.SystemTextJson)]
public partial struct Name {}


public static class GuidFactory<TSelf>
    where TSelf : IVogen<TSelf, Guid>
{
    public static TSelf NewSequential()
    {
        return TSelf.From(Guid.CreateVersion7());
    }
}