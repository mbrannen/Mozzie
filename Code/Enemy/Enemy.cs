using Godot;
using Mozzie.Code.Attack;

namespace Mozzie.Code.Enemy;

public partial class Enemy : Node2D
{
	[Export] public EnemyNames EnemyNameDropdown;
	[Export] public Node2D NodeDamageTexts;
	[Export] public PackedScene DamageText;
	[Export] public Marker2D MarkerDamageText;
	public EnemyBase EnemyBase;

	

	public Enemy()
	{
		EnemyBase = GetEnemy(EnemyNameDropdown);
	}

	public override void _Ready()
	{
		
		EnemyBase.Position = Position;
	}
	
	public override void _Process(double delta)
	{
		EnemyBase.Navigate((float)delta);
		UpdatePosition(EnemyBase.Position);
		DetermineIfDead();
	}

	private EnemyBase GetEnemy(EnemyNames enemyName)
	{
		switch (enemyName)
		{
			case EnemyNames.ZombieKid:
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
