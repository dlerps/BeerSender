using BeerSender.Domain.Boxes.Events;
using Marten.Events.Aggregation;

namespace BeerSender.Domain.Boxes;

public class BoxContent
{
    public Guid Id { get; set; }
    public Dictionary<string, int> ByBrand { get; set; } = new();
}

public class BoxContentProjection : SingleStreamProjection<BoxContent,Guid>
{
    public static BoxContent Create(BoxCreated created)
    {
        return new()
        {
            Id = created.BoxId
        };
    }
    
    public void Apply(BottlesAdded @event, BoxContent view)
    {
        foreach (var brand in @event.Bottles.Distinct())
        {
            var count = @event.Bottles.Count(bottle => bottle == brand);
            if (!view.ByBrand.TryAdd(brand, count))
                view.ByBrand[brand] += count;
        }
        
    }
}