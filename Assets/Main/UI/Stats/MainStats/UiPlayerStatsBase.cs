using Main.Units.Player;
using TMPro;
using UnityEngine;

namespace Main.UI.Stats.MainStats
{
    public abstract class UiPlayerStatsBase : MonoBehaviour
    {
        protected PlayerMainStats PlayerMainStats;
        private TextMeshProUGUI countTextMesh;

        private void Awake()
        {
            PlayerMainStats = FindObjectOfType<PlayerMainStats>();
            countTextMesh = GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            countTextMesh.text = GetStat().ToString();
        }

        protected abstract int GetStat();
    }
}