using System;
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
	public StatSheet StatSheet;
	
	//singleton declaration
	private static StatManager _instance;
	public static StatManager Instance => _instance;

	private int XPToLevel { get; set; }
	
	public delegate void LeveledUpDelegate(int xpToLevel, byte level);
	public event LeveledUpDelegate LeveledUp;
	public delegate void ExperienceChangedDelegate(int value);
	public event ExperienceChangedDelegate ExperienceChanged;
	
	public delegate void HealthChangedDelegate(int value);
	public event HealthChangedDelegate HealthChanged;
	

	public override void _EnterTree()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		StatSheet = new StatSheet()
		{
			Level = new Stat<byte>(StatType.Level, 1),
			Experience = new Stat<int>(StatType.Experience, 0),
			Health = new Stat<int>(StatType.Health, 100),
			Speed = new Stat<int>(StatType.Speed, 200)
		};
		PickupManager.NotifyPickedUp += PickupManagerOnNotifyPickedUp;
		EnemyManager.NotifyPlayerDamaged += EnemyManagerOnNotifyPlayerDamaged;
	}

	public override void _Ready()
	{
		//emit signal to init the values
		ExperienceChanged?.Invoke(StatSheet.Experience.Value);
		HealthChanged?.Invoke(StatSheet.Health.Value);
		
		//DetermineInitial experience needed to level
		LevelCurve.BakeResolution = Player.MaxLevel; //editor default to 30 adjust on player class if needed
		LevelCurve.Bake();
		XPToLevel = SampleLevelXPNeeded();
		LeveledUp?.Invoke(XPToLevel, StatSheet.Level.Value);
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
			StatSheet.Experience.Value += value;
			ExperienceChanged?.Invoke(StatSheet.Experience.Value);
		}
		else
		{
			var remainder = value - (XPToLevel - StatSheet.Experience.Value);
			StatSheet.Level.Value++;
			StatSheet.Experience.Value = 0 + remainder;
			XPToLevel = SampleLevelXPNeeded();
			
			LeveledUp?.Invoke(XPToLevel, StatSheet.Level.Value);
			ExperienceChanged?.Invoke(StatSheet.Experience.Value);
			GD.Print($"Player leveled up to {StatSheet.Level.Value}!");
		}

		GD.Print($"({StatSheet.Level.Value})Player XP: +{value} :: {StatSheet.Experience.Value}/{XPToLevel}");
	}

	private bool IsEnoughToLevelUp(int value)
	{
		return value + StatSheet.Experience.Value >= XPToLevel;
	}
	
	//Adjust player's health stat and emit an event for HUD to update
	private void AdjustHealth(int value)
	{
		StatSheet.Health.Value += value;
		HealthChanged?.Invoke(StatSheet.Health.Value);
	}

	// ReSharper disable once InconsistentNaming
	private int SampleLevelXPNeeded()
	{
		return (int) LevelCurve.SampleBaked(((float)(StatSheet.Level.Value-1)/Player.MaxLevel))*10; //multiply by a factor of 10 since editor max y value only goes to 1024 (max is set at a 1000 so level 30 should have an XP cap of 10000)
	}

	public T GetStat<T>(StatType stat)
	{
		switch (stat)
		{
			case StatType.Speed:
				return (T)(object)StatSheet.Speed.Value;
			default:
				return default;
		}
	}
}
