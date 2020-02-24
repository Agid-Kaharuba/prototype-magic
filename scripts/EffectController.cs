using System;
using Godot;
using System.Collections.Generic;

public class EffectController : Node2D 
{
	private List<Effect> effects = new List<Effect>();
	private HealthController healthController;

	public override void _Ready()
	{
		healthController = GetParent().GetNode("HealthController") as HealthController;
	}

	public void HealthAction(Action<HealthController> action)
	{
		if (healthController != null)
		{
			action(healthController);
		}
	}

	public void AddEffect(Effect effect)
	{
		bool shouldCreateEffect = true;

		foreach (Effect child in GetChildren())
		{
			StackResult result = child.StackEffects(effect);

			if (result == StackResult.StopStacking)
			{
				shouldCreateEffect = false;
				break;
			} 
			else if (result == StackResult.ContinueStacking)
			{
				shouldCreateEffect = false;
			}
		}

		if (shouldCreateEffect)
		{
			AddChild(effect);
		}
		else
		{
			// effect.QueueFree();
		}
	}

	public bool HasEffect(string effectName)
	{
		foreach (Effect child in GetChildren())
		{
			if (child.effectName == effectName)
			{
				return true;
			}
		}
		return false;
	}
}
