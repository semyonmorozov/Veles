using UnityEngine;
using UnityEngine.Events;

namespace Main
{
    public class GlobalEventManager
    {
        public static UnityEvent PlayerDeath = new();
        public static UnityEvent<Transform> EnemyDeath = new();
    }
}