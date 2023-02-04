using UnityEngine;

namespace Polyjam2023
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TestCardLogic_1")]
    public class TestCardDescription1 : CardDescription
    {
        public override string EffectDescription => "Test card 1 effect description.";

        public override bool TryPlayCard(GameState gameState)
        {
            gameState.PlayerHand.AddCards(gameState.PlayerDeck.TakeCards(1));
            return true;
        }
    }
}
