using Godot;
using System;

public partial class PowerUpTest : Area2D
{
    private powerupSet getter;

    private AudioStreamPlayer audioPlayer;
    public override void _Ready()
    {
        getter = GetParent().GetNode<powerupSet>(".");
        GetParent().GetNode<Sprite2D>("Sprite2D").Texture = getter.gunSprite;
        audioPlayer = GetParent().GetParent().GetNode<AudioStreamPlayer>("PickUp");

    }
    private void OnBodyEnter(CharacterBody2D body)
    {
        //! Play sound on picking up
        body.GetNode<Weapon>("Gun").gunType = getter.GetGunType();
        audioPlayer.Play();
        body.GetNode<Weapon>("Gun").fireDelay = getter.GetFireRate();
        body.GetNode<Weapon>("Gun").count = getter.GetGunCount();
        GetParent().QueueFree();
    }
}
