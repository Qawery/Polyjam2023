using UnityEngine;

namespace Polyjam2023
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TestCardLogic_2")]
    public class TestCardDescription2 : CardDescription
    {
        public override string EffectDescription => "Test card 2 effect description.";

        public override bool TryPlayCard(GameState gameState)
        {
            gameState.PlayerHand.AddCards(gameState.PlayerDeck.TakeCards(2));
            return true;
        }
    }
}