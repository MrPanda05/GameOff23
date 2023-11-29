using Godot;
using System;

public partial class EnemyArea : Area2D
{
    public void OnBodyEnter(CharacterBody2D body)
    {
        if(body.Name != "Player1")
        {
            GD.Print("penis");
            body.GetNode<Enemy>(".").isRight = !body.GetNode<Enemy>(".").isRight;
        }
        else
        {
            return;
        }
    }
}
