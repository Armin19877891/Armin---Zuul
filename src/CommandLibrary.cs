using System.Collections.Generic;

namespace Zuul
{
    class CommandLibrary
    {
        private HashSet<string> validCommands;

        public CommandLibrary()
        {
            validCommands = new HashSet<string>
            {
                "go",
                "quit",
                "help",
                "look",
                "take",
                "drop",
                "status"
            };
        }

        public bool IsValidCommand(string commandWord)
        {
            return validCommands.Contains(commandWord);
        }

        public string ShowAll()
        {
            return string.Join(" ", validCommands);
        }
    }
}
