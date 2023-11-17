using Mozzie.Code.Player;

namespace Mozzie.Code.Buffs.Speed.BlackLeaf;

public class BlackLeaf : AdditiveBuffBase
{
    public override BuffAffectType BuffAffectType => BuffAffectType.Player;
    public override string Name => "Black Leaf";
    public override StatType StatType { get; }
    public override Rarity Rarity => Rarity.Common;
    public override int Value => 10;
}