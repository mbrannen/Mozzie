using System.Diagnostics;
using Godot;
using Mozzie.Code.Attack;

namespace Mozzie;

public partial class Player : Node2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export]
	public int Speed {get; set;} = 200;
	[ExportGroup("Visuals")]
	[Export] public AnimatedSprite2D PlayerSprite;
	[Export] public GpuParticles2D PlayerDustParticles;


	[ExportGroup("Attacks")] 
	[Export] public Node2D AttackNode; //Global Node to attach attacks to
	[Export] public Marker2D AttackRootMarker;
	[Export] public PackedScene[] AttackScenes;
	public Attack[] Attacks;
	
	
	public PlayerDirection Direction;
	public Vector2 ScreenSize;
	public Vector2 Velocity;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		
		//load the attack scenes
		if (AttackScenes is { Length: > 0 }) 
			Attacks = new Attack[AttackScenes.Length];
		else
			GD.PushWarning("No Attacks loaded!");
		
		//convert scenes to Attack and add as a child node
		for (int i = 0; i < AttackScenes.Length; i++)
		{
			Attacks[i]  = AttackScenes[i].Instantiate() as Attack;
			Attacks[i].Player = this;
			GD.Print($"Loaded {Attacks[i].AttackNameDropdown} attack.");
			AttackNode.AddChild(Attacks[i]);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Velocity = Vector2.Zero;

		if (Input.IsActionPressed("down"))
		{
			Velocity.Y += 1;
			Direction = PlayerDirection.Down;
		}

		if (Input.IsActionPressed("up"))
		{
			Velocity.Y -= 1;
			Direction = PlayerDirection.Up;
		}
		if (Input.IsActionPressed("right"))
		{
			Velocity.X += 1;
			
		}

		if (Input.IsActionPressed("left"))
		{
			Velocity.X -= 1;
			Direction = PlayerDirection.Left;
		}
		
		//var PlayerSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D_player");
		//var PlayerDustParticles = GetNode<GpuParticles2D>("GPUParticles2D");
	
		if (Velocity.Length() > 0)
		{
			Velocity = Velocity.Normalized() * Speed;
			PlayerSprite.Play();
			PlayerDustParticles.Emitting = true;
		}
		else
		{
			PlayerDustParticles.Emitting = false;
			PlayerSprite.Stop();
		}
		
		Position += Velocity * (float)delta;
		// Position = new Vector2(
		// 	x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
		// 	y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
		
		if (Velocity.X != 0)
		{
			if(Velocity.X < 0)
				Direction = PlayerDirection.Left;
			else
				Direction = PlayerDirection.Right;
			
			PlayerSprite.Animation = "walk_lateral";
			PlayerSprite.FlipH = false;
			// See the note below about boolean assignment.
			PlayerSprite.FlipH = Velocity.X < 0;
			
		}
		else if (Velocity.Y < 0)
			PlayerSprite.Animation = "walk_up";
		else if (Velocity.Y > 0)
			PlayerSprite.Animation = "walk_down";
	}
	
	private void OnBodyEntered(Node2D body)
	{
		Hide(); // Player disappears after being hit.
		//EmitSignal(global::Player.SignalName.Hit);
		// Must be deferred as we can't change physics properties on a physics callback.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}

	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
}

public enum PlayerDirection
{
	Up,
	Down,
	Left,
	Right
}
