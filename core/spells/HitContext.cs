using Godot;
using System.Collections.Generic;

public class HitContext
{
    public readonly Node2D node;
    public Vector2 direction;
    private EffectController effectController;
    private bool hasSearchedEffectController;
    private HealthController healthController;
    private bool hasSearchedHealthController;
    private float damageTaken;
    private float healTaken;
    private float damageAmplification;
    private float healAmplification;
    private List<Effect> effectsToAdd = new List<Effect>();

    private EffectController EffectController 
    {
        get
        {
            if (!hasSearchedEffectController)
            {
                effectController = node.GetNode("EffectController") as EffectController;
                hasSearchedEffectController = true;
            }
            return effectController;
        }
    }
    private HealthController HealthController
    {
        get
        {
            if (!hasSearchedHealthController)
            {
                healthController = node.GetNode("HealthController") as HealthController;
                hasSearchedHealthController = true;
            }
            return healthController;
        }
    }
    public Vector2 Direction
    {
        get { return direction; }
    }
    
    
    public HitContext(Node2D node,Vector2 direction = new Vector2())
    {
        this.node = node;
        this.direction = direction;
    }

    public void ApplyHitActions()
    {
        if (HealthController != null)
        {
            float damage = damageTaken * (damageAmplification == 0 ? 1 : damageAmplification);
            HealthController.TakeDamage(damage);
        }

        if (HealthController != null)
        {
            float heal = healAmplification * (healAmplification == 0 ? 1 : healAmplification);
            HealthController.TakeHeal(heal);
        }

        if (EffectController != null)
        {
            effectsToAdd.ForEach(e => EffectController.AddEffect(e));
        }
    }

    public void TakeDamage(float amount)
    {
        damageTaken += amount;   
    }

    public void AmplifyDamage(float amount)
    {
        damageAmplification += amount;
    }

    public void TakeHeal(float amount)
    {
        healAmplification += amount;
    }

    public void AmplifyHeal(float amount)
    {
        healTaken *= amount;
    }

    public void AddEffect(Effect effect)
    {
        effectsToAdd.Add(effect);
    }

    public bool HadEffect(string effectName)
    {
        if (EffectController != null)
        {
            return EffectController.HasEffect(effectName);
        }
        return false;
    }
}