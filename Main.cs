using Godot;
using Mozzie.Code.Enemy;
using Mozzie.Code.Utility;

namespace Mozzie;

public partial class Main : Node2D
{
	[Export]
	public Node2D PlayerNode { get; set; }
	[Export] public PackedScene ZombieKid { get; set; }
	[Export] public Node EnemyNode { get; set; }
	[Export] public Node2D NodeDamageTexts { get; set; }
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
	
	//TODO: Move this to some sort of UI Manager
	private void UpdateLabelForNumberOfEnemies(int numberToIncrement)
	{
		NumberOfEnemies += numberToIncrement;
		LabelNumberOfEnemies.Text = $"Number of Enemies: {NumberOfEnemies}";
	}
}
