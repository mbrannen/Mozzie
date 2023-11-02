using Godot;
using Mozzie.Code.Talents;

namespace Mozzie.Code.Attack;

public sealed class Cough : AttackBase
{
	public Cough(TalentModel talents) : base(talents)
	{
	}

	public override string Name => "Cough";
	public override string Description => "An infectious cough...";
	public override float BaseTimer => 3f;
	public override int BaseDamage => 2;
	public override int BaseCount => 1;
	public override float BaseCriticalChance => 0.25f;
}