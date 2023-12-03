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

    private AudioStreamPlayer audioPlayer;

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
		audioPlayer = GetNode<AudioStreamPlayer>("JumpFx");
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
			Sprite.FlipH = true;
			Looks = 0;
		}
		else if(vel.X > 0)
		{
			Sprite.FlipH = false;
			Looks = 1;
		}
	}

	public void SetGunRotationAndFlips(bool flipH, bool flipV, float degrees, Vector2 Offset)
	{
		SpriteGun.RotationDegrees = degrees;
		SpriteGun.FlipH = flipH;
		SpriteGun.FlipV = flipV;
		//SpriteGun.Offset = Offset;
		gunPos = Offset;
		
	}

	public void RotationLogicGround()
	{
		// Olhando pra cima parado e esta nao esta agaixado
		if(isUP && !isCrouch && Velocity == Vector2.Zero)
		{
			if(Looks == 1)
			{
				SetGunRotationAndFlips(false, false, -90, Vector2.Zero);
			}
			else
			{
				//SetGunRotationAndFlips(false, true, -90, Vector2.Zero);
				SetGunRotationAndFlips(false, true, -90, new Vector2(-15,0));
			}
		}
		//Olhando pra cima parado agaixado
		else if(isUP && isCrouch)
		{
			if(Looks == 1)
			{
				SetGunRotationAndFlips(false, false, -90, new Vector2(0, 40));
			}
			else
			{
				SetGunRotationAndFlips(false, true, -90, new Vector2(-10, 40));
			}
		}
		//Olhando pra cima enquanto anda
		else if(isUP && Velocity != Vector2.Zero && !isDown)
		{
			if(Looks == 1)
			{
				SetGunRotationAndFlips(false, false, -45, Vector2.Zero);
			}
			else
			{
				SetGunRotationAndFlips(true, false, 45, new Vector2(-15,0));
			}
		}
		//Agaixado
		else if(!isUP && isCrouch)
		{
			if(Looks == 1)
			{
				SetGunRotationAndFlips(false, false, 0, new Vector2(0, 40));
			}
			else
			{
				SetGunRotationAndFlips(true, false, 0, new Vector2(0, 40));
			}
		}
		//Olhanod pra baixo e andando
		else if(!isUP && isDown && Velocity != Vector2.Zero)
		{
			if(Looks == 1)
			{
				SetGunRotationAndFlips(true, true, -135, Vector2.Zero);
			}
			else
			{
				SetGunRotationAndFlips(false, true, 135, new Vector2(-10,0));
			}
		}
		//Parado our andando sem direcao
		else if(!isUP && !isDown)
		{
			if(Looks == 1)
			{
				SetGunRotationAndFlips(false, false, 0, Vector2.Zero);
			}
			else
			{
				//SetGunRotationAndFlips(true, false, 0, Vector2.Zero);
				SetGunRotationAndFlips(true, false, 0, new Vector2(-10,0));

			}
		}
	}

	public void RotationLogicAir()
	{
		//Jumping without moving looking up
			if(isUP && !isDown && Velocity.X == 0)
			{
				if(Looks == 1)
				{
					SetGunRotationAndFlips(false, false, -90, Vector2.Zero);
				}
				else
				{
					SetGunRotationAndFlips(false, true, -90, Vector2.Zero);
				}
			}
			//Jumping without moving looking Down
			else if(!isUP && isDown && Velocity.X == 0)
			{
				if(Looks == 1)
				{
					SetGunRotationAndFlips(false, false, 90, Vector2.Zero);
				}
				else
				{
					SetGunRotationAndFlips(false, true, 90, Vector2.Zero);
				}
			}
			else if(!isUP && isDown && Velocity != Vector2.Zero)
			{
				if(Looks == 1)
				{
					SetGunRotationAndFlips(true, true, -135, Vector2.Zero);
				}
				else
				{
					SetGunRotationAndFlips(false, true, 135, Vector2.Zero);
				}
			}
			else if(isUP && Velocity != Vector2.Zero && !isDown)
			{
				if(Looks == 1)
				{
					SetGunRotationAndFlips(false, false, -45, Vector2.Zero);
				}
				else
				{
					SetGunRotationAndFlips(true, false, 45, Vector2.Zero);
				}
			}
			else if(!isUP && !isDown)
			{
				if(Looks == 1)
				{
					SetGunRotationAndFlips(false, false, 0, Vector2.Zero);
				}
				else
				{
					SetGunRotationAndFlips(true, false, 0, Vector2.Zero);
				}
			}
	}
	public void SetRotation()
	{
		if(IsOnFloor())
		{
			RotationLogicGround();
		}
		else
		{
			RotationLogicAir();
		}
	}
	public void Jump(float delta)
	{
		audioPlayer.Play();
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
		gun.Position = gunPos;
		MoveAndSlide();
	}
}
