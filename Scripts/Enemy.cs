using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    private Vector2 vel;

    private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public bool isRight = true;

    public override void _PhysicsProcess(double delta)
    {
        vel = Velocity;
        if(!IsOnFloor())
		{
			vel.Y += gravity;
		}

        if(isRight)
        {
            vel.X = 400f;
        }
        else
        {
            vel.X = -400f;
        }
        MoveAndSlide();
        Velocity = vel;
    }
}
