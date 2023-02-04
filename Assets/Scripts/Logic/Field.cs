using System.Collections.Generic;

namespace Polyjam2023
{
    public class Field
    {
        private List<UnitInstance> unitsPresent  = new ();

        public event System.Action OnChanged;
        
        public IReadOnlyList<UnitInstance> UnitsPresent => unitsPresent;

        public void AddUnit(UnitInstance newUnitInstance)
        {
            unitsPresent.Add(newUnitInstance);
            OnChanged?.Invoke();
        }
    }
}