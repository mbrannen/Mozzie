using Godot;
using System;

public partial class ZombieKid : Area2D, IEnemy
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = PlayerNode.GetNode<Area2D>("Area2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		Navigate((float)delta);
	}

	[Export] public int Health { get; set; }
	[Export] public float Speed { get; set; } = 100f;

	[Export]
	public Node2D PlayerNode { get; set; }
	private Area2D Player { get; set; }

	public void Navigate(float delta)
	{
		var moveTowardsVector = (Player.Position - Position).Normalized() * Speed * delta;
		Position += new Vector2(moveTowardsVector.X, moveTowardsVector.Y);
	}

}
