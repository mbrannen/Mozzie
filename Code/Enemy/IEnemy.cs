using Godot;
using System;

public interface IEnemy
{
    public string Name { get; }
    public string Description { get; }
    public bool IsDead { get; set; }
    public int BaseHealth { get; }
    public int Health { get; set; }
    public float BaseSpeed { get; }
    public float Speed { get; set; }
    public Node2D PlayerNode { get; set; }
    public Area2D Player { get; set; }

    protected Vector2 Position { get; set; }
    void Navigate(float delta);
    
}
