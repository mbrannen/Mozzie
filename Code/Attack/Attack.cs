using System;
using Godot;

namespace Mozzie.Code.Attack;

public partial class Attack : Node
{
	[Export] public GradientTexture1D AnimationFade;
	[Export()] public AttackNames AttackNameDropdown;
	private AttackBase _attack;

	public override void _Ready()
	{
		_attack =  GetAttack(AttackNameDropdown);
		GD.Print(_attack.Name);
		GD.Print(_attack.Description);
	}

	private AttackBase GetAttack(AttackNames attackName)
	{
		switch (attackName)
		{
			case AttackNames.Cough:
				return new Cough();
			case AttackNames.Crows:
				return new Crows();
			default:
				return null;
			
		}
	}
}
