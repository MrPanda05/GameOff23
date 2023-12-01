using Godot;
using System;

public partial class KillPlayer : Area2D
{
    public void OnBodyEnter(CharacterBody2D body)
    {
        GD.Print("Player die due to colision with Bullet");
        //!Play sound o player dying
        GetParent().QueueFree();
    }
}
