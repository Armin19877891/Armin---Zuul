using System.Collections.Generic;

public class CommandLibrary
{
    private Dictionary<string, string> commands;

    public CommandLibrary()
    {
        commands = new Dictionary<string, string>();

        // valid commands
        commands["go"] = "move to another room";
        commands["quit"] = "quit the game";
        commands["help"] = "show help";
        commands["look"] = "look around";
        commands["status"] = "show player status";
        commands["take"] = "take an item";
        commands["drop"] = "drop an item";
        commands["use"] = "use an item";
    }

    // check if command exists
    public bool IsValidCommandWord(string commandWord)
    {
        return commands.ContainsKey(commandWord);
    }

    // return all commands as a string
    public string GetCommandsString()
    {
        return string.Join(" ", commands.Keys);
    }
}
