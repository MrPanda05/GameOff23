using Godot;
using System;


namespace PlayerMecha
{
    public partial class Player : CharacterBody2D
    {
        [ExportGroup("Player propreties")]
        [Export] private float speed = 300f;
        [Export] private float jumpForce = 1500f;
        [Export] private float friction = 0.1f;
        [Export] private float acceleration = 0.25f;

        private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
        private float dir;


        private Vector2 vel;

        public bool isCrouch { get; private set; } 
        public bool isUP { get; private set; }
        public bool isDown { get; private set; }
        public bool oneWay { get; private set; }
        public bool faceDir { get; private set; }
        public bool isGrounded { get; private set; }



        #region Movement methods
        public void Jump()
        {
            vel.Y = -jumpForce;
        }
        public void Movement()
        {
            if(dir != 0)
            {
                vel.X = Mathf.Lerp(vel.X, dir * speed, acceleration);
            }
            else
            {
                vel.X = Mathf.Floor(Mathf.Lerp(vel.X, 0, friction));
            }
            

        }
        public void MovementLogic(float delta)
        {
            if(Input.IsActionJustPressed("Jump") && IsOnFloor() && !isCrouch)
            {
                Jump();
            }
            SetBools();
            Movement();
            MoveAndSlide();

        }
        public void SetBools()
        {
            isDown = Input.IsActionPressed("Crouch");
            isUP = Input.IsActionPressed("Up");
            oneWay = Input.IsActionPressed("OneWayDown");
            isCrouch = vel.X == 0 && isDown && IsOnFloor();
            faceDir = vel.X < 0;
            isGrounded = IsOnFloor();
        }



        #endregion Movement methods

        #region Helper
        public void PlayerPrinter()
        {
            GD.Print($"Player variables \n isUp:{isUP} \n isDown:{isDown} \n oneWay:{oneWay} \n isCrouch:{isCrouch} \n faceDir:{(faceDir ? "Left" :"Right")} \n isGrounded:{isGrounded}");
        }
        #endregion Helper


        public override void _Ready()
        {
            gravity /= 11;
        }

        public override void _PhysicsProcess(double delta)
        {
            vel = Velocity;
            if(!IsOnFloor())
            {
                vel.Y += gravity;
            }
            dir = Input.GetAxis("Left", "Right");
            MovementLogic((float)delta);
            Velocity = vel;
            if(Input.IsActionJustPressed("Debug")) PlayerPrinter();
        }
    }
}
