using UnityEngine;

namespace Polyjam2023
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TestCardLogic_2")]
    public class TestCardLogic_2 : CardLogic
    {
        public override string CardName => "Test Card 2";

        public override bool TryPlayCard(GameState gameState)
        {
            gameState.PlayerHand.AddCards(gameState.PlayerDeck.TakeCards(2));
            return true;
        }
    }
}