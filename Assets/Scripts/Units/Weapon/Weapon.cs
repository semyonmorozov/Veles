using System.Collections;
using UnityEngine;

namespace Units.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        public float cooldown = 1;
        
        private bool isReloaded = true;

        public void Attack()
        {
            if (!isReloaded)
                return;

            OnAttack();

            StartCoroutine(StartCooldown());
        }

        protected abstract void OnAttack();

        private IEnumerator StartCooldown()
        {
            isReloaded = false;
            yield return new WaitForSeconds(cooldown);
            isReloaded = true;
        }
    }
}