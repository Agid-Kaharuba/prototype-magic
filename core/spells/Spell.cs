using Godot;
using System.Collections.Generic;

public class Spell
{
    private readonly string name;
    private List<Physicist> physicists;
    private List<string> runes;
    private Caster lastCaster;

    public List<Physicist> Physicists
    {
        get { return physicists; }
    }

    public string Name
    {
        get { return name; }
    }

    public Spell(string name, List<string> runes)
    {
        this.name = name;
        runes = new List<string>(runes);
        physicists = Physicist.HirePhysicists(this);
        runes.ForEach(r => physicists.ForEach(p => p.ProcessRune(r)));
    }

    public void CastSpell(KinematicBody2D player)
    {
        if (CanSpellCast())
        {
            Caster caster = new Caster(player);

            // Add the caster to the scene first.
            caster.GlobalPosition = player.GlobalPosition;
            player.GetParent().AddChild(caster);

            // Process the physicists
            foreach (Physicist physicist in physicists)
            {
                if (physicist.ShouldCreatePhysics())
                {
                    SpellPhysics physics = physicist.CreatePhysics(caster);
                    caster.AddSpellPhysics(physics);
                }
            }
            lastCaster = caster;
            GD.Print($"Generating caster with {caster.GetChildCount()} physics");
        }
    }

    private bool CanSpellCast()
    {
        return lastCaster == null || lastCaster.IsQueuedForDeletion() || lastCaster.IsSpellFinished();
    }
}