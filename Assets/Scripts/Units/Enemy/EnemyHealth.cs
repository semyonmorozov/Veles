namespace Units.Enemy
{
    public class EnemyHealth : Health
    {
        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}