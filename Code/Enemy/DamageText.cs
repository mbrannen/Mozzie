using Godot;
using Mozzie.Code.Attack;

namespace Mozzie.Code.Enemy;

public partial class DamageText : Node2D
{
	[Export] public AnimationPlayer AnimationPlayerDamage;
	[Export] public Label LabelDamage;
	
	
	private const string DEFAULT_ANIMATION = "default";
	private const string CRITICAL_ANIMATION = "critical";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimationPlayerDamage.AnimationFinished += OnAnimationFinished;
		
	}

	public void SetDamage(Damage damage)
	{
		AnimationPlayerDamage.Play(damage.IsCrit ? CRITICAL_ANIMATION : DEFAULT_ANIMATION);
		LabelDamage.Text = $"{damage.DamageAmount}";
	}
	
	public void OnAnimationFinished(StringName animName)
	{
		if( animName == DEFAULT_ANIMATION)
			QueueFree();
		if( animName == CRITICAL_ANIMATION)
			QueueFree();
	}
	
	
}
