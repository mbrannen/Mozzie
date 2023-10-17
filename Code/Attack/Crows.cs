namespace Mozzie.Code.Attack;

class Crows : AttackBase
{
    public override string Name => "Crows";

    public override string Description => "A swarm of friendly crows surrounds you.";

    public override float Timer => 2f;

    public override int Damage => 2;

    public override int Count => 3;
}