using Godot;
using System;

namespace Commons.Components
{
    /// <summary>
    /// Health base class
    /// </summary>
    public partial class Health : Node2D
    {
        [Export] protected int maxHealth;
        protected int health;
       

        public virtual void Damage(int amount) { }

        public virtual int GetHealth()
        {
            return health;
        }
        public virtual int GetMaxHealth()
        {
            return maxHealth;
        }
        public virtual void SetHealth(int value) 
        { 
            health = value;
        }
        public virtual void SetMaxHealth(int value)
        {
            maxHealth = value;
        }
        public virtual void Heal(int amount)
        {
            health += amount;
        }

    }
}

