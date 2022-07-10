using UnityEngine;
using UnityEngine.AI;

namespace World
{
    public class NavMeshBaker : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }
}