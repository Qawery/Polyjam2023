using UnityEngine;

namespace Polyjam2023
{
    [CreateAssetMenu(menuName = "ScriptableObjects/UnitCardTemplate")]
    public class UnitCardTemplate : CardTemplate
    {
        public override string EffectDescription => $"Unit";

        public override bool TryPlayCard(GameState gameState)
        {
            gameState.Field.AddUnit(new UnitInstance(this));
            return true;
        }
    }
}