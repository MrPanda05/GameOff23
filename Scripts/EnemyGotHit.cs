using Godot;
using System;

public partial class EnemyGotHit : Area2D
{

    //Kill Enemy or player
    public void OnBodyEnter(CharacterBody2D body)
    {
        if(body.Name == "Player1")
        {
            //! Play sound of player dying
            GD.Print("Player die due to colision with enemy");
            body.QueueFree();
        }
        else
        {
            //! Play sound of enemy dying
            body.QueueFree();
            GetParent().QueueFree();
        }
    }
}
