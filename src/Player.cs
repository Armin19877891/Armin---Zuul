using System;

public class Player
{
    public Room CurrentRoom { get; set; }

    private int health;
    private Inventory backpack;

    public Player()
    {
        health = 100;
        backpack = new Inventory(25);
        CurrentRoom = null;
    }

    // Player loses health
    public void Damage(int amount)
    {
        health -= amount;
        if (health < 0)
            health = 0;
    }

    // Player gains health
    public void Heal(int amount)
    {
        health += amount;
        if (health > 100)
            health = 100;
    }

    // Check if alive
    public bool IsAlive()
    {
        return health > 0;
    }

    // Show status
    public void ShowStatus()
    {
        Console.WriteLine("Health: " + health);
        Console.WriteLine("Backpack: " + backpack.Show());
    }

    // Take item from room
    public bool TakeFromChest(string itemName)
    {
        Item item = CurrentRoom.Chest.Get(itemName);

        if (item == null)
        {
            Console.WriteLine("Item not found.");
            return false;
        }

        if (!backpack.Put(itemName, item))
        {
            Console.WriteLine("Too heavy.");
            CurrentRoom.Chest.Put(itemName, item);
            return false;
        }

        Console.WriteLine("Taken: " + itemName);
        return true;
    }

    // Drop item into room
    public bool DropToChest(string itemName)
    {
        Item item = backpack.Get(itemName);

        if (item == null)
        {
            Console.WriteLine("You don't have that item.");
            return false;
        }

        CurrentRoom.Chest.Put(itemName, item);
        Console.WriteLine("Dropped: " + itemName);
        return true;
    }

    // Use item
    public bool HasItem(string itemName)
    {
        return backpack.Get(itemName) != null;
    }

    public void UseMedkit()
    {
        Item med = backpack.Get("medkit");

        if (med == null)
        {
            Console.WriteLine("You don't have a medkit.");
            return;
        }

        Heal(50);
        Console.WriteLine("Health restored by 50.");
    }
}
