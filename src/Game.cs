using System;

public class Game
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
            Command command = parser.GetCommand();
            ProcessCommand(command);

            if (!player.IsAlive())
            {
                Console.WriteLine("You died.");
                finished = true;
            }
        }

        Console.WriteLine("Thank you for playing. Goodbye.");
    }

    private void PrintWelcome()
    {
        Console.WriteLine("Welcome to Zuul!");
        Console.WriteLine("Type 'help' if you need help.");
        Console.WriteLine();
        Console.WriteLine(player.CurrentRoom.GetLongDescription());
    }

    private void CreateRooms()
    {
        Room outside = new Room("outside the main entrance");
        Room hall = new Room("in the main hall");
        Room upstairs = new Room("on the upper floor");
        Room basement = new Room("in the basement");

        outside.SetExit("north", hall);
        hall.SetExit("south", outside);

        hall.SetExit("up", upstairs);
        upstairs.SetExit("down", hall);

        outside.SetExit("down", basement);
        basement.SetExit("up", outside);

        player.CurrentRoom = outside;
    }

    private void ProcessCommand(Command command)
    {
        if (command.IsUnknown())
        {
            Console.WriteLine("I don't know what you mean...");
            return;
        }

        string commandWord = command.CommandWord;

        if (commandWord.Equals("help"))
        {
            PrintHelp();
        }
        else if (commandWord.Equals("go"))
        {
            GoRoom(command);
        }
        else if (commandWord.Equals("look"))
        {
            Look();
        }
        else if (commandWord.Equals("status"))
        {
            Status();
        }
        else if (commandWord.Equals("quit"))
        {
            finished = true;
        }
    }

    private void PrintHelp()
    {
        Console.WriteLine("You are lost. You are alone.");
        Console.WriteLine("Your command words are:");
        parser.ShowCommands();
    }

    private void Look()
    {
        Console.WriteLine(player.CurrentRoom.GetLongDescription());
    }

    private void Status()
    {
        Console.WriteLine("Health: " + player.Health);
    }

    private void GoRoom(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Go where?");
            return;
        }

        string direction = command.SecondWord;
        Room nextRoom = player.CurrentRoom.GetExit(direction);

        if (nextRoom == null)
        {
            Console.WriteLine("There is no door!");
        }
        else
        {
            player.CurrentRoom = nextRoom;
            player.Damage(10);
            Console.WriteLine(player.CurrentRoom.GetLongDescription());
        }
    }
}
