using System;

class Game
{
    private Parser parser;
    private Player player;
    private bool gameWon;
    private bool powerRestored;

    public Game()
    {
        parser = new Parser();
        player = new Player();
        gameWon = false;
        powerRestored = false;
        CreateRooms();
    }

    private void CreateRooms()
    {
        // === ROOMS ===
        Room airlock = new Room("airlock", "A fractured airlock opens into a silent vessel.");
        Room cargoHold = new Room("cargoHold", "Loose containers drift in weak gravity.");
        Room reactorShaft = new Room("reactorShaft", "A vertical reactor column hums faintly.");
        Room maintenanceTube = new Room("maintenanceTube", "A narrow tube forces you upward.");
        Room commandBridge = new Room("commandBridge", "Dead consoles flicker in darkness.");
        Room crewQuarters = new Room("crewQuarters", "Sleeping pods hang open and empty.");
        Room medBay = new Room("medBay", "Cold lights reveal untouched supplies.");
        Room securitySector = new Room("securitySector", "A damaged defense unit hangs above.");
        Room powerCore = new Room("powerCore", "The main power core sits dormant.");
        Room escapeShuttle = new Room("escapeShuttle", "An emergency shuttle waits silently.");

        // === EXITS ===
        airlock.SetExit("east", cargoHold);

        cargoHold.SetExit("west", airlock);
        cargoHold.SetExit("north", reactorShaft);
        cargoHold.SetExit("up", maintenanceTube);

        maintenanceTube.SetExit("down", cargoHold);
        maintenanceTube.SetExit("up", commandBridge);

        commandBridge.SetExit("down", maintenanceTube);
        commandBridge.SetExit("south", securitySector);
        commandBridge.SetExit("west", crewQuarters);

        crewQuarters.SetExit("east", commandBridge);
        crewQuarters.SetExit("north", medBay);

        medBay.SetExit("south", crewQuarters);

        securitySector.SetExit("north", commandBridge);
        securitySector.SetExit("east", escapeShuttle);

        reactorShaft.SetExit("south", cargoHold);
        reactorShaft.SetExit("east", powerCore);

        powerCore.SetExit("west", reactorShaft);
        escapeShuttle.SetExit("west", securitySector);

        // === ITEMS ===
        cargoHold.Chest.Put("fusioncell", new Item(5, "A compact fusion power cell."));
        medBay.Chest.Put("medkit", new Item(3, "A portable medical kit."));
        crewQuarters.Chest.Put("medkit2", new Item(3, "A portable medical kit."));
        cargoHold.Chest.Put("medkit3", new Item(3, "A portable medical kit."));

        player.CurrentRoom = airlock;
    }

    public void Play()
    {
        Console.WriteLine("DRIFT PROTOCOL");
        Console.WriteLine(player.CurrentRoom.GetLongDescription());

        while (!gameWon && player.IsAlive())
        {
            Command command = parser.GetCommand();
            ProcessCommand(command);
        }
    }

    private void ProcessCommand(Command command)
    {
        if (command.IsUnknown())
        {
            Console.WriteLine("Unknown command.");
            return;
        }

        switch (command.CommandWord)
        {
            case "help":
                parser.ShowCommands();
                break;

            case "go":
                GoRoom(command);
                break;

            case "look":
                Console.WriteLine(player.CurrentRoom.GetLongDescription());
                break;

            case "quit":
                gameWon = true;
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
            Console.WriteLine("No path detected.");
            return;
        }

        if (nextRoom.Name == "escapeShuttle" && !powerRestored)
        {
            Console.WriteLine("The shuttle has no power.");
            return;
        }

        player.CurrentRoom = nextRoom;
        player.Damage(5);

        Console.WriteLine(player.CurrentRoom.GetLongDescription());
    }
}
