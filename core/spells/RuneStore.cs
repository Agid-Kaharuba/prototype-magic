using System.Collections.Generic;
using System.Collections;
using System;
using Godot;

public class RuneStore
{
    private static RuneStore instance;
    private Dictionary<string, Texture> runes = new Dictionary<string, Texture>();

    public static RuneStore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RuneStore();
            }
            return instance;
        }
    }

    private RuneStore() 
    {
    }

    public Texture GetTexture(string id)
    {
        if (!runes.ContainsKey(id))
        {
            runes[id] = (Texture) GD.Load("res://assets/runes/" + id + ".png");
        }
        return runes[id];
    }
}