using Mozzie.Code.Player;

namespace Mozzie.Code.Buffs;

public abstract class PercentageBuffBase : IBuff
{
    public abstract BuffAffectType BuffAffectType { get; }
    public abstract string Name { get; }
    public abstract StatType StatType { get; }
    public abstract Rarity Rarity { get; }
    public abstract float Percent { get; }
}