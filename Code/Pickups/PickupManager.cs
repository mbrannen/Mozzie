using Godot;
using System;
using System.Collections.Generic;

public partial class PickupManager : Node
{
    [Export] public PackedScene[] PickupScenes;
    public List<Pickup> Pickups = new();

    public override void _Ready()
    {
        for (int i = 0; i < PickupScenes.Length; i++)
        {
            var scene = PickupScenes[i].Instantiate() as Pickup;
            Pickups.Add(scene);
        }
    }
}
