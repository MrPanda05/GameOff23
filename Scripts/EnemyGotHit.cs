using Godot;
using System;

public partial class EnemyGotHit : Area2D
{
    private int health = 3;

    private AudioStreamPlayer enemySoundHit, playerSoundDeath, enemyDeath;

    //Kill Enemy or player

    public override void _Ready()
    {
        enemySoundHit = GetParent().GetParent().GetNode<AudioStreamPlayer>("EnemyHit");
        playerSoundDeath = GetParent().GetParent().GetNode<AudioStreamPlayer>("PlayerKillViaEnemyColi");
    }
    public void OnBodyEnter(CharacterBody2D body)
    {
        if(body.Name == "Player1")
        {
            //! Play sound of player dying
            GD.Print("Player die due to colision with enemy");
            playerSoundDeath.Play();
            if(Name == "KillBox")
            {
                var bruh = GetParent().GetNode<Respawn>("Respawn");
                bruh.isDead = true;
                body.QueueFree();
                return;
            }
            else
            {
                var bruh = GetParent().GetParent().GetNode<Respawn>("Respawn");
                bruh.isDead = true;
                body.QueueFree();
            }
        }
        else
        {
            //! Play sound of enemy dying
            if(health <= 0)
            {
                GD.Print("E");
                body.QueueFree();
                GetParent().QueueFree();

            }
            else
            {
                //! sound of enemy hit
                enemySoundHit.Play();
                health -= 1;
                body.QueueFree();
            }
        }
    }
}
