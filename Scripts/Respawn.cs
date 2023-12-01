using Godot;
using System;

public partial class Respawn : Node2D
{
    public bool isDead = false;
    public override void _Process(double delta)
    {
        if(isDead)
        {
            GD.Print("Respawn");
            GetTree().ReloadCurrentScene();
            isDead = false;
        }
    }
}
