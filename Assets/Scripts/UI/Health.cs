using TMPro;
using Units.Player;
using UnityEngine;

namespace UI
{
    public class Health : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        private TextMeshProUGUI textMesh;

        private void Awake()
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            textMesh.text = $"Health {playerHealth.health.ToString()}";
        }
    }
}
