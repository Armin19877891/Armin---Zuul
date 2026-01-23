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
            finished = false;
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
            // Ground floor
            Room outside = new Room("outside the main entrance");
            Room hall = new Room("in a large hall");
            Room lab = new Room("inside a laboratory");

            // Upper floor
            Room balcony = new Room("on a balcony overlooking the hall");

            // Connections (horizontal)
            outside.SetExit("north", hall);
            hall.SetExit("south", outside);
            hall.SetExit("east", lab);
            lab.SetExit("west", hall);

            // Connections (vertical)
            hall.SetExit("up", balcony);
            balcony.SetExit("down", hall);

            // Items
            outside.Chest.Put("key", new Item(1, "A rusty key"));
            outside.Chest.Put("torch", new Item(2, "A wooden torch"));
            hall.Chest.Put("map", new Item(1, "A map of the area"));
            lab.Chest.Put("medkit", new Item(3, "Restores health"));
            balcony.Chest.Put("coin", new Item(1, "An old gold coin"));

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
                case "quit":
                    finished = true;
                    break;
            }
        }

        private void PrintHelp()
        {
            Console.WriteLine("You are lost.");
            Console.WriteLine("Your command words are:");
            Console.WriteLine(parser.CommandLibrary.ShowAll());
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
                Console.WriteLine("There is no exit that way!");
            }
            else
            {
                player.CurrentRoom = nextRoom;
                player.Damage(5); // health loss on movement
                PrintRoomInfo();
            }
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
    }
}
