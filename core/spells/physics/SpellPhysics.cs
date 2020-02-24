using Godot;

public abstract class SpellPhysics : Node2D
{
    protected Caster caster;

    public SpellPhysics(Caster caster)
    {
        this.caster = caster;
    }

    public virtual bool IsSpellFinished()
    {
        return true;
    }
}