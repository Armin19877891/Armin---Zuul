using System.Collections.Generic;
using System.Text;

// Stores items with a maximum allowed weight
public class Inventory
{
    private int maxWeight;
    private Dictionary<string, Item> items;

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        items = new Dictionary<string, Item>();
    }

    // Add item if weight allows
    public bool Put(string itemName, Item item)
    {
        if (FreeWeight() >= item.Weight)
        {
            items[itemName] = item;
            return true;
        }
        return false;
    }

    // Remove and return item
    public Item Get(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            Item item = items[itemName];
            items.Remove(itemName);
            return item;
        }
        return null;
    }

    // Total weight of all items
    public int TotalWeight()
    {
        int total = 0;
        foreach (Item item in items.Values)
            total += item.Weight;

        return total;
    }

    // Remaining weight capacity
    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    // Show item names
    public string Show()
    {
        if (items.Count == 0)
            return "Nothing";

        StringBuilder sb = new StringBuilder();
        foreach (var pair in items)
            sb.Append(pair.Key + " ");

        return sb.ToString();
    }
}
