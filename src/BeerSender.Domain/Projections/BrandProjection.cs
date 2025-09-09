using BeerSender.Domain.Boxes.Events;
using JasperFx.Events;
using Marten.Events.Projections;

namespace BeerSender.Domain.Projections;

public class Brand
{
    public string Name { get; set; }
    public int TotalBottleCount { get; set; }
    public List<Guid> BoxIds { get; set; } = new();
}

public class BrandProjection : MultiStreamProjection<Brand, string>
{
    public BrandProjection()
    {
        Identity<BottlesAdded>(ba => ba.Bottles.First());
    }
    public static Brand Create(IEvent<BottlesAdded> @event)
    {
        return new Brand
        {
            Name = @event.Data.Bottles.First(),
            TotalBottleCount = @event.Data.Bottles.Length,
            BoxIds = [ @event.StreamId ]
        };
    }
    
    public Brand Apply(IEvent<BottlesAdded> @event, Brand view)
    {
        if (!view.BoxIds.Contains(@event.StreamId))
            view.BoxIds.Add(@event.StreamId);
        
        view.TotalBottleCount += @event.Data.Bottles.Length;
        
        return view;
    }
}