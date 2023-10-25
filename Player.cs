using Godot;

namespace Mozzie;

public partial class Player : Node2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export]
	public int Speed {get; set;} = 200;

	[Export] public AnimatedSprite2D PlayerSprite;
	[Export] public GpuParticles2D PlayerDustParticles;
	[Export] public Marker2D AttackRootMarker;
	
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
		
		//var PlayerSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D_player");
		//var PlayerDustParticles = GetNode<GpuParticles2D>("GPUParticles2D");
	
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			PlayerSprite.Play();
			PlayerDustParticles.Emitting = true;
		}
		else
		{
			PlayerDustParticles.Emitting = false;
			PlayerSprite.Stop();
		}
		
		Position += velocity * (float)delta;
		// Position = new Vector2(
		// 	x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
		// 	y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
		
		if (velocity.X != 0)
		{
			PlayerSprite.Animation = "walk_lateral";
			PlayerSprite.FlipH = false;
			// See the note below about boolean assignment.
			PlayerSprite.FlipH = velocity.X < 0;
			
		}
		else if (velocity.Y < 0)
			PlayerSprite.Animation = "walk_up";
		else if (velocity.Y > 0)
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
