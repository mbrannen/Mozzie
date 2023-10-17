using Godot;

namespace Mozzie.Code.Attack;

public sealed class Cough : AttackBase
{
	public override string Name => "Cough";
	public override string Description => "An infectious cough...";
	public override float Timer => 2f;
	public override int Damage => 2;
	
}