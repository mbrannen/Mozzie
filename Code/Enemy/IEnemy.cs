using Godot;
using System;

public interface IEnemy
{
    public int Health { get; set; }
    public float Speed { get; set; }
    void Navigate(float delta);
    
}
