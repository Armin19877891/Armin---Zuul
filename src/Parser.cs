using System;

namespace Zuul
{
    class Parser
    {
        private CommandLibrary commandLibrary;

        public Parser()
        {
            commandLibrary = new CommandLibrary();
        }

        public CommandLibrary CommandLibrary
        {
            get { return commandLibrary; }
        }

        public Command GetCommand()
        {
            Console.Write("> ");
            string inputLine = Console.ReadLine();
            string[] words = inputLine.Split(' ');

            string word1 = null;
            string word2 = null;
            string word3 = null;

            if (words.Length > 0) word1 = words[0];
            if (words.Length > 1) word2 = words[1];
            if (words.Length > 2) word3 = words[2];

            if (commandLibrary.IsValidCommand(word1))
            {
                return new Command(word1, word2, word3);
            }

            return new Command(null, null, null);
        }
    }
}
