using System;
using Godot;
using System.Collections.Generic;

public class ProjectilePhysicist : Physicist
{
    private int count = 0;
    private int size = 5;
    private int speedMultiplier = 10;
    private const int BaseSpeed = 1600;
    private float castDelay = 0.2f;
    private int spread = 0;
    private float cooldownOnFinish = 0.08f;
    private List<Action<HitContext>> hitActions = new List<Action<HitContext>>();

    public int Spread 
    {
        get { return spread; }
    }
    public int ProjectileCount 
    {
        get { return count; }
    }
    public int Size
    {
        get { return size; }
    }
    public int Speed
    {
        get { return BaseSpeed; }
    }
    public float CastDelay
    {
        get { return castDelay; }
    }
    public float CooldownOnFinish
    {
        get { return cooldownOnFinish; }
    }
    public List<Action<HitContext>> HitActions
    {
        get { return hitActions; }
    }

    public ProjectilePhysicist(Spell spell) : base(spell) {}

    public override bool ShouldCreatePhysics()
    {
        return count > 0;
    }

    public override SpellPhysics CreatePhysics(Caster caster)
    {
        return new ProjectilePhysics(caster, this);
    }

    public override void ProcessRune(string runeID)
    {
        switch (runeID)
        {
            case "projectile":
                count++;
                speedMultiplier *= 2;
                castDelay -= 0.035f;
                cooldownOnFinish += 0.15f;
                break;
            case "spread":
                spread++;
                break;
            case "fire":
                hitActions.Add(hit => 
                {
                    hit.AddEffect(new BurningEffect(2, 6));
                    hit.TakeDamage(20);
                });
                break;
            case "force":
                hitActions.Add(hit =>
                {
                    hit.node.GlobalPosition += hit.direction * 10;
                });
                break;
            case "water":
                hitActions.Add(hit =>
                {
                    hit.AddEffect(new TemporaryEffect("wet", 10, true));
                });
                break;
            case "electric":
                hitActions.Add(hit =>
                {
                    hit.TakeDamage(5);
                    if (hit.HadEffect("wet"))
                        hit.AmplifyDamage(2);
                });
                break;
        }
        GD.Print("Encountered rune " + runeID);
    }
}