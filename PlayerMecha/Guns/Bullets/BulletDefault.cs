using Commons.Components;
using Godot;
using System;

namespace PlayerMecha.Guns.Bullets
{
    public partial class BulletDefault : Bullet
    {


        public override void Movement(float delta)
        {
            if(!localfaceDir)
            {
                vel = Vector2.Right * speed;
                RotationDegrees = 0;
                return;
            }else
            {
                vel = Vector2.Left * speed;
                RotationDegrees = 0;
                return;
            }
        }


        public override void _Ready()
        {
            deathTimer = Time.GetTicksMsec() + deathTimerDelay;
            hitbox = GetNode<HitboxComponent>("HitboxComponent");
            //GD.Print(layer);
            //GD.Print(mask);
            SetLayerMask();
            //GD.Print(hitbox.GetCollisionLayerValue(3));
            //GD.Print(hitbox.GetCollisionMaskValue(2));
        }
        public override void _Process(double delta)
        {
            if(Time.GetTicksMsec() >= deathTimer)
            {
                QueueFree();
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            Movement((float) delta);
            Velocity = vel;
            MoveAndSlide();
        }
    }
}
