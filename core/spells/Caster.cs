using System.Linq;
using Godot;
using System.Collections.Generic;

public class Caster : Node2D
{
    private Node2D entity;

    public Node2D Entity
    {
        get { return entity; }
    }

    public Caster(Node2D entity)
    {
        this.entity = entity;
    }

    public void AddSpellPhysics(SpellPhysics spellPhysics)
    {
        AddChild(spellPhysics);
        spellPhysics.Connect("tree_exited", this, nameof(OnSpellPhysicsExit));
    }

    public void OnSpellPhysicsExit()
    {
        if (GetChildCount() == 0)
        {
            GD.Print($"Removing caster {Name}!");
            GetParent().RemoveChild(this);
        }
    }

    public Vector2 GetAimVector()
    {
        return GetLocalMousePosition().Normalized();
    }

    public bool IsSpellFinished()
    {
        foreach (SpellPhysics spellPhysics in GetChildren())
        {
            if (!spellPhysics.IsSpellFinished())
                return false;
        }
        return true;
    }
}