using Units.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Weapon
{
    public class SowBall : SpellBase
    {
        public Object Projectile;
        private PlayerStats playerStats;
        private SnowBallProjectile snowBallProjectile;
        private Rigidbody playerRigidbody;
        private Rigidbody projectileRigidbody;

        private int Damage => 10 + playerStats.Intelligence * 5;
        private float Force => -1f + playerStats.WillPower * 2f;
        private float Size => 0.5f + playerStats.Intelligence / 8f;
        private float ProjectileMoveSpeed => 200f + playerStats.Strength * 30f;
        private float ProjectileLifeTime => 1f + playerStats.Endurance/1.5f;
        private int ProjectilePierce => playerStats.Intelligence / 3;

        protected override float Cooldown
        {
            get
            {
                var cooldown = 2f - playerStats.WillPower / 10f * 3f;
                return cooldown <= 0.5f ? 0.5f : cooldown;
            }
        }

        protected override float CastTime
        {
            get
            {
                var castTime = 3f - (playerStats.Intelligence * 2 + playerStats.WillPower) / 30f * 3f;
                return castTime < 0.5f ? 0.5f : castTime;
            }
        }

        private new void Awake()
        {
            base.Awake();
            playerStats = GetComponent<PlayerStats>();
            playerRigidbody = GetComponent<Rigidbody>();
            Projectile = Resources.Load("SnowBallProjectile");
            projectileRigidbody = Projectile.GetComponent<Rigidbody>();
            snowBallProjectile = Projectile.GetComponent<SnowBallProjectile>();
        }

        protected override void OnAttack()
        {
            snowBallProjectile.Damage = Damage;
            snowBallProjectile.Force = Force;
            snowBallProjectile.Size = Size;
            snowBallProjectile.MoveSpeed = ProjectileMoveSpeed;
            snowBallProjectile.LifeTimeSeconds = ProjectileLifeTime;
            snowBallProjectile.Pierce = ProjectilePierce;

            projectileRigidbody.velocity = playerRigidbody.velocity;
            
            var playerTransform = transform;
            var playerTransformRotation = playerTransform.rotation;
            var playerTransformPosition = playerTransform.position;
            var shift = playerTransform.forward.normalized;
            shift.y = 1.5f;

            Instantiate(Projectile, playerTransformPosition + shift, playerTransformRotation);
        }
    }
}