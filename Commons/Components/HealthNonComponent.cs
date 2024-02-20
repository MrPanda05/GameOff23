using Godot;
using System;

namespace Commons.Components
{
    /// <summary>
    /// Health component where the parent is NOT queue free on death
    /// </summary>
    public partial class HealthNonComponent : Health
    {
        public override void Damage(int amount = 1)
        {
            health -= amount;
            if (health <= 0)
            {
                GD.Print("Death");
                Node2D parent = (Node2D)GetParent();
                parent.Visible = false;
                parent.ProcessMode = ProcessModeEnum.Disabled;
            }
        }
    }
}
