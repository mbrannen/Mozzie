using Godot;
using System;
using Mozzie.Code.Enemy;

public interface IEnemy
{
    public string Name { get; }
    public string Description { get; }
    public EnemyType Type { get; }
    public EnemyState State { get; }
    public bool IsDead { get; set; }
    public int BaseHealth { get; }
    public int Health { get; set; }
    public int BaseDamage { get; }
    public int Damage { get; set; }
    public float BaseSpeed { get; }
    public float Speed { get; set; }
    public Node2D Player { get; set; }


    protected Vector2 Position { get; set; }
    void Navigate(float delta);
    
}
