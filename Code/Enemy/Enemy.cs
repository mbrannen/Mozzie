using Godot;
using Mozzie.Code.Attack;

namespace Mozzie.Code.Enemy;

public partial class Enemy : Node2D
{
	[Export] public EnemyType EnemyTypeDropdown;
	[Export] public Node2D NodeDamageTexts;
	[Export] public PackedScene DamageText;
	[Export] public Marker2D MarkerDamageText;
	public EnemyBase EnemyBase;

	public Enemy()
	{
		//_EnterTree();
		//EnemyBase = GetEnemy(EnemyTypeDropdown);
	}

	public override void _EnterTree()
	{
		EnemyBase = GetEnemy(EnemyTypeDropdown);
	}

	public override void _Ready()
	{
		EnemyBase = GetEnemy(EnemyTypeDropdown);
		EnemyBase.Position = Position;
	}
	
	public override void _Process(double delta)
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

	private void UpdatePosition(Vector2 newPosition)
	{
		Position = newPosition;
	}

	private void DetermineIfDead()
	{
		if(EnemyBase.IsDead)
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
}
