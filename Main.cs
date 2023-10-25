using Godot;
using Mozzie.Code.Enemy;
using Mozzie.Code.Utility;

namespace Mozzie;

public partial class Main : Node2D
{
	[Export]
	public Node2D PlayerNode { get; set; }
	
	//TODO: Add logic for a collection of enemies to choose from and instantiate
	[Export] public PackedScene ZombieKid { get; set; }
	[Export] public Node EnemyNode { get; set; }
	[Export] public Label LabelNumberOfEnemies { get; set; }

	public int NumberOfEnemies { get; set; }

	private Area2D _playerBody;

	private Player _player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = PlayerNode as Player;
		_playerBody = PlayerNode.GetNode<Area2D>("Area2D");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	//Timers - set to spawn a zombie kid every 1s
	//TODO: Move this to some sort of enemy spawning manager
	private void OnSpawnTimerTimeout()
	{
		#region SingleSpaw
		//initial test of spawning
		var zombieKid = ZombieKid.Instantiate<Enemy>();
		var spawnLocation = Spawn.GetRandomPointInCircleAroundPosition(PlayerNode.Position, 300f);
		zombieKid.Position = spawnLocation;
		zombieKid.EnemyBase.Player = _player;
		EnemyNode.AddChild(zombieKid);
		UpdateLabelForNumberOfEnemies(1);
		
		#endregion

		#region MultiSpawn
		// initial multispawn testing
		var spawnLocations = Spawn.GetPointsInCircleAroundPosition(PlayerNode.Position, 500f, 100);
		foreach (var location in spawnLocations)
		{
			var zombieKidMulti = ZombieKid.Instantiate<Enemy>();
			zombieKidMulti.EnemyBase.Player = _player;
			zombieKidMulti.Position = location;
			EnemyNode.AddChild(zombieKidMulti);
			UpdateLabelForNumberOfEnemies(1);
		}

		#endregion

	}
	//TODO: Move this to some sort of UI Manager
	private void UpdateLabelForNumberOfEnemies(int numberToIncrement)
	{
		NumberOfEnemies += numberToIncrement;
		LabelNumberOfEnemies.Text = $"Number of Enemies: {NumberOfEnemies}";
	}
}
