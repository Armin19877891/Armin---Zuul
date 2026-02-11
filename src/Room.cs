using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    class Room
    {
        private string description;
        private Dictionary<string, Room> exits;
        private HashSet<string> lockedExits;
        private Inventory chest;

        public Room(string description)
        {
            this.description = description;
            exits = new Dictionary<string, Room>();
            lockedExits = new HashSet<string>();
            chest = new Inventory(999999);
        }

        public Inventory Chest => chest;

        public void SetExit(string direction, Room neighbor, bool locked = false)
        {
            exits[direction] = neighbor;
            if (locked)
                lockedExits.Add(direction);
        }

        public Room GetExit(string direction)
        {
            if (lockedExits.Contains(direction))
                return null;

            exits.TryGetValue(direction, out Room room);
            return room;
        }

        public bool UnlockExit(string direction)
        {
            return lockedExits.Remove(direction);
        }

        public bool HasExit(string direction)
        {
            return exits.ContainsKey(direction);
        }

        public bool IsExitLocked(string direction)
        {
            return lockedExits.Contains(direction);
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetExitString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string direction in exits.Keys)
            {
                if (lockedExits.Contains(direction))
                    sb.Append($"{direction} (locked) ");
                else
                    sb.Append($"{direction} ");
            }

            return sb.ToString().Trim();
        }
    }
}
