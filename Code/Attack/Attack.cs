using System;
using Godot;
using Mozzie.Code.Talents;

namespace Mozzie.Code.Attack;

public partial class Attack : Node
{
	[Export] public GradientTexture1D AnimationFade;
	[Export()] public AttackNames AttackNameDropdown;
	[Export] public Timer AttackTimer;
	private AttackBase _attack;

	public override void _Ready()
	{
		_attack =  GetAttack(AttackNameDropdown);
		//sets the timer on the scene to match the attack's timer
		AttackTimer.WaitTime = _attack.Timer;
	}

	private AttackBase GetAttack(AttackNames attackName)
	{
		var talents = new TalentModel();
		switch (attackName)
		{
			case AttackNames.Cough:
				return new Cough(talents);
			case AttackNames.Crows:
				return new Crows(talents);
			case AttackNames.Shiv:
				return new Shiv(talents);
			default:
				return null;
		}
	}

	private void OnAttackTimeout()
	{
		CastAttack();
		
	}

	private void CastAttack()
	{
		var damage = _attack.CalculateDamage();
		GD.Print($"!!{_attack.Name}: {damage}!!");
	}
}
