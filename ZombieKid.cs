using Godot;
using Mozzie.Code.Enemy;

namespace Mozzie;

public partial class ZombieKid : EnemyBase
{
    public ZombieKid() : base()
    {
    }

    public override string Name => "ZombieKid";
    public override string Description => "A small annoying little fucker.";
    public override EnemyType Type => EnemyType.ZombieKid;
    public override int BaseHealth => 6;
    public override int BaseDamage => 5;
    public override float BaseSpeed => 50f;
}
