using System;
using Godot;
using Mozzie.Code.Talents;

namespace Mozzie.Code.Attack;

public partial class Attack : Node2D
{
	[Export] public GradientTexture1D AnimationFade;
	[Export] public AnimatedSprite2D Animation;
	[Export] public Curve VelocityCurve;
	private double _percentageProgress;
	private PlayerDirection _direction;
	[Export()] public AttackNames AttackNameDropdown;
	[Export] public Timer AttackTimer;
	
	private AttackBase _attack;
	public Player Player;

	public override void _Ready()
	{
		_attack =  GetAttack(AttackNameDropdown);
		//sets the timer on the scene to match the attack's timer
		AttackTimer.WaitTime = _attack.Timer;
	}
	
	public override void _Process(double delta)
	{
		_percentageProgress = (_attack.Timer - AttackTimer.TimeLeft) / _attack.Timer;
		AdjustOpacity();
		Move(delta);
		
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
		GetPlayerDirection();
		ResetPosition();
		CastAttack();
	}

	private void GetPlayerDirection()
	{
		_direction = Player.Direction;
	}

	private void ResetPosition()
	{
		GlobalPosition = Player.AttackRootMarker.GlobalPosition;
	}

	private void CastAttack()
	{
		var damage = _attack.CalculateDamage();
		Animation.Play();
	}

	private void AdjustOpacity()
	{
		Animation.Modulate = new Color(Animation.Modulate.R, Animation.Modulate.G, Animation.Modulate.B, SampleCurve());
	}
	
	private void Move(double delta)
	{
		var veloctity = Vector2.Zero;

		if (_direction == PlayerDirection.Up)
			veloctity.Y = -1;
		if (_direction == PlayerDirection.Down)
			veloctity.Y = 1;
		if (_direction == PlayerDirection.Left)
			veloctity.X = -1;
		if (_direction == PlayerDirection.Right)
			veloctity.X = 1;

		Position += veloctity * (Player.Speed + 50*SampleCurve())  * (float)delta;
	}

	private float SampleCurve()
	{
		return VelocityCurve.Sample((float)(_percentageProgress * 256)/256);
	}
	
}
