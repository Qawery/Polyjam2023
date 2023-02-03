using UnityEngine;

namespace Polyjam2023
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TestCardLogic_3")]
    public class TestCardLogic_3 : CardLogic
    {
        public override string CardName => "Test Card 3";

        public override bool TryPlayCard(GameState gameState)
        {
            gameState.PlayerHand.AddCards(gameState.PlayerDeck.TakeCards(3));
            return true;
        }
    }
}