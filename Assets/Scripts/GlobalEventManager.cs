using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent PlayerFellEvent = new();
    public static UnityEvent PlayerDeath = new();
}
