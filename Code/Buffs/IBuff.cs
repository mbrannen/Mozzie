using Mozzie.Code.Player;

namespace Mozzie.Code.Buffs;

public interface IBuff
{
    public BuffAffectType BuffAffectType { get;}
    public string Name { get; }
    public StatType StatType { get; }
    public Rarity Rarity { get; }
}