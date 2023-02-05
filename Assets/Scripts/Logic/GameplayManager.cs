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
        private bool bossKilled = false;

        public event System.Action<GameEndReason> OnGameEnded;
        
        public GameState GameState { get; private set; } = new ();

        private void Awake()
        {
            cardLibrary = FindObjectOfType<DependencyResolver>().CardLibrary;
            
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            GameState.PlayerDeck.AddCards(new List<string>
            {
                "Riflemen", "Flame Soldier", "Scout",
                "Riflemen", "Flame Soldier", "Scout",
                "Riflemen", "Flame Soldier", "Scout"
            });
            GameState.PlayerDeck.Shuffle();
            
            GameState.PlayerHand.AddCards(GameState.PlayerDeck.TakeCards(StartingCards));
            GameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate("Boar") as UnitCardTemplate));
            GameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate("Root of Evil") as UnitCardTemplate));
            GameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate("Boar") as UnitCardTemplate));
            GameState.Field.OnUnitKilled += (unitInstance) =>
            {
                if (unitInstance.UnitCardTemplate.CardName == "Root of Evil")
                {
                    bossKilled = true;
                }
            };
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

            if (bossKilled)
            {
                OnGameEnded?.Invoke(GameEndReason.Victory);
                return;
            }
            
            //Enemy turn.
            int roll = Random.Range(0, 6);
            switch (roll)
            {
                case 0:
                {
                    GameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate("Boar") as UnitCardTemplate));
                    break;
                }
                case 1:
                case 2:
                {
                    GameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate("Hulk") as UnitCardTemplate));
                    break;
                }
                case 3:
                case 4:
                {
                    GameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate("Nightmare") as UnitCardTemplate));
                    break;
                }
            }
            
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
