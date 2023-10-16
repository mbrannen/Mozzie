using Godot;
using System;

public partial class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export]
	public int Speed {get; set;} = 200;
	
	public Vector2 ScreenSize;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		//Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;
		
		if (Input.IsActionPressed("right"))
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("left"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("down"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("up"))
		{
			velocity.Y -= 1;
		}
		
		var playerSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D_player");
		var dustEmitter = GetNode<GpuParticles2D>("GPUParticles2D");
	
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			playerSprite.Play();
			dustEmitter.Emitting = true;
		}
		else
		{
			dustEmitter.Emitting = false;
			playerSprite.Stop();
		}
		
		Position += velocity * (float)delta;
		// Position = new Vector2(
		// 	x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
		// 	y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
		
		if (velocity.X != 0)
		{
			playerSprite.Animation = "walk_lateral";
			playerSprite.FlipH = false;
			// See the note below about boolean assignment.
			playerSprite.FlipH = velocity.X < 0;
			
		}
		else if (velocity.Y < 0)
			playerSprite.Animation = "walk_up";
		else if (velocity.Y > 0)
			playerSprite.Animation = "walk_down";
	}
	
	private void OnBodyEntered(Node2D body)
	{
		Hide(); // Player disappears after being hit.
		EmitSignal(SignalName.Hit);
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
