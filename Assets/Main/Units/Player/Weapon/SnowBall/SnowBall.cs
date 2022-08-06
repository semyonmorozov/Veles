using Unity.VisualScripting;
using UnityEngine;

namespace Main.Units.Player.Weapon.SnowBall
{
    public class SnowBall : SpellBase
    {
        public Object Projectile;
        private PlayerMainStats playerMainStats;
        private SnowBallProjectile snowBallProjectile;
        private Rigidbody playerRigidbody;
        private Rigidbody projectileRigidbody;

        private int Damage => 10 + playerMainStats.Intelligence * 5;
        private float Force => -1f + playerMainStats.WillPower * 20f;
        private float Size => 0.5f + playerMainStats.Intelligence / 8f;
        private float ProjectileMoveSpeed => 200f + playerMainStats.Strength * 30f;
        private float ProjectileLifeTime => 1f + playerMainStats.Endurance / 1.5f;

        protected override float Cooldown => 0;

        protected override float CastTime
        {
            get
            {
                var castTime = 3f - (playerMainStats.Intelligence * 2 + playerMainStats.WillPower) / 30f * 3f;
                return castTime < 0.5f ? 0.5f : castTime;
            }
        }

        private new void Awake()
        {
            base.Awake();
            playerMainStats = GetComponent<PlayerMainStats>();
            playerRigidbody = GetComponent<Rigidbody>();
            Projectile = Resources.Load("Weapon/SnowBall/SnowBallProjectile");
            CastSound = (AudioClip)Resources.Load("Weapon/SnowBall/SnowBallCast");
            projectileRigidbody = Projectile.GetComponent<Rigidbody>();
            snowBallProjectile = Projectile.GetComponent<SnowBallProjectile>();
        }

        protected override void DealSpell()
        {
            snowBallProjectile.Damage = Damage;
            snowBallProjectile.Force = Force;
            snowBallProjectile.Size = Size;
            snowBallProjectile.MoveSpeed = ProjectileMoveSpeed;
            snowBallProjectile.LifeTimeSeconds = ProjectileLifeTime;

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