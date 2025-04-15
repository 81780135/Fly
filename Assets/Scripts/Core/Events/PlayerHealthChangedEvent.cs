using System;

public class PlayerHealthChangedEvent : EventArgs
{
    public int NewHealth { get; }

    public PlayerHealthChangedEvent(int newHealth)
    {
        NewHealth = newHealth;
    }
}