using System.Linq;
using System.Collections.Generic;
using Godot;

public class Crafter : Control 
{
    private HBoxContainer runePanel; 
    private PackedScene runeScene;
    private List<Rune> runes = new List<Rune>();

    public override void _Ready()
    {
        runePanel = (HBoxContainer) GetNode("RuneSpace/RunePanel");
        runeScene = (PackedScene) ResourceLoader.Load("res://scenes/components/Rune.tscn");
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("action_craft"))
        {
            OnExitClicked();
        }
    }

    public void AddRuneToSpell(RuneButton runeButton)
    {
        Rune rune = CreateRune(runeButton.RuneID);
        runePanel.AddChild(rune);
        runes.Add(rune);
    }

    private Rune CreateRune(string id)
    {
        Texture texture = RuneStore.Instance.GetTexture(id);
        Rune rune = (Rune) runeScene.Instance();
        rune.ID = id;
        TextureRect runeTextureRect = (TextureRect) rune.GetNode("RuneTexture");
        runeTextureRect.Texture = texture;
        return rune;
    }

    public void OnCraft()
    {
        Spell spell = new Spell("Craftable", runes.Select(c => c.ID).ToList());
        ProjectilePhysicist phy = (ProjectilePhysicist) spell.Physicists.Where(p => p is ProjectilePhysicist).First();
        GD.Print(phy.ProjectileCount);
        SpellInventory.Instance.AddSpell(spell);
        GD.Print("Spell Inventory now has: ");
        SpellInventory.Instance.Spells.ForEach(s => GD.Print(s.Name));
        DeleteAllRunes();
    }

    public void OnDeleteRune()
    {
        DeleteLastRune();
    }

    public void OnExitClicked()
    {
        GetNode("/root/GameManager").Call("close_crafting");
    }

    private void DeleteLastRune()
    {
        Godot.Collections.Array runePanelChildren = runePanel.GetChildren();
        if (runePanelChildren.Count > 0)
        {
            Control lastChild = (Control) runePanelChildren[runePanelChildren.Count - 1];
            runePanel.RemoveChild(lastChild);
            runes.Remove(runes.Last());
        }
    }

    private void DeleteAllRunes()
    {
        foreach (Rune rune in runePanel.GetChildren())
        {
            rune.QueueFree();
        }
        runes.Clear();
    }
}
