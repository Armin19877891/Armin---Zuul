public class Command
{
    public string CommandWord { get; }
    public string SecondWord { get; }

    public Command(string word1, string word2)
    {
        CommandWord = word1;
        SecondWord = word2;
    }

    public bool HasSecondWord()
    {
        return SecondWord != null;
    }

    public bool IsUnknown()
    {
        return CommandWord == null;
    }
}
