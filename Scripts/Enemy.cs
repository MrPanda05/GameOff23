using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    private Vector2 vel;
    private Sprite2D Sprite;

    private Timer timer;

    PackedScene bullet = GD.Load<PackedScene>("res://Scenes/bulletEnemy.tscn");

    private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    [Export]public bool isRight = true;

    [Export] float fireDelay, speed = 200;

    public void SetSpriteSide()
	{
	if(vel.X < 0)
		{
			Sprite.FlipH = true;
		}
		else if(vel.X > 0)
		{
			Sprite.FlipH = false;
		}
	}

    public override void _Ready()
    {
        Sprite = GetNode<Sprite2D>("Sprite");
        timer = GetNode<Timer>("Timer");
    }

    public void Shoot()
    {
            
            timer.Start(fireDelay);
            var newBullet = bullet.Instantiate() as CharacterBody2D;
            GetParent().GetParent().AddChild(newBullet);
            newBullet.GlobalPosition = new Vector2(GlobalPosition.X, GlobalPosition.Y - 30);
            if(isRight)
            {
                newBullet.GetNode<bulletEnemy>(".").isRight = true;
            }
            else
            {
                newBullet.GetNode<bulletEnemy>(".").isRight = false;
            }
    }

    public void NormalEnemy()
    {
        if(!IsOnFloor())
		{
			vel.Y += gravity;
		}

        if(isRight)
        {
            vel.X = speed;
        }
        else
        {
            vel.X = -speed;
        }
        MoveAndSlide();

    }

    public void Turred()
    {

    }
    public override void _PhysicsProcess(double delta)
    {
        vel = Velocity;
        if(speed != 0)
        {
            NormalEnemy();
            
        }

        if(timer.IsStopped())
        {
            Shoot();
            //! PlaySound but only if is very close to player
        }
        SetSpriteSide();
        Velocity = vel;
    }
}
