using System;

// Reads player input
public class Parser
{
    private CommandLibrary commands;

    public Parser()
    {
        commands = new CommandLibrary();
    }

    public Command GetCommand()
    {
        Console.Write("> ");
        string input = Console.ReadLine()?.ToLower();
        string[] words = input?.Split(' ') ?? new string[0];

        string word1 = words.Length > 0 ? words[0] : null;
        string word2 = words.Length > 1 ? words[1] : null;
        string word3 = words.Length > 2 ? words[2] : null;

        if (!commands.IsCommand(word1))
            return new Command(null, null, null);

        return new Command(word1, word2, word3);
    }

    // Show all valid commands
    public void ShowCommands()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine(commands.ShowAll());
    }
}
