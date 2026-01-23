using System.Collections.Generic;

namespace Zuul
{
    class Room
    {
        private string description;
        private Dictionary<string, Room> exits;
        private Inventory chest;

        public Room(string description)
        {
            this.description = description;
            exits = new Dictionary<string, Room>();
            chest = new Inventory(999999);
        }

        public Inventory Chest
        {
            get { return chest; }
        }

        public void SetExit(string direction, Room neighbor)
        {
            exits[direction] = neighbor;
        }

        public Room GetExit(string direction)
        {
            exits.TryGetValue(direction, out Room room);
            return room;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetExitString()
        {
            return string.Join(" ", exits.Keys);
        }
    }
}
