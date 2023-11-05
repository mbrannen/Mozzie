using Godot;
using Mozzie.Code.Player;

namespace Mozzie;
public partial class StatManager : Node
{
	[Export] public PickupManager PickupManager;
	[Export] public Player Player;
	
	public delegate void ExperienceChangedDelegate(int value);
	public event ExperienceChangedDelegate ExperienceChanged;
	
	public delegate void HealthChangedDelegate(int value);
	public event HealthChangedDelegate HealthChanged;

	public override void _Ready()
	{
		//emit signal to init the values
		ExperienceChanged?.Invoke(Player.Experience);
		HealthChanged?.Invoke(Player.Health);
		
		foreach (var pickup in PickupManager.Pickups)
		{
			pickup.PickedUp += StatChanged;
		}
	}
	
	public override void _Process(double delta)
	{
		
	}

	private void StatChanged(StatType statType, int value)
	{
		switch (statType)
		{
			case StatType.Health:
				AdjustHealth(value);
				break;
			case StatType.Experience:
				AdjustExperience(value);
				break;
				
		}
	}
	//Adjust player's experience stat and emit an event for HUD to update
	private void AdjustExperience(int value)
	{
		Player.Experience += value;
		ExperienceChanged?.Invoke(Player.Experience);
	}
	//Adjust player's health stat and emit an event for HUD to update
	private void AdjustHealth(int value)
	{
		Player.Health += value;
		HealthChanged?.Invoke(Player.Health);
	}
}
