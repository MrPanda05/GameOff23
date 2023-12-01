using Godot;
using System;

public partial class bulletEnemy : CharacterBody2D
{

    public bool isRight = false;

    private Vector2 vel;
    public override void _Process(double delta)
    {
        if(Time.GetTicksMsec() >= Time.GetTicksMsec() + 8000)
        {
            QueueFree();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        vel = Velocity;
        if(isRight)
        {
            vel = Vector2.Right * 500 * (float)delta;
        }
        else
        {
            vel = Vector2.Left * 500 * (float)delta;
        }
        var coli = MoveAndCollide(vel);
        if(coli != null)
        {
            QueueFree();

        }
        Velocity = vel;
    }
}
