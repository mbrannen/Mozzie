using Godot;
using System;
using System.Collections.Generic;
using Mozzie.Code.Enemy;
using Mozzie.Code.Pickups;
using Mozzie.Code.Player;

public partial class PickupManager : Node
{
    [Export] public PackedScene[] PickupScenes;
    [Export] public EnemyManager EnemyManager;

    private const int INDEX_EXPERIENCE = 0;
    
    //TODO: Maybe some sort of loot table?   Don't plan on adding much loot but maybe certain creatures drop certain things
    
    public delegate void NotifyPickedUpDelegate(StatType type,int value);
    public event NotifyPickedUpDelegate NotifyPickedUp;

    public override void _EnterTree()
    {
        EnemyManager.NotifyDead += EnemyManagerOnNotifyDead;
    }
    /// <summary>
    /// A function to handle event notifications from the EnemyManager when any enemy is killed
    /// </summary>
    /// <param name="type">The type of enemy that was killed</param>
    /// <param name="position">Global position of where the enemy died</param>
    private void EnemyManagerOnNotifyDead(EnemyType type, Vector2 position)
    {
        var experience = PickupScenes[INDEX_EXPERIENCE].Instantiate<Pickup>();
        experience.Position = position;
        experience.PickedUp += OnPickedUp;
        AddChild(experience);
    }

    private void OnPickedUp(StatType stattype, int value)
    {
        GD.Print($"{stattype} picked up for {value}!");
        NotifyPickedUp?.Invoke(stattype, value);
    }

    public override void _Ready()
    {
        
    }
}
