using Godot;
using Mozzie.Code.Attack;
using Mozzie.Code.Utility;

namespace Mozzie.Code.Enemy;

public partial class Enemy : Node2D
{
	[Export] public EnemyType EnemyTypeDropdown;
	[Export] public Area2D CollisionArea;
	[Export] public Node2D NodeDamageTexts;
	[Export] public PackedScene DamageText;
	[Export] public Marker2D MarkerDamageText;
	[Export] public Timer StateTimer;
	public EnemyBase EnemyBase;
	
	public delegate void EnemyDiedDelegate(EnemyType type, Vector2 position);
	public event EnemyDiedDelegate EnemyDied;
	
	public delegate void DamagedPlayerDelegate(int damage);

	public event DamagedPlayerDelegate DamagedPlayer;
	

	public override void _EnterTree()
	{
		EnemyBase = GetEnemy(EnemyTypeDropdown);
		CollisionArea.AreaEntered += OnBodyEntered;
		StateTimer.Timeout += StateTimerOnTimeout;
		
	}

	public override void _Ready()
	{
		EnemyBase = GetEnemy(EnemyTypeDropdown);
		EnemyBase.Position = Position;
		

		

	}
	
	public override void _PhysicsProcess(double delta)
	{
		EnemyBase.Navigate((float)delta);
		UpdatePosition(EnemyBase.Position);
		DetermineIfDead();
	}

	private EnemyBase GetEnemy(EnemyType enemyType)
	{
		switch (enemyType)
		{
			case EnemyType.ZombieKid:
				return new ZombieKid();
			default:
				return null;
		}
	}
	
	private void StateTimerOnTimeout()
	{
		EnemyBase.ChangePursuitState();
	}

	private void UpdatePosition(Vector2 newPosition)
	{
		Position = newPosition;
	}

	private void DetermineIfDead()
	{
		if (EnemyBase.IsDead)
			IsDead();
	}

	private void IsDead()
	{
		EnemyDied?.Invoke(EnemyTypeDropdown, GlobalPosition);
		QueueFree();
	}

	public void TakeDamage(Damage damage)
	{
		EnemyBase.TakeDamage(damage.DamageAmount);
		RunDamageText(damage);
	}

	private void RunDamageText(Damage damage)
	{
		var damageText = DamageText.Instantiate<DamageText>();
		damageText.SetDamage(damage);
		NodeDamageTexts.AddChild(damageText);
		damageText.Position = MarkerDamageText.GlobalPosition;
	}

	private void OnBodyEntered(Area2D body)
	{
		var parent = body.GetParent();

		if (parent.IsInGroup(Groups.PLAYER))
		{
			OnDamagePlayer(EnemyBase.Damage);
			if (EnemyBase.State == EnemyState.Pursuit)
			{
				EnemyBase.ChangePursuitState();
				StateTimer.Start();	
			}
		}
	}

	private void OnDamagePlayer(int enemyBaseDamage)
	{
		DamagedPlayer?.Invoke(enemyBaseDamage);
	}
}
