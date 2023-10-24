using Godot;

namespace Mozzie.Code.Enemy;

public abstract class EnemyBase : IEnemy
{
    public virtual string Name => "Not Implemented";
    public virtual string Description => "Not Implemented";
    public bool IsDead { get; set; } = false;
    public virtual int BaseHealth => 0;
    public int Health { get; set; }
    public virtual float BaseSpeed => 0f;
    public virtual float Speed { get; set; }
    
    public Node2D PlayerNode { get; set; }
    public Area2D Player { get; set; }
    public Vector2 Position { get; set; }

    public EnemyBase()
    {
        Health = BaseHealth;
        Speed = BaseSpeed;
    }
    
    public void Navigate(float delta)
    {
        var moveTowardsVector = (Player.Position - Position).Normalized() * Speed * delta;
        Position += new Vector2(moveTowardsVector.X, moveTowardsVector.Y);
    }
    
    public void CheckHealth()
    {
        IsDead = Health <= 0;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        CheckHealth();
    }
}

public enum EnemyNames
{
    ZombieKid
}