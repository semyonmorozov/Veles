using System;
using Units.Player;
using UnityEngine;

namespace UI
{
    public class HealthGlobe : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        private Vector3 startScale;
        private float _angle;
        private Vector3 _centre;
        public float RotateSpeed = 0.5f;

        private void Awake()
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            startScale = transform.localScale;
            _centre = transform.localPosition;
        }

        private void FixedUpdate()
        {
            transform.localScale = new Vector3(
                ReduceByCurrentHealth(startScale.x),
                ReduceByCurrentHealth(startScale.y),
                startScale.z
            );
            
            _angle += RotateSpeed * Time.fixedDeltaTime;
 
            var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * ((1f-GetPlayerCurrentHealthPercent())*30f);
            transform.localPosition = _centre + offset;
        }

        private float ReduceByCurrentHealth(float value)
        {
            //Уменьшаем площадь сферы пропорционально текущему здоровью, при условии, что параметры скейла можно считать за диаметр сферы
            return (float)Math.Sqrt(Math.Pow(value/2,2) * GetPlayerCurrentHealthPercent())*2;
        }

        private float GetPlayerCurrentHealthPercent()
        {
            return playerHealth.currentHealth / ((float)playerHealth.maxHealth / 100) / 100;
        }
    }
}