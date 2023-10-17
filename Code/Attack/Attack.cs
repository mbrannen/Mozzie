using System;
using Godot;

namespace Mozzie.Code.Attack;

public partial class Attack : Node
{
	[Export] public GradientTexture1D AnimationFade;
	[Export] public Attacks AttackDropdown;
	private AttackBase _attack;

	public override void _Ready()
	{
		_attack =  GetAttack(AttackDropdown);
		GD.Print(_attack.Name);
		GD.Print(_attack.Description);
	}

	private AttackBase GetAttack(Attacks attack)
	{
		switch (attack)
		{
			case Attacks.Cough:
				return new Cough();
			case Attacks.Crows:
				return new Crows();
			default:
				return null;
			
		}
	}
}
