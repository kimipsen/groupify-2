using app.Domain.Types;
using app.Setup.Handlers;

namespace app.Features.People;

public class DeletePersonCommand
{
    public PersonId Id { get; set; }
}

public class DeletePersonCommandHandler(IPersonRepository repository) : ICommandHandler<DeletePersonCommand>
{
    public async Task Handle(DeletePersonCommand command, CancellationToken cancellationToken)
    {
        await repository.DeletePerson(command.Id, cancellationToken);
    }
}
