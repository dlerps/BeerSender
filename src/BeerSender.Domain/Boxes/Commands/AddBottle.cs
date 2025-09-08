using BeerSender.Domain.Boxes.Events;
using Marten;

namespace BeerSender.Domain.Boxes.Commands;

public record AddBottle(Guid BoxId, string BeerBrand)
    : ICommand;

public class AddBottleHandler : AbstractCommandHandler<AddBottle>
{
    public override async Task Handle(IDocumentSession session, AddBottle command)
    {
        var stream = await session.Events.FetchForWriting<Box>(command.BoxId);
        var box = stream.Aggregate;

        if (box is null)
            return;
        if (box.IsFull())
            return;
        
        stream.AppendOne(new BottlesUpdated([..box.Bottles, command.BeerBrand]));
    }
}