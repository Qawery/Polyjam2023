using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class GameplayManager : MonoBehaviour
    {
        private const int StartingCards = 5;
        private const int DrawCardsPerTurn = 2;
        
        [SerializeField] private CardLibrary cardLibrary;

        public event System.Action<GameEndReason> OnGameEnded;
        
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
        }

        private void Start()
        {
            GameState.PlayerHand.AddCards(GameState.PlayerDeck.TakeCards(StartingCards));
        }

        public void PlayPlayerCard(string cardName)
        {
            if (cardLibrary.GetCardDescription(cardName).TryPlayCard(GameState))
            {
                GameState.PlayerHand.RemoveCard(cardName);
            }
        }

        public void EndPlayerTurn()
        {
            //Enemy turn.
            //TODO:
            
            //Victory conditions.
            if (GameState.PlayerDeck.NumberOfCardsInDeck > 0)
            {
                GameState.PlayerHand.AddCards(GameState.PlayerDeck.TakeCards(DrawCardsPerTurn));
            }
            else
            {
                OnGameEnded?.Invoke(GameEndReason.DeckEnded);
            }
        }
    }
}
