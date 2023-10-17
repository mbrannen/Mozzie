using System;
using Godot;

namespace Mozzie.Code.Attack;

public partial class AttackImplementation : Node
{
	[Export] public GradientTexture1D AnimationFade;
	[Export] public Attacks Attack;
	private AttackBase _attack;

	public override void _Ready()
	{
		var test = Attack;
		GD.Print(test);
		_attack =  GetAttack(Attack);
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
