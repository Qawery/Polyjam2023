using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class GameplayManager : MonoBehaviour
    {
        private CardLibrary cardLibrary;
        private GameSettings gameSettings;

        public event System.Action<GameEndReason> OnGameEnded;
        public event System.Action OnPlayerTurnEnded;
        public event System.Action OnPlayerTurnStarted;
        
        public EnemyManager EnemyManager { get; private set; } = new ();
        public GameState GameState { get; private set; } = new ();

        private void Awake()
        {
            var dependencyResolver = FindObjectOfType<DependencyResolver>();
            cardLibrary = dependencyResolver.CardLibrary;
            gameSettings = dependencyResolver.GameSettings;
            
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            Assert.IsNotNull(gameSettings, $"Missing {nameof(gameSettings)} on {gameObject.name}.");
            
            //Hard difficulty (base)
            GameState.PlayerDeck.AddCards(new List<string>
            {
                "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                "Rifleman", "Rifleman", "Flame Soldier", "Scout"
            });
            
            //Medium difficulty
            if (gameSettings.difficulty < Difficulty.Hard)
            {
                GameState.playerHandLimit += 3;
                GameState.PlayerDeck.AddCards(new List<string>
                {
                    "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                    "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                    "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                    "Rifleman", "Rifleman", "Flame Soldier", "Scout"
                });
            }
            
            //Easy difficulty
            if (gameSettings.difficulty < Difficulty.Medium)
            {
                GameState.playerHandLimit += 2;
                GameState.PlayerDeck.AddCards(new List<string>
                {
                    "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                    "Rifleman", "Rifleman", "Flame Soldier", "Scout",
                    "Rifleman", "Rifleman", "Flame Soldier", "Scout"
                });
            }
            
            GameState.PlayerDeck.Shuffle();
            GameState.PlayerHand.AddCards(GameState.PlayerDeck.TakeCards(GameState.playerHandLimit));

            EnemyManager.InitializeEnemy(GameState, cardLibrary);
            EnemyManager.OnBossKilled += () => { OnGameEnded?.Invoke(GameEndReason.Victory); };
        }

        private void OnDestroy()
        {
            cardLibrary = null;
            gameSettings = null;
            EnemyManager = null;
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
            OnPlayerTurnEnded?.Invoke();
            
            GameState.Field.ResolveFieldCombat();
            
            EnemyManager.EnemyTurn(GameState);
            
            //Defeat conditions.
            if (GameState.PlayerHand.Cards.Sum(card => card.quantity) == 0 && 
                GameState.PlayerDeck.NumberOfCardsInDeck == 0 && 
                GameState.Field.PlayerUnitsPresent.Count == 0)
            {
                OnGameEnded?.Invoke(GameEndReason.DeckEnded);
                return;
            }
            
            GameState.PlayerHand.AddCards(GameState.PlayerDeck.TakeCards(GameState.playerHandLimit - GameState.PlayerHand.Cards.Sum(cardEntry => cardEntry.quantity)));
            OnPlayerTurnStarted?.Invoke();
        }
    }
}
