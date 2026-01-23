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

            string word1 = null;
            string word2 = null;

            string[] words = inputLine.Split(' ');

            if (words.Length > 0)
                word1 = words[0];

            if (words.Length > 1)
                word2 = words[1];

            if (commandLibrary.IsValidCommand(word1))
            {
                return new Command(word1, word2);
            }
            else
            {
                return new Command(null, word2);
            }
        }
    }
}
