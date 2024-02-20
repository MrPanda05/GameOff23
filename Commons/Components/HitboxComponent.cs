using Godot;
using System;


namespace Commons.Components
{
	public partial class HitboxComponent : Area2D
	{
		[Export] private Health healthComponent;

		public void Damage(int amount = 1)
		{
			healthComponent?.Damage(amount);
		}
	}
}
