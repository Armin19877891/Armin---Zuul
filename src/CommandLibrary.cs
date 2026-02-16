using System.Collections.Generic;

public class CommandLibrary
{
    private HashSet<string> commands;

    public CommandLibrary()
    {
        commands = new HashSet<string>
        {
            "go","help","quit","look","status",
            "take","drop","use","escape"
        };
    }

    public bool IsCommand(string word)
    {
        return commands.Contains(word);
    }

    public string ShowAll()
    {
        return string.Join(" ", commands);
    }
}
