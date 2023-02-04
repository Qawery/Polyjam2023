namespace Polyjam2023
{
    public class UnitInstance
    {
        private readonly UnitCardTemplate unitCardTemplate;
        
        public UnitCardTemplate UnitCardTemplate => unitCardTemplate;

        public UnitInstance(UnitCardTemplate unitCardTemplate)
        {
            this.unitCardTemplate = unitCardTemplate;
        }
    }
}
