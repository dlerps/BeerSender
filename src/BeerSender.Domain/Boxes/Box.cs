using BeerSender.Domain.Boxes.Events;

namespace BeerSender.Domain.Boxes;

public class Box
{
    public Guid Id { get; set; }
    public BoxCapacity? BoxType { get; set; }
    public List<string> Bottles { get; set; } = new();

    public static Box Create(BoxCreated created)
    {
        return new Box()
        {
            BoxType = created.Capacity
        };
    }
    
    public void Apply(BottlesUpdated @event)
    {
        Bottles = @event.Bottles.ToList();
    }
    
    public bool IsFull()
    {
        if (BoxType is null)
            throw new InvalidOperationException("Box type must be set before checking if full.");

        return Bottles.Count >= BoxType.NumberOfSpots;
    }
}

public record ShippingLabel(Carrier Carrier, string TrackingCode)
{
    public bool IsValid()
    {
        return Carrier switch
        {
            Carrier.UPS => TrackingCode.StartsWith("ABC"),
            Carrier.FedEx => TrackingCode.StartsWith("DEF"),
            Carrier.PostDanmark => TrackingCode.StartsWith("GHI"),
            _ => throw new ArgumentOutOfRangeException(nameof(Carrier), Carrier.ToString(), null),
        };
    }
}

public enum Carrier
{
    UPS,
    FedEx,
    PostDanmark 
}