using Units.Player.Weapon.FireBall;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Player.Weapon.SnowBall
{
    public class FireBall : SpellBase
    {
        public Object Projectile;
        private int Damage => 10 + playerStats.Intelligence * 2;
        private float Size => 0.5f + playerStats.Intelligence / 10f;
        private float ProjectileMoveSpeed => 400f + playerStats.Intelligence * 30f;
        private float ProjectileLifeTime => 1f + playerStats.WillPower / 3f;
        protected override float Cooldown => 3f - playerStats.WillPower / 4f;
        protected override float CastTime => 0;
        
        private PlayerStats playerStats;
        private Rigidbody playerRigidbody;
        private Rigidbody projectileRigidbody;
        private FireBallProjectile fireBallProjectile;

        private new void Awake()
        {
            base.Awake();
            playerStats = GetComponent<PlayerStats>();
            playerRigidbody = GetComponent<Rigidbody>();
            Projectile = Resources.Load("Weapon/FireBall/FireBallProjectile");
            projectileRigidbody = Projectile.GetComponent<Rigidbody>();
            fireBallProjectile = Projectile.GetComponent<FireBallProjectile>();
        }

        protected override void DealSpell()
        {
            fireBallProjectile.Damage = Damage;
            fireBallProjectile.Size = Size;
            fireBallProjectile.MoveSpeed = ProjectileMoveSpeed;
            fireBallProjectile.LifeTimeSeconds = ProjectileLifeTime;

            //projectileRigidbody.velocity = playerRigidbody.velocity;

            var playerTransform = transform;
            var playerTransformRotation = playerTransform.rotation;
            var playerTransformPosition = playerTransform.position;
            var shift = playerTransform.forward.normalized;
            shift.y = 1.5f;

            Instantiate(Projectile, playerTransformPosition + shift, playerTransformRotation);
        }
    }
}