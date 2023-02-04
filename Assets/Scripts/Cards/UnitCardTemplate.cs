using UnityEngine;

namespace Polyjam2023
{
    [CreateAssetMenu(menuName = "ScriptableObjects/UnitCardTemplate")]
    public class UnitCardTemplate : CardTemplate
    {
        [SerializeField] private Ownership ownership;
        [SerializeField, Range(0, 10)] private int attack;
        [SerializeField, Range(1, 10)] private int initiative;
        [SerializeField, Range(1, 10)] private int health;
        
        public override string EffectDescription => $"Unit";
        public Ownership Ownership => ownership;
        public int Attack => attack;
        public int Initiative => initiative;
        public int Health => health;

        public override bool TryPlayCard(GameState gameState)
        {
            gameState.Field.AddUnit(new UnitInstance(this));
            return true;
        }
    }
}