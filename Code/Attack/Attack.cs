using System;
using Godot;
using Mozzie.Code.Enemy;
using Mozzie.Code.Player;
using Mozzie.Code.Talents;

namespace Mozzie.Code.Attack;

public partial class Attack : Node2D
{
	[Export] public Area2D CollisionArea;
	[Export] public GradientTexture1D AnimationFade;
	[Export] public AnimatedSprite2D Animation;
	[Export] public Curve AttackCurve;
	private double _percentageProgress;
	private PlayerDirection _direction;
	[Export()] public AttackNames AttackNameDropdown;
	[Export] public Timer AttackTimer;
	
	private AttackBase _attack;
	public Mozzie.Player Player;
	private StatManager _statManager = StatManager.Instance;

	public Damage Damage;
	public bool CanHit;

	public override void _Ready()
	{
		_attack =  GetAttack(AttackNameDropdown);
		//sets the timer on the scene to match the attack's timer
		AttackTimer.WaitTime = _attack.Timer;
		CollisionArea.AreaEntered += OnEnemyEntered;
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
	
	public override void _Process(double delta)
	{
		_percentageProgress = (_attack.Timer - AttackTimer.TimeLeft) / _attack.Timer;
		CalculateCanHit();
		if (!CanHit)
			CollisionArea.Monitoring = false;
		Animate();
		if(_attack.CanMove)
			Move(delta);
		else
			FollowPlayer();
	}

	private void CalculateCanHit()
	{
		CanHit = SampleCurve() > 0;
	}
	/// <summary>
	/// Linked to Attack timer in scene
	/// </summary>
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
		CollisionArea.Monitoring = true;
		GlobalPosition = Player.AttackRootMarker.GlobalPosition;
	}

	private void FollowPlayer()
	{
		if (_attack.IsRootedToPlayer)
			GlobalPosition = Player.GlobalPosition;
		else	
			GlobalPosition = Player.AttackRootMarker.GlobalPosition;
		GetPlayerDirection();
		RotateTowardsPlayerDirection();
	}

	private void CastAttack()
	{
		Damage = _attack.CalculateDamage();
		
		Animation.Play();
		if (_attack.CanBeRotated)
			RotateTowardsPlayerDirection();


	}

	#region Visuals
	private void Animate()
	{
		AdjustOpacity();
	}

	private void RotateTowardsPlayerDirection()
	{
		if(!_attack.CanBeRotated)
			return;
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
	

	#endregion

	
	private void Move(double delta)
	{
		var velocity = Vector2.Zero;

		if (_direction == PlayerDirection.Up)
			velocity.Y = -1;
		if (_direction == PlayerDirection.Down)
			velocity.Y = 1;
		if (_direction == PlayerDirection.Left)
			velocity.X = -1;
		if (_direction == PlayerDirection.Right)
			velocity.X = 1;

		Position += velocity * (_statManager.GetStat<int>(StatType.Speed) + 50*SampleCurve())  * (float)delta;
	}

	private float SampleCurve()
	{
		return AttackCurve.Sample((float)(_percentageProgress * 256)/256);
	}
	
	private void OnEnemyEntered(Area2D body)
	{
		var enemy = body.GetParent<Node2D>() as Enemy.Enemy;

		enemy.TakeDamage(Damage);
		
		GD.Print($"{_attack.Name} collided with {enemy.EnemyBase.Name}");
	}
	
}



