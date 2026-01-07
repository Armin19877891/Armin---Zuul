public class Player
{
    // fields
    private int health;

    // auto property
    public Room CurrentRoom { get; set; }

    // property for status output
    public int Health
    {
        get { return health; }
    }

    // constructor
    public Player()
    {
        health = 100;
        CurrentRoom = null;
    }

    // methods
    public void Damage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > 100)
        {
            health = 100;
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
