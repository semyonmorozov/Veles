using Main.Units.Player;
using TMPro;
using UnityEngine;

namespace Main.UI.Stats.MainStats
{
    public abstract class UiPlayerStatsBase : MonoBehaviour
    {
        protected PlayerStats PlayerStats;
        private TextMeshProUGUI countTextMesh;

        private void Awake()
        {
            PlayerStats = FindObjectOfType<PlayerStats>();
            countTextMesh = GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            countTextMesh.text = GetStat().ToString();
        }

        protected abstract int GetStat();
    }
}