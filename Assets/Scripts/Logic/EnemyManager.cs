using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Polyjam2023
{
    public class EnemyManager
    {
        private CardLibrary cardLibrary;
        private const int ReinforcementsTime = 2;
        private int reinforcementsTimer = ReinforcementsTime;
        private int totalCards;
        private bool bossSpawned = false;
        
        public const string RootOfEvilName = "Root of Evil";
        
        public System.Action OnBossKilled;
        
        public void InitializeEnemy(GameState gameState, CardLibrary cardLibrary)
        {
            this.cardLibrary = cardLibrary;
            
            var partialDeck = new List<string>
            {
                RootOfEvilName, "Hulk", "Hulk", "Hulk", "Hulk",
                "Nightmare", "Nightmare", "Nightmare", "Nightmare", "Nightmare"
            };
            Deck.ShuffleCardList(ref partialDeck);
            gameState.EnemyDeck.AddCards(partialDeck);
            
            partialDeck = new List<string>
            {
                "Hulk", "Hulk", "Hulk", "Boar", "Boar",
                "Nightmare", "Nightmare", "Nightmare", "Boar", "Boar"
            };
            Deck.ShuffleCardList(ref partialDeck);
            gameState.EnemyDeck.AddCards(partialDeck);
            
            partialDeck = new List<string>
            {
                "Boar", "Boar", "Boar", "Boar", "Boar",
                "Boar", "Boar", "Boar", "Boar", "Boar"
            };
            Deck.ShuffleCardList(ref partialDeck);
            gameState.EnemyDeck.AddCards(partialDeck);

            totalCards = gameState.EnemyDeck.NumberOfCardsInDeck;
            
            foreach(var card in gameState.EnemyDeck.TakeCards(Random.Range(1, 3)))
            {
                gameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate(card) as UnitCardTemplate));
            }
        }
        
        public void EnemyTurn(GameState gameState)
        {
            if (bossSpawned && !gameState.Field.EnemyUnitsPresent.Any(unitPresent => unitPresent.UnitCardTemplate.CardName == RootOfEvilName))
            {
                OnBossKilled?.Invoke();
                return;
            }
            
            if (!gameState.Field.EnemyUnitsPresent.Any() || reinforcementsTimer == 0)
            {
                List<string> cardsToTake;
                if (gameState.EnemyDeck.NumberOfCardsInDeck > totalCards * 2 / 3)
                {
                    cardsToTake = gameState.EnemyDeck.TakeCards(Random.Range(1, 3));
                }
                else if (gameState.EnemyDeck.NumberOfCardsInDeck > totalCards / 3)
                {
                    
                    cardsToTake = gameState.EnemyDeck.TakeCards(Random.Range(2, 4));
                }
                else
                {
                    cardsToTake = gameState.EnemyDeck.TakeCards(Random.Range(2, 5));
                }
                
                for(int i = 0; i < cardsToTake.Count; ++i)
                {
                    gameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate(cardsToTake[i]) as UnitCardTemplate));
                    if (cardsToTake[i] == RootOfEvilName)
                    {
                        cardsToTake.AddRange(gameState.EnemyDeck.TakeCards(2));
                        bossSpawned = true;
                    }
                }
                reinforcementsTimer = ReinforcementsTime;
            }
            else
            {
                --reinforcementsTimer;
            }
        }
    }
}
