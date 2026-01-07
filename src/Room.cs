using System;
using System.Collections.Generic;

public class Room
{
    // Private fields
    private string description;
    private Dictionary<string, Room> exits; // stores exits of this room

    // Create a room described "description"
    public Room(string desc)
    {
        description = desc;
        exits = new Dictionary<string, Room>();
    }

    // Define an exit from this room
    public void SetExit(string direction, Room neighbor)
    {
        exits[direction] = neighbor;
    }

    // Return the room that is reached if we go from this room in direction "direction"
    public Room GetExit(string direction)
    {
        if (exits.ContainsKey(direction))
        {
            return exits[direction];
        }
        return null;
    }

    // Return a description of the room
    public string GetLongDescription()
    {
        return "You are " + description + ".\n" + GetExitString();
    }

    // Return a string describing the room's exits
    private string GetExitString()
    {
        string str = "Exits: ";
        str += String.Join(", ", exits.Keys);
        return str;
    }
}
