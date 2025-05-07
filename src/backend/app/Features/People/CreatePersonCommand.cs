using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.People;

public class CreatePersonCommand
{
    public Name Name { get; init; } = Name.Unspecified;
}

public class CreatePersonCommandHandler(IPersonRepository repository) : ICommandHandler<CreatePersonCommand, PersonId>
{
    public async Task<PersonId> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        return (await repository.CreatePerson(command.Name, cancellationToken)).Id;
    }
}