using Godot;
using System;

public partial class EnemyArea : Area2D
{

    //Change enemy direction
    public void OnBodyEnter(CharacterBody2D body)
    {
        if(body.Name != "Player1")
        {
            body.GetNode<Enemy>(".").isRight = !body.GetNode<Enemy>(".").isRight;
        }
        else
        {
            return;
        }
    }
}
