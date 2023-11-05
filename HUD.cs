using Godot;
using Mozzie.Code.Pickups;
using Mozzie.Code.Player;

namespace Mozzie;

public partial class HUD : CanvasLayer
{
	[Export] public TextureProgressBar ExperienceBar;
	[Export] public TextureProgressBar HealthBar;
	[Export] public StatManager StatManager;

	public override void _EnterTree()
	{
		StatManager.ExperienceChanged += AdjustExperience;
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
}