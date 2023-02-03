using UnityEngine;

namespace Polyjam2023
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TestCardLogic_1")]
    public class TestCardLogic_1 : CardLogic
    {
        public override string CardName => "Test Card 1";

        public override bool TryPlayCard(GameState gameState)
        {
            gameState.PlayerHand.AddCards(gameState.PlayerDeck.TakeCards(1));
            return true;
        }
    }
}
