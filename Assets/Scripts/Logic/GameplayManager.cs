using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class GameplayManager : MonoBehaviour
    {
        private const int StartingCards = 2;
        private const int DrawCardsPerTurn = 2;
        
        [SerializeField] private CardLibrary cardLibrary;

        public event System.Action<GameEndReason> OnGameEnded;
        
        public GameState GameState { get; private set; } = new ();

        private void Awake()
        {
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            GameState.PlayerDeck.AddCards(new List<string>
            {
                "Fast Player Unit", "Slow Player Unit",
                "Fast Player Unit", "Slow Player Unit",
                "Fast Player Unit", "Slow Player Unit"
            });
            //GameState.PlayerDeck.Shuffle();
        }

        private void Start()
        {
            GameState.PlayerHand.AddCards(GameState.PlayerDeck.TakeCards(StartingCards));
            GameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate("Enemy Unit") as UnitCardTemplate));
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
            //TODO
            
            //Victory conditions.
            if (GameState.PlayerDeck.NumberOfCardsInDeck > 0)
            {
                //GameState.PlayerHand.AddCards(GameState.PlayerDeck.TakeCards(DrawCardsPerTurn));
            }
            else
            {
                OnGameEnded?.Invoke(GameEndReason.DeckEnded);
            }
        }
    }
}
