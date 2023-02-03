using UnityEngine;

namespace Polyjam2023
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TestCardLogic")]
    public class TestCardLogic : CardLogic
    {
        public override string CardName => "";

        public override void PlayCard(GameState gameState)
        {
            gameState.PlayerHand.AddCards(gameState.PlayerDeck.TakeCards(1));
        }
    }
}
