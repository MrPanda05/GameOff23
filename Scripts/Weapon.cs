using Godot;
using System;

public partial class Weapon : Node2D
{
    PackedScene bullet = GD.Load<PackedScene>("res://Scenes/bullet.tscn");
    private Timer timer;

    private float newDelay;

    public int count = 1;

    public int gunType = 0;

    private AudioStreamPlayer audioPlayer;

    [Export] public float fireDelay;
    public override void _Ready()
    {
        timer = GetParent().GetNode<Timer>("Timer");
        audioPlayer = GetNode<AudioStreamPlayer>("ShootFx");
    }
    public void Shoot()
    {
            
            timer.Start(fireDelay);
            audioPlayer.Play();
            for (int i = 0; i < count; i++)
            {
                //! Play sound on Type
                var newBullet = bullet.Instantiate() as CharacterBody2D;
                GetParent().GetParent().AddChild(newBullet);
                newBullet.Name = "Bullet" + count;
                newBullet.GlobalPosition = new Vector2(GlobalPosition.X, GlobalPosition.Y);
            }
    }
    public override void _PhysicsProcess(double delta)
    {
        if(Input.IsActionPressed("Shoot1") && timer.IsStopped())
        {
            Shoot();
        }
    }
}
