using Mozzie.Code.Player;

namespace Mozzie.Code.Buffs.Health.StimPack;

public class LargeStimPack : AdditiveBuffBase
{
    public override BuffAffectType BuffAffectType => BuffAffectType.Player;
    public override string Name => "Stim Pack";
    public override StatType StatType => StatType.Health;
    public override Rarity Rarity => Rarity.Uncommon;
    public override int Value => 10;
}