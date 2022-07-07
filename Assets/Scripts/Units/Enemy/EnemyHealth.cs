namespace Units.Enemy
{
    public class EnemyHealth : Health
    {
        protected override void OnDeath()
        {
            GlobalEventManager.EnemyDeath.Invoke();
            Destroy(gameObject);
        }
    }
}