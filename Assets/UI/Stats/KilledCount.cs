using TMPro;
using UnityEngine;

namespace UI.Stats
{
    public class KilledCount : MonoBehaviour
    {
        private TextMeshProUGUI countTextMesh;
        private int killedCount = 0;

        private void Awake()
        {
            GlobalEventManager.EnemyDeath.AddListener(UpdateCount);
            countTextMesh = GetComponent<TextMeshProUGUI>();
        }

        private void UpdateCount(Transform enemyTransform)
        {
            killedCount++;
            countTextMesh.text = killedCount.ToString();
        }
    }
}