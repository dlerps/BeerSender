using BeerSender.Domain.Boxes.Events;
using Marten;

namespace BeerSender.Domain.Boxes.Commands;

public record AddBottle(Guid BoxId, string BeerBrand, short Quantity = 1)
    : ICommand;

public class AddBottleHandler : AbstractCommandHandler<AddBottle>
{
    public override async Task Handle(IDocumentSession session, AddBottle command)
    {
        var stream = await session.Events.FetchForWriting<Box>(command.BoxId);
        var box = stream.Aggregate;

        if (box is null)
            return;
        if (box.HasSpace(command.Quantity))
            return;
        
        stream.AppendOne(new BottlesAdded(
            Enumerable
                .Range(0, command.Quantity)
                .Select(_ => command.BeerBrand)
                .ToArray())
        );
    }
}