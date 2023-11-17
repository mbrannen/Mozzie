using Mozzie.Code.Player;

namespace Mozzie.Code.Buffs.Speed.BlackLeaf;

public class BlackLeafExtract : PercentageBuffBase
{
    public override BuffAffectType BuffAffectType => BuffAffectType.Player;
    public override string Name => "Black Leaf Extract";
    public override StatType StatType { get; }
    public override Rarity Rarity => Rarity.Uncommon;
    public override float Percent => .10f;
}