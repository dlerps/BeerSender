using Marten;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BeerSender.Domain;

public class CommandRouter(
    IServiceProvider serviceProvider, IDocumentStore store)
{
    public async Task HandleCommand(ICommand command)
    {
        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
        var handler = serviceProvider.GetService(handlerType) as ICommandHandler;
        var methodInfo = handlerType.GetMethod("Handle");
        
        if (handler is null)
            throw new InvalidOperationException($"No handler found for command type {commandType.Name}");

        var session = store.IdentitySession();

        await handler.Handle(session, command);
        
        await session.SaveChangesAsync();
    }
}