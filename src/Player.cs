namespace Zuul
{
    class Player
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

        // ----- Health -----

        public void Damage(int amount)
        {
            health -= amount;
            if (health < 0)
                health = 0;
        }

        public void Heal(int amount)
        {
            health += amount;
            if (health > 100)
                health = 100;
        }

        public bool IsAlive()
        {
            return health > 0;
        }

        public int Health
        {
            get { return health; }
        }

        // ----- Inventory -----

        public bool TakeFromChest(string itemName)
        {
            Item item = CurrentRoom.Chest.Get(itemName);

            if (item == null)
            {
                System.Console.WriteLine("Item is not in this room.");
                return false;
            }

            if (!backpack.Put(itemName, item))
            {
                CurrentRoom.Chest.Put(itemName, item);
                System.Console.WriteLine("Item doesn't fit in your backpack.");
                return false;
            }

            System.Console.WriteLine($"You picked up the {itemName}.");
            return true;
        }

        public bool DropToChest(string itemName)
        {
            Item item = backpack.Get(itemName);

            if (item == null)
            {
                System.Console.WriteLine("You don't have that item.");
                return false;
            }

            CurrentRoom.Chest.Put(itemName, item);
            System.Console.WriteLine($"You dropped the {itemName}.");
            return true;
        }

        public string BackpackContents()
        {
            return backpack.Show();
        }
    }
}
