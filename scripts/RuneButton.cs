using System;
using Godot;

[Tool]
public class RuneButton : Control 
{
    [Export]
    private Texture runeTexture;
    private TextureRect runeTextureRect;
    private Crafter crafter;

    public string RuneID
    {
        get { return Name.ToLower(); }
    }

    public override void _Ready()
    {
        runeTextureRect = (TextureRect) GetNode("Button/RuneTexture");
        if (runeTexture != null)
            runeTextureRect.Texture = runeTexture;
        crafter = GetParent().GetParent().GetParent() as Crafter;
    }

    public override void _Process(float delta)
    {

    }

    public void OnButtonPressed()
    {
        crafter.AddRuneToSpell(this);
    }
}
