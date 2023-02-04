namespace Polyjam2023
{
    public class UnitInstance
    {
        private readonly UnitCardTemplate unitCardTemplate;

        public int currentAttack;
        public int currentInitiative;
        public int currentHealth;
        
        public UnitCardTemplate UnitCardTemplate => unitCardTemplate;

        public UnitInstance(UnitCardTemplate unitCardTemplate)
        {
            this.unitCardTemplate = unitCardTemplate;
            currentAttack = unitCardTemplate.Attack;
            currentInitiative = unitCardTemplate.Initiative;
            currentHealth = unitCardTemplate.Health;
        }
    }
}
