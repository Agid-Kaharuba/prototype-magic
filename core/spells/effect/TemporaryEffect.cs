
public class TemporaryEffect : Effect
{
    protected float duration;
    protected float currentTime;
    private bool singleOnly;

    public float Duration 
    {
        get { return duration; }
    }

    public TemporaryEffect(string id, float duration, bool singleOnly = false) : base(id)
    {
        this.duration = duration;
        this.singleOnly = singleOnly;
    }

    public override void _PhysicsProcess(float delta)
    {
        currentTime += delta;

        if (currentTime >= duration)
        {
            OnEffectFinished();
        }
        else
        {
            OnEffectTick(delta);
        }
    }

    public override StackResult StackEffects(Effect other)
    {
        if (singleOnly && this.effectName == other.effectName)
        {
            this.currentTime = 0;
            return StackResult.StopStacking;
        }
        else
        {
            return base.StackEffects(other);
        }
    }

    protected virtual void OnEffectFinished()
    {
        QueueFree();
    }

    protected virtual void OnEffectTick(float delta) {}
}