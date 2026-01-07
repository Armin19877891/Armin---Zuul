using System;

public class Parser
{
    private CommandLibrary commandLibrary;

    public Parser()
    {
        commandLibrary = new CommandLibrary();
    }

    public Command GetCommand()
    {
        Console.Write("> ");
        string input = Console.ReadLine();
        string[] words = input.Split(" ");

        string word1 = null;
        string word2 = null;

        if (words.Length > 0) word1 = words[0];
        if (words.Length > 1) word2 = words[1];

        if (commandLibrary.IsValidCommandWord(word1))
        {
            return new Command(word1, word2);
        }
        else
        {
            return new Command(null, word2);
        }
    }

    // REQUIRED by Game.cs
    public void ShowCommands()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine(commandLibrary.GetCommandsString());
    }
}
