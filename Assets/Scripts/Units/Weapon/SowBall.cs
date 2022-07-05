using UnityEngine;

namespace Units.Weapon
{
    public class SowBall : Weapon
    {
        public Object projectile;

        private void Awake()
        {
            projectile = Resources.Load("SowBallProjectile");
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