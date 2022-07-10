using Unity.VisualScripting;
using UnityEngine;

namespace Units.Weapon
{
    public class SowBall : Weapon
    {
        public int Damage = 10;
        public Object Projectile;
        private void Awake()
        {
            Projectile = Resources.Load("SowBallProjectile");
            Projectile.GetComponent<SowBallProjectile>().Damage = Damage;
            Cooldown = 0.2f;
        }

        protected override void OnAttack()
        {
            var playerTransform = transform;
            var playerTransformPosition = playerTransform.position;
            var shift = playerTransform.forward.normalized * 2;
            shift.y = 0;
            Instantiate(Projectile, playerTransformPosition + shift, playerTransform.rotation);
        }
    }
}