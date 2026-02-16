using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private Dictionary<string, Item> items;
    private int maxWeight;

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        items = new Dictionary<string, Item>();
    }

    public bool Put(string name, Item item)
    {
        if (CurrentWeight() + item.Weight > maxWeight)
            return false;

        items[name] = item;
        return true;
    }

    public Item Take(string name)
    {
        if (!items.ContainsKey(name))
            return null;

        Item item = items[name];
        items.Remove(name);
        return item;
    }

    public bool Contains(string name)
    {
        return items.ContainsKey(name);
    }

    public int CurrentWeight()
    {
        return items.Values.Sum(i => i.Weight);
    }

    public string Show()
    {
        if (items.Count == 0)
            return "none";

        return string.Join(", ", items.Keys);
    }
}
