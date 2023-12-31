using Godot;
using System;

public partial class powerupSet : Node2D
{
    [Export] private int gunType = 0, count = 1;
    [Export] private float fireRate = 0.8f;

    [Export] public Texture2D gunSprite;


    public Texture2D setText()
    {
        return gunSprite;
    }
    public int GetGunType()
    {
        return gunType;
    }
    public int GetGunCount()
    {
        return count;
    }

    public float GetFireRate()
    {
        return fireRate;
    }
}
