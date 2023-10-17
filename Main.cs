using Godot;
using System;
using System.Web;

public partial class Main : Node2D
{
	[Export]
	public Node2D PlayerNode { get; set; }
	
	[Export] public PackedScene ZombieKid { get; set; }
	[Export] public Node EnemyNode { get; set; }
	[Export] public Label LabelNumberOfEnemies { get; set; }

	public int NumberOfEnemies { get; set; } = 0;

	private Area2D Player { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = PlayerNode.GetNode<Area2D>("Area2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	//Timers - set to spawn a zombie kid every 1s
	private void OnSpawnTimerTimeout()
	{
		#region SingleSpaw
			//initial test of spawning
			ZombieKid zombieKid = ZombieKid.Instantiate<ZombieKid>();
			zombieKid.PlayerNode = PlayerNode;

			var spawnLocation = Spawn.GetRandomPointInCircleAroundPosition(Player.Position, 300f);
			zombieKid.Position = spawnLocation;
			EnemyNode.AddChild(zombieKid);
			UpdateLabelForNumberOfEnemies(1);
		
		#endregion

		#region MultiSpawn
			//inital multispawn testing
			var spawnLocations = Spawn.GetPointsInCircleAroundPosition(Player.Position, 300f, 30);
			foreach (var location in spawnLocations)
			{
				ZombieKid zombieKidMulti = ZombieKid.Instantiate<ZombieKid>();
				zombieKidMulti.PlayerNode = PlayerNode;
				zombieKidMulti.Position = location;
				EnemyNode.AddChild(zombieKidMulti);
				UpdateLabelForNumberOfEnemies(1);
			}

		#endregion

	}

	private void UpdateLabelForNumberOfEnemies(int numberToIncrement)
	{
		NumberOfEnemies += numberToIncrement;
		LabelNumberOfEnemies.Text = $"Number of Enemies: {NumberOfEnemies}";
	}
}
