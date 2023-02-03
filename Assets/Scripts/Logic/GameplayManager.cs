using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private CardLibrary cardLibrary;
        private float delay = 2.0f;
        
        public GameState GameState { get; private set; } = new ();

        private void Awake()
        {
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            GameState.PlayerDeck.AddCards(new List<string>
            {
                "Test Card 1", "Test Card 1", "Test Card 1", "Test Card 1",
                "Test Card 2", "Test Card 2", "Test Card 2", "Test Card 2"
            });
            GameState.PlayerDeck.Shuffle();
            GameState.PlayerDeck.AddCards(new List<string>
            {
                "Test Card 3"
            });
        }

        private void Start()
        {
            GameState.PlayerHand.AddCard("Test Card 3");
        }

        public void PlayPlayerCard(string cardName)
        {
            if (cardLibrary.GetCardLogic(cardName).TryPlayCard(GameState))
            {
                GameState.PlayerHand.RemoveCard(cardName);
            }
        }
    }
}
