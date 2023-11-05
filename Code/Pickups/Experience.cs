using Godot;
using Mozzie.Code.Player;

namespace Mozzie.Code.Pickups;

public class Experience : PickupBase
{
    public override StatType StatType => StatType.Experience;
    public override int Value => 10;
    
}