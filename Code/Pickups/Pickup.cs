using Godot;
using System;
using Mozzie.Code.Pickups;
using Mozzie.Code.Player;

public partial class Pickup : Node
{
	[Export] public Area2D CollisionArea;
	private PickupBase _pickup;
	private StatType _statType;
	
	public delegate void PickedUpDelegate(StatType statType,int value);

	public event PickedUpDelegate PickedUp;

	public override void _Ready()
	{
		_pickup = GetPickup(_statType);
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
		if (body.GetType() == typeof(Mozzie.Player))
		{
			OnPickedUp(_pickup.Value);
			QueueFree();
		}
	}
	private void OnPickedUp(int value)
	{
		PickedUp?.Invoke(_statType, value);
	}
}
