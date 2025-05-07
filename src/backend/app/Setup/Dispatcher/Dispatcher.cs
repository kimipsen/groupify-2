using app.Setup.Handlers;

namespace app.Setup.Dispatcher;

public class Dispatcher(IServiceProvider provider) : IDispatcher
{
    public async Task Send<TCommand>(TCommand command, CancellationToken cancellationToken)
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.Handle(command, cancellationToken);
    }

    public async Task<TResult> Send<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return await handler.Handle(command, cancellationToken);
    }

    public async Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
    {
        var handler = provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.Handle(query, cancellationToken);
    }
}