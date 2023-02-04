using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class GameplayManager : MonoBehaviour
    {
        private CardLibrary cardLibrary;
        private const int StartingCards = 5;
        private const int DrawCardsPerTurn = 2;
        
        public event System.Action<GameEndReason> OnGameEnded;
        
        public GameState GameState { get; private set; } = new ();

        private void Awake()
        {
            cardLibrary = FindObjectOfType<DependencyResolver>().CardLibrary;
            
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            GameState.PlayerDeck.AddCards(new List<string>
            {
                "Test Card 1", "Fast Player Unit", "Slow Player Unit",
                "Test Card 2", "Fast Player Unit", "Slow Player Unit",
                "Test Card 3", "Fast Player Unit", "Slow Player Unit",
                "Test Card 1", "Fast Player Unit", "Slow Player Unit",
                "Test Card 2", "Fast Player Unit", "Slow Player Unit",
                "Test Card 3", "Fast Player Unit", "Slow Player Unit"
            });
            GameState.PlayerDeck.Shuffle();
            
            GameState.PlayerHand.AddCards(GameState.PlayerDeck.TakeCards(StartingCards));
            GameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate("Enemy Unit") as UnitCardTemplate));
        }

        private void OnDestroy()
        {
            cardLibrary = null;
        }

        public void PlayPlayerCard(string cardName)
        {
            if (cardLibrary.GetCardTemplate(cardName).TryPlayCard(GameState))
            {
                GameState.PlayerHand.RemoveCard(cardName);
            }
        }

        public void EndPlayerTurn()
        {
            GameState.Field.ResolveFieldCombat();
            
            //Enemy turn.
            GameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate("Enemy Unit") as UnitCardTemplate));
            
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
