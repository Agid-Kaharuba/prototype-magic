using Godot;

public enum StackResult
{
    DoesNotStack,
    ContinueStacking,
    StopStacking
}

public class Effect : Node2D {

    public readonly string effectName;
    private EffectController effectController;

    protected EffectController EffectController
    {
        get { return effectController; }
    }

    public Effect(string id)
    {
        this.effectName = id.ToLower();
        this.Name = this.effectName;
    }

    public override void _Ready()
    {
        effectController = (EffectController) GetParent();
    }

    public virtual StackResult StackEffects(Effect other) 
    {
        return StackResult.DoesNotStack;
    }
}