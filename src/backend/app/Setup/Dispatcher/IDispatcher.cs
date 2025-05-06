namespace app.Setup.Dispatcher;

public interface IDispatcher
{
    Task Send<TCommand>(TCommand command);
    Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken);
}