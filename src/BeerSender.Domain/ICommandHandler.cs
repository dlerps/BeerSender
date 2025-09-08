using Marten;

namespace BeerSender.Domain;

public interface ICommandHandler<in TCommand> 
    : ICommandHandler
    where TCommand : class, ICommand 
{
    Task Handle(IDocumentSession session, TCommand command);
}

public interface ICommandHandler
{
    Task Handle(IDocumentSession session, ICommand command);
}

public abstract class AbstractCommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    public async Task Handle(IDocumentSession session, ICommand command)
        => await Handle(session, (TCommand)command);

    public abstract Task Handle(IDocumentSession session, TCommand command);
}

public interface ICommand;