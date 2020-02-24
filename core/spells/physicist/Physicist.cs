using System;
using System.Collections.Generic;

public abstract class Physicist
{
    protected Spell spell;

    public static List<Physicist> HirePhysicists(Spell spell)
    {
        return new List<Physicist>(new Physicist[] { new ProjectilePhysicist(spell) });
    }

    public Physicist(Spell spell)
    {
        this.spell = spell;
    }

    public abstract void ProcessRune(string runeID);

    public abstract bool ShouldCreatePhysics();

    public abstract SpellPhysics CreatePhysics(Caster caster);
}