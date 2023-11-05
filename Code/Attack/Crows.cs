using Mozzie.Code.Talents;

namespace Mozzie.Code.Attack;

public sealed class Crows : AttackBase
{
    public Crows(TalentModel talents) : base(talents)
    {
    }

    public override string Name => "Crows";
    public override string Description => "A swarm of friendly crows surrounds you.";

    public override float BaseTimer => 6f;
    public override int BaseDamage => 10;
    public override int BaseCount => 3;
    public override float BaseCriticalChance => 0.05f;
    public override bool CanBeRotated => false;
    public override bool CanMove => false;
    public override bool IsRootedToPlayer => true;
}