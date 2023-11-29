using Godot;
using System;

public partial class PowerUpTest : Area2D
{
    private powerupSet getter;
    public override void _Ready()
    {
        getter = GetParent().GetNode<powerupSet>(".");
        GD.Print(getter.GetGunCount());
    }
    private void OnBodyEnter(CharacterBody2D body)
    {
        body.GetNode<Weapon>("Gun").gunType = getter.GetGunType();
        body.GetNode<Weapon>("Gun").fireDelay = getter.GetFireRate();
        body.GetNode<Weapon>("Gun").count = getter.GetGunCount();
        GetParent().QueueFree();
    }
}
