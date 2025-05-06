namespace app.Setup.Handlers;

public interface ICommandHandler<TCommand>
{
    Task Handle(TCommand command);
}