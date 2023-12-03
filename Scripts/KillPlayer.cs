using Godot;
using System;

public partial class KillPlayer : Area2D
{
    private AudioStreamPlayer playerSoundHit, playerSoundDeath;

    public void OnBodyEnter(CharacterBody2D body)
    {
        playerSoundHit = GetParent().GetNode<AudioStreamPlayer>("GotHit");
        playerSoundDeath = GetParent().GetParent().GetNode<AudioStreamPlayer>("PlayerKillViaEnemyColi");

        GD.Print("Player die due to colision with Bullet");
        var type = GetParent().GetNode<CharacterBody2D>(".").GetNode<Weapon>("Gun").gunType;
        if(type > 0)
        {
            //! Player got hit but didnt die
            playerSoundHit.Play();
            GetParent().GetNode<CharacterBody2D>(".").GetNode<Weapon>("Gun").gunType = 0;
            GetParent().GetNode<CharacterBody2D>(".").GetNode<Weapon>("Gun").count = 1;
        }
        else
        {
            playerSoundDeath.Play();
            var bruh = GetParent().GetParent().GetNode<Respawn>("Respawn");
            bruh.isDead = true;
            //!Play sound o player dying
            GetParent().QueueFree();
        }
    }
}
