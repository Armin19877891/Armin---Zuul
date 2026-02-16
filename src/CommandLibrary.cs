using System.Collections.Generic;

// Stores all valid commands
public class CommandLibrary
{
    private HashSet<string> validCommands;

    public CommandLibrary()
    {
        validCommands = new HashSet<string>
        {
            "go",
            "help",
            "quit",
            "look",
            "status",
            "take",
            "drop",
            "use"
        };
    }

    public bool IsCommand(string commandWord)
    {
        return validCommands.Contains(commandWord);
    }

    public string ShowAll()
    {
        return string.Join(", ", validCommands);
    }
}
