using Godot;
using System;

public partial class bullet : CharacterBody2D
{
    public PlayerMove PlayerDir;
    private Vector2 vel;

    private ulong deathTimer;

    private float randAngle;

    private int localLook, gunType;

    private bool localUp, localDown, isCrouch, isGrounded;

    private float speedX, speedY, ocilation;
    [Export] public float speed, freq, ampli, offset;
    public override void _Ready()
    {  
        //GD.Print(GetParent().GetChild(0).GetNode<PlayerMove>("."));
        PlayerDir = GetParent().GetChild(0).GetNode<PlayerMove>(".");
        localLook = PlayerDir.Looks;
        localUp = PlayerDir.isUP;
        localDown = PlayerDir.isDown;
        isCrouch = PlayerDir.isCrouch;
        gunType = GetParent().GetChild(0).GetNode<PlayerMove>(".").GetNode<Weapon>("Gun").gunType;
        isGrounded = PlayerDir.IsOnFloor();
        speedX = PlayerDir.Velocity.X;
        deathTimer = Time.GetTicksMsec() + 8000;
        GD.Print(speedX);
        ocilation = 0;
        GD.Randomize();
        var random = new RandomNumberGenerator();
        random.Randomize();
        randAngle = random.RandfRange(-1f, 1f);
    }

    public override void _Process(double delta)
    {
        if(Time.GetTicksMsec() >= deathTimer)
        {
            GD.Print("Deleted due to time");
            QueueFree();
        }
        ocilation += (float)delta;
        
    }

    
    //Default shoot type
    public void ShootType0(float delta)
    {
        //* Shoot up
        if(localUp && speedX == 0)
        {
            vel = Vector2.Up * speed * delta;
            vel.X = 0;
            RotationDegrees = 90;
        }

        //* Shoot right
        if(localLook == 1 && !localUp && !localDown)
        {
            vel = Vector2.Right * speed * delta;
            vel.Y = 0;
            RotationDegrees = 0;

        }
        //* Shoot Left
        else if(localLook == 0 && !localUp && !localDown)
        {
            vel = Vector2.Left * speed * delta;
            vel.Y = 0;
            RotationDegrees = 0;
        }
        //*Shoot right down
        if(localLook == 1 && !localUp && isCrouch)
        {
            vel = Vector2.Right * speed * delta;
            vel.Y = 0;
            RotationDegrees = 0;

        }
        //* Shoot Left down
        else if(localLook == 0 && !localUp && isCrouch)
        {
            vel = Vector2.Left * speed * delta;
            vel.Y = 0;
            RotationDegrees = 0;
        }

        //* Shoot Diagonal
        if(localUp && speedX > 0)
        {
            vel = new Vector2(1,-1).Normalized() * speed * delta;
            RotationDegrees = -45;
        }
        else if(localUp && speedX < 0)
        {
            vel = new Vector2(-1,-1).Normalized() * speed * delta;
            RotationDegrees = 45;
        }

        if(localDown && speedX > 0)
        {
            vel = new Vector2(1,1).Normalized() * speed * delta;
            RotationDegrees = -135;
        }
        else if(localDown && speedX < 0)
        {
            vel = new Vector2(-1,1).Normalized() * speed * delta;
            RotationDegrees = 135;
        }
        //* Shoot down while jump
        if(!isGrounded && speedX == 0 && localDown)
        {
            vel = Vector2.Down * speed * delta;
            vel.X = 0;
            RotationDegrees = -90;
        }

    }
    // Type 1 rotate
    public void ShootType1(float delta)
    {
        //* Shoot up
        if(localUp && speedX == 0)
        {
            vel = new Vector2(Mathf.Cos(ocilation * 10) * 5 + 0.5f, -speed / 5) * delta * speed;
            RotationDegrees = 90;
        }

        //* Shoot Right
        if(localLook == 1 && !localUp && !localDown)
        {
            vel = new Vector2(Mathf.Sin(ocilation * freq) * ampli + offset, Mathf.Cos(ocilation * freq) * ampli) * delta * speed;
            RotationDegrees = 0;
        }
        //* Shoot Left
        else if(localLook == 0 && !localUp && !localDown)
        {
            vel = new Vector2(-Mathf.Sin(ocilation * freq) * ampli - offset, Mathf.Cos(ocilation * freq) * ampli) * delta * speed;
            RotationDegrees = 0;
        }
        //*Shoot right down
        if(localLook == 1 && !localUp && isCrouch)
        {
            vel = new Vector2(Mathf.Sin(ocilation * freq) * ampli + offset, Mathf.Cos(ocilation * freq) * ampli) * delta * speed;
            RotationDegrees = 0;

        }
        //* Shoot Left down
        else if(localLook == 0 && !localUp && isCrouch)
        {
            vel = new Vector2(-Mathf.Sin(ocilation * freq) * ampli - offset, Mathf.Cos(ocilation * freq) * ampli) * delta * speed;
            RotationDegrees = 0;
        }

        //* Shoot Diagonal
        if(localUp && speedX > 0)
        {
            //vel = new Vector2(1,-1).Normalized() * speed * (float)delta;
            vel = (new Vector2(Mathf.Sin(ocilation * freq) * ampli + offset, Mathf.Cos(ocilation * freq) * ampli) * delta * speed) + new Vector2(0, -5);
            RotationDegrees = -45;
        }
        else if(localUp && speedX < 0)
        {
            vel = (new Vector2(-Mathf.Sin(ocilation * freq) * ampli - offset, Mathf.Cos(ocilation * freq) * ampli) * delta * speed) + new Vector2(0, -5);
            RotationDegrees = 45;
        }

        if(localDown && speedX > 0)
        {
            vel = (new Vector2(Mathf.Sin(ocilation * freq) * ampli + offset, Mathf.Cos(ocilation * freq) * ampli) * delta * speed) + new Vector2(0, 5);
            RotationDegrees = -135;
        }
        else if(localDown && speedX < 0)
        {
            vel = (new Vector2(-Mathf.Sin(ocilation * freq) * ampli - offset, Mathf.Cos(ocilation * freq) * ampli) * delta * speed) + new Vector2(0, 5);
            RotationDegrees = 135;
        }
        //* Shoot down while jump
        if(!isGrounded && speedX == 0 && localDown)
        {
            vel = new Vector2(-Mathf.Cos(ocilation * 10) * 5 + 0.5f, speed / 5) * delta * speed;
            RotationDegrees = -90;
        }
    }
    // Type 2 Accelerate
    public void ShootType2(float delta)
    {

        //* Shoot up
        if(localUp && speedX == 0)
        {
            vel += Vector2.Up * speed * delta;
            vel.X = 0;
            RotationDegrees = 90;
        }

        //* Shoot right
        if(localLook == 1 && !localUp && !localDown)
        {
            vel += Vector2.Right * speed * delta;
            vel.Y = 0;
            RotationDegrees = 0;

        }
        //* Shoot Left
        else if(localLook == 0 && !localUp && !localDown)
        {
            vel += Vector2.Left * speed * delta;
            vel.Y = 0;
            RotationDegrees = 0;
        }

        //*Shoot right down
        if(localLook == 1 && !localUp && isCrouch)
        {
            vel += Vector2.Right * speed * delta;
            vel.Y = 0;
            RotationDegrees = 0;

        }
        //* Shoot Left down
        else if(localLook == 0 && !localUp && isCrouch)
        {
            vel += Vector2.Left * speed * delta;
            vel.Y = 0;
            RotationDegrees = 0;
        }

        //* Shoot Diagonal
        if(localUp && speedX > 0)
        {
            vel += new Vector2(1,-1).Normalized() * speed * delta;
            RotationDegrees = -45;
        }
        else if(localUp && speedX < 0)
        {
            vel += new Vector2(-1,-1).Normalized() * speed * delta;
            RotationDegrees = 45;
        }

        if(localDown && speedX > 0)
        {
            vel += new Vector2(1,1).Normalized() * speed * delta;
            RotationDegrees = -135;
        }
        else if(localDown && speedX < 0)
        {
            vel += new Vector2(-1,1).Normalized() * speed * delta;
            RotationDegrees = 135;
        }
        //* Shoot down while jump
        if(!isGrounded && speedX == 0 && localDown)
        {
            vel += Vector2.Down * speed * delta;
            RotationDegrees = -90;
        }

    }

