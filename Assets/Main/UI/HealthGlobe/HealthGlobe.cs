using System;
using Main.Units.Player;
using UnityEngine;

namespace Main.UI.HealthGlobe
{
    public class HealthGlobe : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        private Vector3 startScale;
        private float angle;

        private void Awake()
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            startScale = transform.localScale;
        }

        private void FixedUpdate()
        {
            transform.localScale = new Vector3(
                ReduceByCurrentHealth(startScale.x),
                ReduceByCurrentHealth(startScale.y),
                startScale.z
            );
        }

        private float ReduceByCurrentHealth(float diameter)
        {
            //Уменьшаем площадь сферы пропорционально текущему здоровью
            return (float)Math.Sqrt(Math.Pow(diameter/2,2) * GetPlayerCurrentHealthPercent())*2;
        }

        private float GetPlayerCurrentHealthPercent()
        {
            return playerHealth.CurrentHealth / ((float)playerHealth.MaxHealth / 100) / 100;
        }
    }
}