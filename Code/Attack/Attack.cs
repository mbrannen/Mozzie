using System;
using Godot;
using Mozzie.Code.Talents;

namespace Mozzie.Code.Attack;

public partial class Attack : Node2D
{
	[Export] public GradientTexture1D AnimationFade;
	[Export] public AnimatedSprite2D Animation;
	[Export] public Curve AttackCurve;
	private double _percentageProgress;
	private PlayerDirection _direction;
	[Export()] public AttackNames AttackNameDropdown;
	[Export] public Timer AttackTimer;
	
	private AttackBase _attack;
	public Player Player;

	public int Damage;
	public bool CanHit;

	public override void _Ready()
	{
		_attack =  GetAttack(AttackNameDropdown);
		//sets the timer on the scene to match the attack's timer
		AttackTimer.WaitTime = _attack.Timer;
	}
	
	public override void _Process(double delta)
	{
		_percentageProgress = (_attack.Timer - AttackTimer.TimeLeft) / _attack.Timer;
		CalculateCanHit();
		AdjustOpacity();
		Move(delta);
		
	}

	private void CalculateCanHit()
	{
		CanHit = SampleCurve() > 0;
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
		Reset();
		CastAttack();
	}

	private void GetPlayerDirection()
	{
		_direction = Player.Direction;
	}

	private void Reset()
	{
		GlobalPosition = Player.AttackRootMarker.GlobalPosition;
	}

	private void CastAttack()
	{
		Damage = _attack.CalculateDamage();
		
		Animation.Play();
		if (_attack.CanBeRotated)
			RotateTowardsPlayerDirection();


	}

	private void RotateTowardsPlayerDirection()
	{
		if (_direction == PlayerDirection.Up)
			Animation.Rotation = Mathf.DegToRad(270);
		if (_direction == PlayerDirection.Down)
			Animation.Rotation = Mathf.DegToRad(90);
		if (_direction == PlayerDirection.Left)
			Animation.Rotation = Mathf.DegToRad(180);
		if (_direction == PlayerDirection.Right)
			Animation.Rotation = Mathf.DegToRad(0);
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
		return AttackCurve.Sample((float)(_percentageProgress * 256)/256);
	}
	
}
