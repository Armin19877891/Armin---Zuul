using System;

namespace Zuul
{
    class Game
    {
        private Parser parser;
        private Player player;
        private bool finished;

        public Game()
        {
            parser = new Parser();
            player = new Player();
            CreateRooms();
        }

        public void Play()
        {
            PrintWelcome();

            while (!finished)
            {
                if (!player.IsAlive())
                {
                    Console.WriteLine("You have died. Game over.");
                    break;
                }

                Command command = parser.GetCommand();
                ProcessCommand(command);
            }

            Console.WriteLine("Thank you for playing.");
        }

        private void PrintWelcome()
        {
            Console.WriteLine("Welcome to Zuul!");
            Console.WriteLine("Type 'help' if you need help.");
            Console.WriteLine();
            PrintRoomInfo();
        }

        private void CreateRooms()
        {
            Room outside = new Room("outside the main entrance");
            Room hall = new Room("in the main hall");
            Room hallUpstairs = new Room("on the upper floor of the hall");
            Room lab = new Room("inside the laboratory");

            // Horizontal exits
            outside.SetExit("north", hall, true);
            hall.SetExit("south", outside);

            hall.SetExit("east", lab, true);
            lab.SetExit("west", hall);

            hall.SetExit("up", hallUpstairs);
            hallUpstairs.SetExit("down", hall);

            // Items
            outside.Chest.Put("key1", new Item(1, "A small brass key"));
            hall.Chest.Put("key2", new Item(1, "A heavy iron key"));
            lab.Chest.Put("medkit", new Item(3, "Restores health"));

            player.CurrentRoom = outside;
        }

        private void ProcessCommand(Command command)
        {
            if (command.IsUnknown())
            {
                Console.WriteLine("I don't know what you mean...");
                return;
            }

            switch (command.CommandWord)
            {
                case "help":
                    PrintHelp();
                    break;
                case "go":
                    GoRoom(command);
                    break;
                case "look":
                    PrintRoomInfo();
                    break;
                case "take":
                    Take(command);
                    break;
                case "drop":
                    Drop(command);
                    break;
                case "status":
                    PrintStatus();
                    break;
                case "use":
                    Use(command);
                    break;
                case "quit":
                    finished = true;
                    break;
            }
        }

        private void GoRoom(Command command)
        {
            if (!command.HasSecondWord())
            {
                Console.WriteLine("Go where?");
                return;
            }

            Room nextRoom = player.CurrentRoom.GetExit(command.SecondWord);

            if (nextRoom == null)
            {
                Console.WriteLine("That exit is locked or does not exist.");
            }
            else
            {
                player.CurrentRoom = nextRoom;
                player.Damage(5);
                PrintRoomInfo();
            }
        }

        private void Use(Command command)
        {
            if (!command.HasSecondWord() || !command.HasThirdWord())
            {
                Console.WriteLine("Use what where?");
                return;
            }

            Console.WriteLine(player.Use(command.SecondWord, command.ThirdWord));
        }

        private void Take(Command command)
        {
            if (!command.HasSecondWord())
            {
                Console.WriteLine("Take what?");
                return;
            }

            player.TakeFromChest(command.SecondWord);
        }

        private void Drop(Command command)
        {
            if (!command.HasSecondWord())
            {
                Console.WriteLine("Drop what?");
                return;
            }

            player.DropToChest(command.SecondWord);
        }

        private void PrintStatus()
        {
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine("Backpack: " + player.BackpackContents());
        }

        private void PrintRoomInfo()
        {
            Console.WriteLine("You are " + player.CurrentRoom.GetDescription());
            Console.WriteLine("Exits: " + player.CurrentRoom.GetExitString());
            Console.WriteLine("Items here: " + player.CurrentRoom.Chest.Show());
        }

        private void PrintHelp()
        {
            Console.WriteLine("Your command words are:");
            Console.WriteLine(parser.CommandLibrary.ShowAll());
        }
    }
}
