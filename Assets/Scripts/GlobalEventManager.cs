using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent<GameObject> UnitFellEvent = new();
    public static UnityEvent PlayerDeath = new();
    public static UnityEvent<Transform> EnemyDeath = new();
}