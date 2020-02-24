using System.Collections.Generic;
using Godot;

public class SpellInventory : Node2D
{
    private List<Spell> spells = new List<Spell>();
    private static SpellInventory spellInventory;

    public static SpellInventory Instance
    {
        get { return spellInventory; }
    }

    public List<Spell> Spells
    {
        get { return spells; }
    }

    public override void _Ready()
    {
        spellInventory = this;
    }

    public void AddSpell(Spell spell)
    {
        spells.Insert(0, spell);
        if (spells.Count == 5)
            spells.RemoveAt(4);
    }

    public void CastSpellAtSlot(int index, KinematicBody2D player)
    {
        if (index < spells.Count)
            spells[index].CastSpell(player);
    }
}