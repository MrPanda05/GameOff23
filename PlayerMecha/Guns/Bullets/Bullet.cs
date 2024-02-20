using Commons.Components;
using Godot;
using System;
using System.Reflection.Emit;
using System.Threading.Tasks;


namespace PlayerMecha.Guns.Bullets
{
    public partial class Bullet : CharacterBody2D
    {
        [Export] protected float speed;
        [Export] protected ulong deathTimerDelay;

        protected bool localUp, localDown, isCrouch, isGrounded, localfaceDir;
        protected Vector2 vel;
        protected ulong deathTimer;

        protected HitboxComponent hitbox;

        protected int layer, mask;
        

        //Set propreties that the bullets needs from the player
        public void SetPropreties(bool localUp, bool localDown, bool isCrouch, bool isGrounded, bool localfaceDir)
        {
            this.localDown = localDown;
            this.localUp = localUp;
            this.isCrouch = isCrouch;
            this.isGrounded = isGrounded;
            this.localfaceDir = localfaceDir;
        }
        public void SetIntOfLayerMask(int layer, int mask)
        {
            this.layer = layer;
            this.mask = mask;
        }
        protected void SetLayerMask()
        {
            //GD.Print(layer);
            //GD.Print(mask);
            hitbox.SetCollisionLayerValue(layer, true);
            hitbox.SetCollisionMaskValue(mask, true);
        }

        public virtual void Movement(float delta) { }

    }
}

