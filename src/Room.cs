using System.Collections.Generic;

public class Room
{
    private string name;
    private string darkDescription;
    private string lightDescription;
    private Dictionary<string, Room> exits;

    public Inventory Chest { get; }

    public Room(string name, string darkDesc, string lightDesc)
    {
        this.name = name;
        darkDescription = darkDesc;
        lightDescription = lightDesc;
        exits = new Dictionary<string, Room>();
        Chest = new Inventory(9999);
    }

    public string Name => name;

    public void SetExit(string direction, Room neighbor)
    {
        exits[direction] = neighbor;
    }

    public Room GetExit(string direction)
    {
        return exits.ContainsKey(direction) ? exits[direction] : null;
    }

    public string GetLongDescription(bool powerOn)
    {
        string desc = powerOn ? lightDescription : darkDescription;

        return $"You are in {name}.\n" +
               desc +
               "\nExits: " + string.Join(" ", exits.Keys) +
               "\nItems: " + Chest.Show();
    }
}
