using Mozzie.Code.Talents;

namespace Mozzie.Code.Attack;

public sealed class Crows : AttackBase
{
    public Crows(TalentModel talents) : base(talents)
    {
    }

    public override string Name => "Crows";
    public override string Description => "A swarm of friendly crows surrounds you.";

    public override float BaseTimer => 4f;
    public override int BaseDamage => 2;
    public override int BaseCount => 3;
    public override float BaseCriticalChance => 0.05f;

}