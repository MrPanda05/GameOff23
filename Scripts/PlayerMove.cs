using Godot;
using System;

public partial class PlayerMove : CharacterBody2D
{
	//Exported Var
    [Export] public float player1Speed = 350.0f;
	[Export] public float player1Jump = 1500.0f;
	[Export] public float friction = 0.1f;
	[Export] public float acceleration = 0.25f;

	[Export] public Texture2D down, up;

	public int Looks = 1;

	private float dir;

	//public float inflationRate = 0.0005f;

	private Sprite2D SpriteGun;

	private AnimatedSprite2D Sprite;
	//private CharacterBody2D player;
	private Node2D gun;
	public bool isUP, isDown, isCrouch, oneWay;

	private float tempX;

	private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private Vector2 vel, gunPos;

	[Export] CollisionShape2D Area, HitBox;
    public override void _Ready()
    {
        Sprite = GetNode<AnimatedSprite2D>("SpritePlayer");
		gun = GetNode<Node2D>("Gun");
		SpriteGun = gun.GetNode<Sprite2D>("GunSprite");
    }
  
    public override void _PhysicsProcess(double delta)
    {
		vel = Velocity;
		gunPos = gun.Position;
		isDown = Input.IsActionPressed("DownP1");
		isUP = Input.IsActionPressed("Up");
		oneWay = Input.IsActionJustPressed("OneWayDown");
		isCrouch = vel.X == 0 && isDown && IsOnFloor();
        //GlobalScale += new Vector2(inflationRate, inflationRate);
		SetRotation();
		if(!IsOnFloor())
		{
			vel.Y += (gravity / 11) + (float)delta;
		}

		MovementPlayer((float)delta);
		SetSpriteSide();
    }

	public void SetSpriteSide()
	{
	if(vel.X < 0)
		{
			SpriteGun.FlipH = true;
			Sprite.FlipH = true;
			Looks = 0;
		}
		else if(vel.X > 0)
		{
			SpriteGun.FlipH = false;
			Sprite.FlipH = false;
			Looks = 1;
		}
	}
	public void SetRotation()
	{
		if(isUP)
		{
			gun.RotationDegrees = 90;
			tempX = Velocity.X;
			if(tempX > 0)
			{
				gun.RotationDegrees = -45;
				return;
			}
			else if (tempX < 0)
			{
				gun.RotationDegrees = 45;
				return;
			}
		}
		else if(isDown)
		{
			tempX = Velocity.X;
			
			if(!IsOnFloor() && tempX == 0)
			{
				gun.RotationDegrees = 90;
				return;
			}
			if(tempX > 0)
			{
				gun.RotationDegrees = -135;
				return;
			}
			else if (tempX < 0)
			{
				gun.RotationDegrees = 135;
				return;
			}
			else
			{
				gun.RotationDegrees = 0;
				return;
			}
		}
		else
		{
			gun.RotationDegrees = 0;
			return;
		}
		
	}
	public void Jump(float delta)
	{
		vel.Y = -player1Jump;
	}

	public void SetGunPos(Vector2 Pos)
	{
		gun.Position = Pos;
	}
	
	public void playAnim()
	{
		if(Velocity == Vector2.Zero && !isDown && !isUP && IsOnFloor())
		{
			Sprite.Play("Idle");
			return;
		}
		else if(Velocity == Vector2.Zero && isCrouch && !isUP)
		{
			Sprite.Play("Down");
			return;
		}
		else if(Velocity == Vector2.Zero && !isDown && isUP && IsOnFloor())
		{
			Sprite.Play("Up");
			return;
		}
		else if(Velocity == Vector2.Zero && isCrouch && isUP)
		{
			Sprite.Play("DownUp");
			return;
		}
		else if(Velocity != Vector2.Zero && !isDown && !isUP && IsOnFloor())
		{
			Sprite.Play("Walk");
			return;
		}
		else if(Velocity != Vector2.Zero && !isDown && isUP && IsOnFloor())
		{
			Sprite.Play("WalkUp");
			return;
		}
		else if(Velocity != Vector2.Zero && isDown && !isUP && IsOnFloor())
		{
			Sprite.Play("WalkDown");
			return;
		}
		else if(Velocity != Vector2.Zero && !IsOnFloor())
		{
			Sprite.Play("Jump");

		}
	}
	public void MovementPlayer(float delta)
	{
		//* Change sprite while walking
		if(Input.IsActionJustPressed("Jump") && IsOnFloor() && !isCrouch)
		{
			Jump(delta);
			//!Play jump sound
		}

		dir = Input.GetAxis("LeftP1", "RightP1");
		if(dir != 0)
		{
			vel.X = Mathf.Lerp(vel.X, dir * player1Speed, acceleration);
		}
		else
		{
			vel.X = Mathf.Lerp(vel.X, 0, friction);

		}

		if(vel.X <= 0.00001 && vel.X >= -0.00001)
		{
			vel.X = 0;
		}
		if(isCrouch)
		{
			SetGunPos(new Vector2(0,40));
			Area.Position = new Vector2(-15, 36);
			Area.Scale = new Vector2(1, 0.4f);
			HitBox.Position = new Vector2(-13.5f, 39);
			HitBox.Scale = new Vector2(0.904f, 0.5f);
			vel.X = 0;
		}
		else
		{
			SetGunPos(Vector2.Zero);
			Area.Position = new Vector2(-15, -1);
			Area.Scale = new Vector2(1, 1);
			HitBox.Position = new Vector2(-13.5f, 1);
			HitBox.Scale = new Vector2(0.904f, 1.387f);
		}
		if(isDown && IsOnFloor() && oneWay)
		{
			var pos = Position;
			pos.Y += 1;
			Position = pos;
		}
		playAnim();
		Velocity = vel;
		MoveAndSlide();
	}
}
