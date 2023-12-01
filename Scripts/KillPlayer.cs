using Godot;
using System;

public partial class KillPlayer : Area2D
{
    public void OnBodyEnter(CharacterBody2D body)
    {
        GD.Print("Player die due to colision with Bullet");
        var type = GetParent().GetNode<CharacterBody2D>(".").GetNode<Weapon>("Gun").gunType;
        if(type > 0)
        {
            //! Player got hit but didnt die
            GetParent().GetNode<CharacterBody2D>(".").GetNode<Weapon>("Gun").gunType = 0;
            GetParent().GetNode<CharacterBody2D>(".").GetNode<Weapon>("Gun").count = 1;
        }
        else
        {
            var bruh = GetParent().GetParent().GetNode<Respawn>("Respawn");
            bruh.isDead = true;
            //!Play sound o player dying
            GetParent().QueueFree();
        }
    }
}
