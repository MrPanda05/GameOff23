using Godot;
using System;


namespace Commons.Components
{
    /// <summary>
    /// Health component where the parent IS queue free on death
    /// </summary>
    public partial class HealthComponent : Health
    {
        public override void Damage(int amount = 1)
        {
            health -= amount;
            if (health <= 0)
            {
                GD.Print("Death");
                GetParent().QueueFree();
            }
        }
    }
}
