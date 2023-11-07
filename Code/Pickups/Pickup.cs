using Godot;
using System;
using Mozzie;
using Mozzie.Code.Pickups;
using Mozzie.Code.Player;

public partial class Pickup : Node2D
{
	[Export] public StatType StatType;
	[Export] public Area2D CollisionArea;
	public PickupBase _pickup;
	
	
	public delegate void PickedUpDelegate(StatType statType,int value);

	public event PickedUpDelegate PickedUp;

	public override void _EnterTree()
	{
		_pickup = GetPickup(StatType);
		if (!_pickup.IsSpawned())
		{
			QueueFree();
			return;
		}
		CollisionArea.AreaEntered += OnPlayerEntered;
	}

	public override void _Ready()
	{
		_pickup = GetPickup(StatType);
	}
	
	
	private PickupBase GetPickup(StatType statType)
	{
		switch (statType)
		{
			case StatType.Null:
				GD.PrintErr("No Stat selected!");
				return null;
			case StatType.Experience:
				return new Experience();
			case StatType.Health:
				GD.PrintErr("Health not implemented!");
				return null;
			default:
				GD.PrintErr("How did you even get here?");
				return null;
				
		}
	}

	private void OnPlayerEntered(Area2D body)
	{
			OnPickedUp(_pickup.Value);
			QueueFree();
	}
	private void OnPickedUp(int value)
	{
		PickedUp?.Invoke(StatType, value);
	}
}
