namespace app.Setup.Dispatcher;

public interface IDispatcher
{
    Task Send<TCommand>(TCommand command, CancellationToken cancellationToken);
    Task<TResult> Send<TCommand, TResult>(TCommand command, CancellationToken cancellationToken);
    Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken);
}