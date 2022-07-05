namespace Units.Player
{
    public class PlayerHealth : Health
    {
        protected override void OnDeath()
        {
            GlobalEventManager.PlayerDeath.Invoke();
        }
    }
}