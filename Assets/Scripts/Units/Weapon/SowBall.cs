using Unity.VisualScripting;
using UnityEngine;

namespace Units.Weapon
{
    public class SowBall : Weapon
    {
        public int damage = 10;
        public Object projectile;
        private void Awake()
        {
            projectile = Resources.Load("SowBallProjectile");
            projectile.GetComponent<SowBallProjectile>().damage = damage;
            cooldown = 0.2f;
        }

        protected override void OnAttack()
        {
            var playerTransform = transform;
            var playerTransformPosition = playerTransform.position;
            var shift = playerTransform.forward.normalized * 2;
            shift.y = 0;
            Instantiate(projectile, playerTransformPosition + shift, playerTransform.rotation);
        }
    }
}