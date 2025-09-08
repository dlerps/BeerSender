namespace BeerSender.Domain.Boxes.Events;

public record BoxCreated(Guid BoxId, BoxCapacity Capacity);

public record ShippingLabelAdded(ShippingLabel Label);

public record FailedToAddShippingLabel(FailedToAddShippingLabel.FailReason Reason)
{
    public enum FailReason
    {
        InvalidCarrier,
        InvalidShippingLabel
    }
}

public record BottlesAdded(string[] Bottles);
