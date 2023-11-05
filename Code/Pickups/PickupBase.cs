using Mozzie.Code.Player;

namespace Mozzie.Code.Pickups;

public abstract class PickupBase : IPickup
{
    public virtual string Name => "Not Implemented";
    public virtual string Description => "Not Implemented";
    public abstract StatType StatType { get; }
    public abstract int Value { get; }
    
}