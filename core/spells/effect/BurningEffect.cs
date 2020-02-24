using Godot;
public class BurningEffect : TemporaryEffect
{
    private float damage;
    
    public float Damage
    {
        get { return damage; }
    }

    public BurningEffect(float damage, float duration) : base("burning", duration)
    {
        this.damage = damage;
    }

    protected override void OnEffectTick(float delta)
    {
        float damageInDelta = damage * delta;
        GD.Print("Damage sending " + damageInDelta);
        EffectController.HealthAction((h) => h.TakeDamage(damageInDelta));
    }

    public override StackResult StackEffects(Effect other)
    {
        if (other is BurningEffect otherBurning && otherBurning.Name == Name)
        {
            currentTime = 0;
            damage = (damage + otherBurning.damage) * 1.2f;
            return StackResult.StopStacking;
        }
        return StackResult.DoesNotStack;
    }
}