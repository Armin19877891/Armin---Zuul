using System;

class Game
{
    private Parser parser;
    private Player player;
    private bool powerOn;
    private bool gameOver;

    public Game()
    {
        parser = new Parser();
        player = new Player();
        powerOn = false;
        gameOver = false;
        CreateRooms();
    }

    private void CreateRooms()
    {
        Room airlock = new Room("airlock",
            "The entry airlock is dark and silent.",
            "The entry airlock is brightly illuminated.");

        Room cargoHold = new Room("cargoHold",
            "Containers float in shadow.",
            "Cargo crates sit secured under bright lights.");

        Room reactorShaft = new Room("reactorShaft",
            "The reactor shaft is dormant.",
            "Energy flows through the reactor systems.");

        Room maintenanceTube = new Room("maintenanceTube",
            "A narrow vertical shaft disappears upward.",
            "Maintenance lights guide the vertical climb.");

        Room commandBridge = new Room("commandBridge",
            "The bridge consoles are lifeless.",
            "Navigation systems glow across the bridge.");

        Room crewQuarters = new Room("crewQuarters",
            "Sleeping pods drift in darkness.",
            "Crew cabins are softly lit.");

        Room medBay = new Room("medBay",
            "Medical tools float in dim light.",
            "Medical systems hum with restored power.");

        Room securitySector = new Room("securitySector",
            "A broken turret hangs from the ceiling.",
            "Security systems scan the corridor.");

        Room powerCore = new Room("powerCore",
            "The core is completely inactive.",
            "The core radiates stable energy.");

        Room escapeShuttle = new Room("escapeShuttle",
            "The shuttle sits powerless.",
            "The shuttle engines are ready for launch.");

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

        cargoHold.Chest.Put("fusioncell", new Item(10, "Fusion cell"));
        medBay.Chest.Put("medkit1", new Item(5, "Medical kit"));
        crewQuarters.Chest.Put("medkit2", new Item(5, "Medical kit"));
        securitySector.Chest.Put("medkit3", new Item(5, "Medical kit"));

        player.CurrentRoom = airlock;
    }

    public void Play()
    {
        Console.WriteLine("DRIFT PROTOCOL");
        Console.WriteLine("Type 'help' to see commands.\n");
        Console.WriteLine(player.CurrentRoom.GetLongDescription(powerOn));

        while (!gameOver && player.IsAlive())
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
            case "help": parser.ShowCommands(); break;
            case "go": GoRoom(command); break;
            case "look": Console.WriteLine(player.CurrentRoom.GetLongDescription(powerOn)); break;
            case "status": player.ShowStatus(); break;
            case "take": if (command.HasSecondWord()) player.TakeFromRoom(command.SecondWord); break;
            case "drop": if (command.HasSecondWord()) player.DropToRoom(command.SecondWord); break;
            case "use": Use(command); break;
            case "escape": Escape(); break;
            case "quit": gameOver = true; break;
        }
    }

    private void GoRoom(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Go where?");
            return;
        }

        Room next = player.CurrentRoom.GetExit(command.SecondWord);

        if (next == null)
        {
            Console.WriteLine("No path.");
            return;
        }

        player.CurrentRoom = next;

        string cause = next.Name switch
        {
            "securitySector" => "a broken turret",
            "reactorShaft" => "radiation exposure",
            "cargoHold" => "loose wiring",
            _ => "unstable flooring"
        };

        player.Damage(10, cause);

        Console.WriteLine(player.CurrentRoom.GetLongDescription(powerOn));
    }

    private void Use(Command command)
    {
        if (!command.HasSecondWord())
            return;

        string item = command.SecondWord;

        if (item.StartsWith("medkit") && player.HasItem(item))
        {
            player.Heal(50);
            player.RemoveItem(item);
            return;
        }

        if (item == "fusioncell")
        {
            if (player.CurrentRoom.Name == "powerCore" && player.HasItem("fusioncell"))
            {
                powerOn = true;
                player.RemoveItem("fusioncell");
                Console.WriteLine("Power restored.");
            }
            else
            {
                Console.WriteLine("No place found to put fusioncell.");
            }
        }
    }

    private void Escape()
    {
        if (player.CurrentRoom.Name == "escapeShuttle" && powerOn)
        {
            Console.WriteLine("You launch the shuttle and escape.");
            gameOver = true;
        }
        else
        {
            Console.WriteLine("Escape not possible.");
        }
    }
}
