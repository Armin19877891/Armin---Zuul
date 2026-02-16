// Represents one user command
public class Command
{
    public string CommandWord { get; }
    public string SecondWord { get; }
    public string ThirdWord { get; }

    public Command(string commandWord, string secondWord, string thirdWord)
    {
        CommandWord = commandWord;
        SecondWord = secondWord;
        ThirdWord = thirdWord;
    }

    public bool HasSecondWord()
    {
        return SecondWord != null;
    }

    public bool HasThirdWord()
    {
        return ThirdWord != null;
    }

    // Check if command is unknown
    public bool IsUnknown()
    {
        return CommandWord == null;
    }
}
