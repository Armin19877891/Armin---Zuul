using System;

public class Player
{
    public Room CurrentRoom { get; set; }

    private int health;
    private Inventory backpack;

    public Player()
    {
        health = 100;
        backpack = new Inventory(15);
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void Damage(int amount, string cause)
    {
        health -= amount;
        Console.WriteLine($"You lost {amount} health due to {cause}.");
        Console.WriteLine($"Health: {health}");
    }

    public void Heal(int amount)
    {
        health += amount;
        Console.WriteLine($"You healed for {amount}. Current Health: {health}");
    }

    public void ShowStatus()
    {
        Console.WriteLine($"Health: {health}");
        Console.WriteLine($"Backpack ({backpack.CurrentWeight()}/15): {backpack.Show()}");
    }

    public void TakeFromRoom(string name)
    {
        Item item = CurrentRoom.Chest.Take(name);

        if (item == null)
        {
            Console.WriteLine("No such item here.");
            return;
        }

        if (!backpack.Put(name, item))
        {
            Console.WriteLine("You cant carry anymore stuff.");
            CurrentRoom.Chest.Put(name, item);
            return;
        }

        Console.WriteLine($"{name} taken.");
    }

    public void DropToRoom(string name)
    {
        Item item = backpack.Take(name);

        if (item == null)
        {
            Console.WriteLine("You don't have that.");
            return;
        }

        CurrentRoom.Chest.Put(name, item);
        Console.WriteLine($"{name} dropped, it floats in the air.");
    }

    public bool HasItem(string name)
    {
        return backpack.Contains(name);
    }

    public void RemoveItem(string name)
    {
        backpack.Take(name);
    }
}
