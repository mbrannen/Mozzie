using System.Collections.Generic;
using System.Linq;
using Godot;
using Mozzie.Code.Utility;

namespace Mozzie.Code.Enemy;

public partial class EnemyManager : Node2D
{
	[Export] public Node2D PlayerNode;
	[Export] public PackedScene[] EnemyScenes;
	[Export] public Node2D EnemyNode;
	public List<Enemy> Enemies = new();
	
	[Export] public Node2D NodeDamageTexts { get; set; }
	[Export] public Label LabelNumberOfEnemies { get; set; }
	
	[Export] public Timer ZombieKidSpawnTimer;
	[Export] public Timer ZombieKidMultiSpawnTimer;
	
	private Mozzie.Player _player;

	private const int INDEX_ZOMBIEKID = 0;
	private const int WAVEQUANTITY_ZOMBIEKID = 20;
	
	public delegate void NotifyDeadDelegate(EnemyType type, Vector2 position);
	public event NotifyDeadDelegate NotifyDead;
	
	// Called when the node enters the scene tree for the first time.
	public override void _EnterTree()
	{
		ZombieKidSpawnTimer.Timeout += ZombieKidSpawnTimerOnTimeout;
		ZombieKidMultiSpawnTimer.Timeout += ZombieKidMultiSpawnTimerOnTimeout;
	}
	
	private void ZombieKidMultiSpawnTimerOnTimeout()
	{
		var spawnLocations = Spawn.GetPointsInCircleAroundPosition(PlayerNode.Position, 600f, WAVEQUANTITY_ZOMBIEKID);
		foreach (var location in spawnLocations)
		{
			var zombieKid = EnemyScenes[INDEX_ZOMBIEKID].Instantiate<Enemy>();
			zombieKid.NodeDamageTexts = NodeDamageTexts;
			zombieKid.Position = location;
			zombieKid.EnemyDied += OnEnemyDied;
			AddChild(zombieKid);
			zombieKid.EnemyBase.Player = _player;
		}
	}

	private void ZombieKidSpawnTimerOnTimeout()
	{
		var zombieKid = EnemyScenes[INDEX_ZOMBIEKID].Instantiate<Enemy>();
		var spawnLocation = Spawn.GetRandomPointInCircleAroundPosition(PlayerNode.Position, 450f);
		zombieKid.NodeDamageTexts = NodeDamageTexts; //TODO Move this to an event based system
		zombieKid.Position = spawnLocation;
		zombieKid.EnemyDied += OnEnemyDied;
		AddChild(zombieKid);
		zombieKid.EnemyBase.Player = _player;
	}

	private void OnEnemyDied(EnemyType type, Vector2 position)
	{
		//TODO: Update score of some kind of tracker here as well
		NotifyDead?.Invoke(type, position);
	}

	public override void _Ready()
	{
		_player = PlayerNode as Mozzie.Player;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}