using app.Setup.Handlers;

namespace app.Setup.Dispatcher;

public class Dispatcher(IServiceProvider provider) : IDispatcher
{
    public async Task Send<TCommand>(TCommand command)
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.Handle(command);
    }

    public async Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
    {
        var handler = provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.Handle(query, cancellationToken);
    }
}