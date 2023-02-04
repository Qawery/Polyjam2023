using System.Collections.Generic;

namespace Polyjam2023
{
    public class Field
    {
        private List<UnitInstance> enemyUnitsPresent  = new ();
        private List<UnitInstance> playerUnitsPresent  = new ();

        public event System.Action OnEnemyUnitsChanged;
        public event System.Action OnPlayerUnitsChanged;
        
        public IReadOnlyList<UnitInstance> EnemyUnitsPresent => enemyUnitsPresent;
        public IReadOnlyList<UnitInstance> PlayerUnitsPresent => playerUnitsPresent;

        public void AddEnemyUnit(UnitInstance newUnitInstance)
        {
            playerUnitsPresent.Add(newUnitInstance);
            OnEnemyUnitsChanged?.Invoke();
        }
        
        public void AddPlayerUnit(UnitInstance newUnitInstance)
        {
            playerUnitsPresent.Add(newUnitInstance);
            OnPlayerUnitsChanged?.Invoke();
        }
    }
}