using Godot;
using Mozzie.Code.Pickups;
using Mozzie.Code.Player;

namespace Mozzie;

public partial class HUD : CanvasLayer
{
	[Export] public TextureProgressBar ExperienceBar;
	[Export] public TextureProgressBar HealthBar;
	[Export] public Label LevelLabel;
	[Export] public StatManager StatManager;

	public override void _EnterTree()
	{
		StatManager.LeveledUp += StatManagerOnLeveledUp;
		StatManager.ExperienceChanged += AdjustExperience;
		StatManager.HealthChanged += AdjustHealth;
	}

	private void StatManagerOnLeveledUp(int xpToLevel, byte level)
	{
		ExperienceBar.MaxValue = xpToLevel;
		LevelLabel.Text = level.ToString();
	}

	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		
	}

	private void AdjustExperience(int value)
	{
		ExperienceBar.Value = value;
	}
	
	private void AdjustHealth(int value)
	{
		HealthBar.Value = value;
	}
}