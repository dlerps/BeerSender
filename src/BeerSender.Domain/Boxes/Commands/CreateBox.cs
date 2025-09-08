using Marten;

namespace BeerSender.Domain.Boxes.Commands;

public record CreateBox(
    Guid BoxId,
    int DesiredNumberOfSpots
) : ICommand;

public class CreateBoxHandler
    : AbstractCommandHandler<CreateBox>
{
    public override Task Handle(IDocumentSession session, CreateBox command)
    {
        throw new NotImplementedException();
    }
}