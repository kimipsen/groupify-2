using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.People;

public class UpdatePersonCommand
{
    public PersonId Id { get; set; } = PersonId.Empty;
    public Name Name { get; set; } = Name.Unspecified;
}

public class UpdatePersonCommandHandler(IPersonRepository repository) : ICommandHandler<UpdatePersonCommand>
{
    public async Task Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        await repository.UpdatePerson(command.Id, command.Name, cancellationToken);
    }
}