using Mozzie.Code.Talents;

namespace Mozzie.Code.Attack;

public sealed class Shiv : AttackBase
{
    public Shiv(TalentModel talents) : base(talents)
    {
    }

    public override string Name => "Shiv";
    public override string Description => "A broken shiv";
    
    public override float BaseTimer => 1f;
    public override int BaseDamage => 5;
    public override int BaseCount => 1;
    public override float BaseCriticalChance => 0.10f;
    public override bool CanBeRotated => true;
    public override bool CanMove => false;
    public override bool IsRootedToPlayer => false;
}