using Units.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Weapon
{
    public class SowBallWeaponBase : WeaponBase
    {
        public Object Projectile;
        private PlayerStats playerStats;
        private SnowBallProjectile snowBallProjectile;

        private int Damage => 2 + playerStats.Intelligence * 5;
        private float Force => -1 + playerStats.WillPower * 2;
        private float Size => 0.5f + playerStats.Intelligence / 8f;
        protected override float Cooldown => 3f - playerStats.WillPower / 10f * 3f;

        private void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
            Projectile = Resources.Load("SnowBallProjectile");
            snowBallProjectile = Projectile.GetComponent<SnowBallProjectile>();
        }

        protected override void OnAttack()
        {
            snowBallProjectile.Damage = Damage;
            snowBallProjectile.Force = Force;
            snowBallProjectile.Size = Size;

            var playerTransform = transform;
            var playerTransformRotation = playerTransform.rotation;
            var playerTransformPosition = playerTransform.position;
            var shift = playerTransform.forward.normalized;
            shift.y = 0;
            Instantiate(Projectile, playerTransformPosition + shift, playerTransformRotation);
        }
    }
}