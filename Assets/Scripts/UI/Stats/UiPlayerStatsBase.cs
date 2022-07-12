using TMPro;
using Units.Player;
using UnityEngine;

namespace UI.Stats
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