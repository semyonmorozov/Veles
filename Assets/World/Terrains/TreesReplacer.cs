using System;
using UnityEngine;

namespace World.Terrains
{
    public class TreesReplacer : MonoBehaviour
    {
        private TreeInstance[] originalTreeInstances;

        private TerrainData terrainData;
        //Заменяем деревья на терейне на реальные объекты, чтобы на них нормально работали оверлапы, например в ClearSight

        private void Awake()
        {
            terrainData = GetComponent<Terrain>().terrainData;
            originalTreeInstances = (TreeInstance[])terrainData.treeInstances.Clone();
            
            var allTrees = new GameObject("Trees");
            allTrees.transform.SetParent(transform);
            
            var trees = terrainData.treePrototypes;

            var width = terrainData.size.x;
            var height = terrainData.size.z;
            var y = terrainData.size.y;
            foreach (var tree in terrainData.treeInstances)
            {
                var position = new Vector3(tree.position.x * width, tree.position.y * y, tree.position.z * height);
                var prefab = trees[tree.prototypeIndex].prefab;
                var scale = new Vector3(tree.widthScale, tree.heightScale, tree.widthScale);
                prefab.transform.localScale = scale;
                var rotation = Quaternion.Euler(0f,Mathf.Rad2Deg*tree.rotation,0f);
                Instantiate(prefab, position, rotation, allTrees.transform);
            }

            terrainData.treeInstances = Array.Empty<TreeInstance>(); //Опасная херня, если не вернуть на место, то сотрет дервеья с терейна насовсем, даже после остановки приложения их не будет
        }

        private void OnDestroy()
        {
            terrainData.SetTreeInstances(originalTreeInstances, false);
        }
    }
}