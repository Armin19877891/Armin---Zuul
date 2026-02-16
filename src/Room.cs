using System.Collections.Generic;

// Represents one room in the spaceship
public class Room
{
    private string name;
    private string description;
    private Dictionary<string, Room> exits;
    private Inventory chest;

    public Room(string name, string description)
    {
        this.name = name;
        this.description = description;
        exits = new Dictionary<string, Room>();
        chest = new Inventory(999999);
    }

    // Room internal name
    public string Name
    {
        get { return name; }
    }

    // Room short description
    public string Description
    {
        get { return description; }
    }

    public void SetExit(string direction, Room neighbor)
    {
        exits[direction] = neighbor;
    }

    public Room GetExit(string direction)
    {
        if (exits.ContainsKey(direction))
            return exits[direction];

        return null;
    }

    // FULL description with exits and items
    public string GetLongDescription()
    {
        return description +
               "\nExits: " + string.Join(" ", exits.Keys) +
               "\nItems: " + chest.Show();
    }

    public Inventory Chest
    {
        get { return chest; }
    }
}
