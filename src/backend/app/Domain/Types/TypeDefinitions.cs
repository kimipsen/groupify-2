using Vogen;

namespace app.Domain.Types;

[Instance("Empty", "")]
[ValueObject<string>(Conversions.SystemTextJson)]
public readonly partial struct SearchTerm {}

[Instance("Empty", "")]
[ValueObject<Guid>(Conversions.EfCoreValueConverter | Conversions.SystemTextJson)]
public readonly partial struct PersonId 
{
    public static bool operator >(PersonId left, PersonId right) => left.CompareTo(right) > 0;
    public static bool operator <(PersonId left, PersonId right) => left.CompareTo(right) < 0;
    public static bool operator >=(PersonId left, PersonId right) => left.CompareTo(right) >= 0;
    public static bool operator <=(PersonId left, PersonId right) => left.CompareTo(right) <= 0;
}

[Instance("Empty", "")]
[ValueObject<Guid>(Conversions.EfCoreValueConverter | Conversions.SystemTextJson)]
public readonly partial struct OrganizationId
{
    public static bool operator >(OrganizationId left, OrganizationId right) => left.CompareTo(right) > 0;
    public static bool operator <(OrganizationId left, OrganizationId right) => left.CompareTo(right) < 0;
    public static bool operator >=(OrganizationId left, OrganizationId right) => left.CompareTo(right) >= 0;
    public static bool operator <=(OrganizationId left, OrganizationId right) => left.CompareTo(right) <= 0;
}

[Instance("Unspecified", "Unspecified name")]
[ValueObject<string>(Conversions.EfCoreValueConverter | Conversions.SystemTextJson, stringComparers: StringComparersGeneration.Generate)]
public readonly partial struct Name
{
    private static Validation Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Validation.Invalid("Name cannot be empty or whitespace.");

        if (value.Length > 100)
            return Validation.Invalid("Name cannot be longer than 100 characters.");

        if (value == Unspecified)
            return Validation.Invalid("Name cannot be 'Unspecified name'.");

        return Validation.Ok;
    }
}


public static class GuidFactory<TSelf>
    where TSelf : IVogen<TSelf, Guid>
{
    public static TSelf NewSequential()
    {
        return TSelf.From(Guid.CreateVersion7());
    }
}