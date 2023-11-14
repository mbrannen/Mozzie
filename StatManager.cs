using Godot;
using Mozzie.Code.Enemy;
using Mozzie.Code.Player;

namespace Mozzie;
public partial class StatManager : Node
{
	[Export] public PickupManager PickupManager;
	[Export] public EnemyManager EnemyManager;
	[Export] public Curve LevelCurve;
	[Export] public Player Player;
	
	private int XPToLevel { get; set; }
	
	public delegate void LeveledUpDelegate(int xpToLevel, byte level);
	public event LeveledUpDelegate LeveledUp;
	public delegate void ExperienceChangedDelegate(int value);
	public event ExperienceChangedDelegate ExperienceChanged;
	
	public delegate void HealthChangedDelegate(int value);
	public event HealthChangedDelegate HealthChanged;

	public override void _EnterTree()
	{
		PickupManager.NotifyPickedUp += PickupManagerOnNotifyPickedUp;
		EnemyManager.NotifyPlayerDamaged += EnemyManagerOnNotifyPlayerDamaged;
	}

	public override void _Ready()
	{
		//emit signal to init the values
		ExperienceChanged?.Invoke(Player.Experience);
		HealthChanged?.Invoke(Player.Health);
		
		//DetermineInitial experience needed to level
		LevelCurve.BakeResolution = Player.MaxLevel; //editor default to 30 adjust on player class if needed
		LevelCurve.Bake();
		XPToLevel = SampleLevelXPNeeded();
		LeveledUp?.Invoke(XPToLevel, Player.Level);
	}
	
	public override void _Process(double delta)
	{
		
	}
	
	private void EnemyManagerOnNotifyPlayerDamaged(int damage)
	{
		StatChanged(StatType.Health, -damage);
	}

	private void PickupManagerOnNotifyPickedUp(StatType type, int value)
	{
		StatChanged(type, value);
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
		if (!IsEnoughToLevelUp(value))
		{
			Player.Experience += value;
			ExperienceChanged?.Invoke(Player.Experience);
		}
		else
		{
			var remainder = value - (XPToLevel - Player.Experience);
			Player.Level++;
			Player.Experience = 0 + remainder;
			XPToLevel = SampleLevelXPNeeded();
			
			LeveledUp?.Invoke(XPToLevel, Player.Level);
			ExperienceChanged?.Invoke(Player.Experience);
			GD.Print($"Player leveled up to {Player.Level}!");
		}

		GD.Print($"({Player.Level})Player XP: +{value} :: {Player.Experience}/{XPToLevel}");
	}

	private bool IsEnoughToLevelUp(int value)
	{
		return value + Player.Experience >= XPToLevel;
	}
	
	//Adjust player's health stat and emit an event for HUD to update
	private void AdjustHealth(int value)
	{
		Player.Health += value;
		HealthChanged?.Invoke(Player.Health);
	}

	// ReSharper disable once InconsistentNaming
	private int SampleLevelXPNeeded()
	{
		return (int) LevelCurve.SampleBaked(((float)(Player.Level-1)/Player.MaxLevel))*10; //multiply by a factor of 10 since editor max y value only goes to 1024 (max is set at a 1000 so level 30 should have an XP cap of 10000)
	}
}