    // Type 3 Impresize shotgun
    public void ShootType3(float delta)
    {
        Rotation = randAngle;
        //vel = new Vector2(1, randAngle) * speed * delta;

        //* Shoot up
        if(localUp && speedX == 0)
        {
            vel = new Vector2(randAngle / 8, -1) * speed * delta;
            RotationDegrees = 90;
        }

        //* Shoot right
        if(localLook == 1 && !localUp && !localDown)
        {
            vel = new Vector2(1, randAngle / 8) * speed * delta;
            RotationDegrees = 0;

        }
        //* Shoot Left
        else if(localLook == 0 && !localUp && !localDown)
        {
            vel = new Vector2(-1, randAngle / 8) * speed * delta;
            RotationDegrees = 0;
        }
        //*Shoot right down
        if(localLook == 1 && !localUp && isCrouch)
        {
            vel = new Vector2(1, randAngle / 8) * speed * delta;
            RotationDegrees = 0;

        }
        //* Shoot Left down
        else if(localLook == 0 && !localUp && isCrouch)
        {
            vel = new Vector2(-1, randAngle / 8) * speed * delta;
            RotationDegrees = 0;
        }

        //* Shoot Diagonal
            
        if(localUp && speedX > 0)
        {
            vel = new Vector2(1 + randAngle / 8, -1 + randAngle / 8).Normalized() * speed * delta;
            
            
            RotationDegrees = -45;
        }
        else if(localUp && speedX < 0)
        {
            vel = new Vector2(-1 - randAngle / 8, -1 + randAngle / 8).Normalized() * speed * delta;
            RotationDegrees = 45;
        }

        if(localDown && speedX > 0)
        {
            vel = new Vector2(1 + randAngle / 8, 1 - randAngle / 8).Normalized() * speed * delta;
            RotationDegrees = -135;
        }
        else if(localDown && speedX < 0)
        {
            vel = new Vector2(-1 + randAngle / 8, 1 + randAngle / 8).Normalized() * speed * delta;
            RotationDegrees = 135;
        }
        //* Shoot down while jump
        if(!isGrounded && speedX == 0 && localDown)
        {
            vel = new Vector2(randAngle / 8, 1) * speed * delta;
            RotationDegrees = -90;
        }
        
    }
    public void ShootAny(int type, float delta)
    {
        switch (type)
        {
            case 0:
            speed = 800f;
            ShootType0(delta);
            break;
            case 1:
            speed = 55f;
            ShootType1(delta);
            break;
            case 2:
            speed = 50f;
            ShootType2(delta);
            break;
            case 3:
            speed = 850f;
            ShootType3(delta);
            break;
            default:
            speed = 800f;
            ShootType0(delta);
            break;
        }
    }
    public override void _PhysicsProcess(double delta)
    {
        vel = Velocity;
        ShootAny(gunType, (float)delta);

        var coli = MoveAndCollide(vel);
        if(coli != null)
        {
            GD.Print("Deleted due colission");
            QueueFree();
        }
        Velocity = vel;
    }
}
