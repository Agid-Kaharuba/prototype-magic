using Godot;

public class Rune : Control
{
    private string id;
    
    public string ID
    {
        get 
        { 
            return id; 
        }
        set
        {
            id = value;
            Name = id;
        }
    }
}