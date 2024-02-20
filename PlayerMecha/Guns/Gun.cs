using Commons.FiniteStateMachine;
using Godot;
using System;
using Godot.Collections;
using PlayerMecha.Guns.Bullets;


namespace PlayerMecha.Guns
{
    public partial class Gun : Node2D
    {
        [Export] private float fireDelay;
        [Export] private Array<PackedScene> bulletsTypes = new Array<PackedScene>();
        [Export] private int gunTypeID = 0;
        [Export] private int layer, mask;

        private int bulletsCounts = 1;

        private Timer timer;

        private Player player;
        /// <summary>
        /// 
        /// </summary>
        public void Shoot()
        {
            if(bulletsTypes[gunTypeID] == null) return;
            timer.Start(fireDelay);
            for(int i = 0; i < bulletsCounts; i++)
            {
                Bullet newBullet = bulletsTypes[gunTypeID].Instantiate() as Bullet;
                newBullet.SetPropreties(player.isUP, player.isDown,player.isCrouch,player.isGrounded, player.faceDir);
                newBullet.SetIntOfLayerMask(layer, mask);
                GetParent().GetParent().AddChild(newBullet);
                newBullet.GlobalPosition = new Vector2(GlobalPosition.X, GlobalPosition.Y);
            }
        }

        public override void _Ready()
        {
            timer = GetNode<Timer>("Timer");
            player = GetNode<Player>("..");
        }
        public override void _PhysicsProcess(double delta)
        {
            if (Input.IsActionPressed("Shoot") && timer.IsStopped())
            {
                GD.Print("Shot");
                Shoot();
            }
        }
    }
}
