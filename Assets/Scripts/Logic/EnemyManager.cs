using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Polyjam2023
{
    public class EnemyManager
    {
        private const int ReinforcementsTime = 2;
        public const string RootOfEvilName = "Root of Evil";
        
        private readonly (int elapsedCards, int minReinforcements, int maxReinforcements)[] deckPhases =
        {
            (3, 1, 2),
            (5, 2, 4),
            (10, 2, 5),
            (10, 3, 5),
            (10, 4, 6)
        };
        
        private CardLibrary cardLibrary;
        private int totalCards;
        
        public System.Action OnReinforcementsTimerChanged;
        public System.Action OnBossKilled;

        public bool BossSpawned { get; private set; }= false;
        public int ReinforcementsTimer { get; private set; } = ReinforcementsTime;
        
        public void InitializeEnemy(GameState gameState, CardLibrary cardLibrary)
        {
            this.cardLibrary = cardLibrary;
            
            var partialDeck = new List<string>
            {
                RootOfEvilName, "Hulk", "Hulk", "Hulk", "Hulk",
                "Nightmare", "Nightmare", "Nightmare", "Nightmare", "Boar"
            };
            Deck.ShuffleCardList(ref partialDeck);
            gameState.EnemyDeck.AddCards(partialDeck);
            
            partialDeck = new List<string>
            {
                "Hulk", "Hulk", "Hulk", "Behemoth", "Boar",
                "Nightmare", "Nightmare", "Nightmare", "Boar", "Boar"
            };
            Deck.ShuffleCardList(ref partialDeck);
            gameState.EnemyDeck.AddCards(partialDeck);
            
            partialDeck = new List<string>
            {
                "Boar", "Boar", "Hulk", "Hulk", "Hulk",
                "Boar", "Boar", "Boar", "Nightmare", "Nightmare"
            };
            Deck.ShuffleCardList(ref partialDeck);
            gameState.EnemyDeck.AddCards(partialDeck);
            
            partialDeck = new List<string>
            {
                "Boar", "Boar", "Spore Carrier", "Spore Carrier", "Infector"
            };
            Deck.ShuffleCardList(ref partialDeck);
            gameState.EnemyDeck.AddCards(partialDeck);
            
            partialDeck = new List<string>
            {
                "Spore Carrier", "Boar", "Spore Carrier"
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
            if (BossSpawned && gameState.Field.EnemyUnitsPresent.All(unitPresent => unitPresent.UnitCardTemplate.CardName != RootOfEvilName))
            {
                OnBossKilled?.Invoke();
                return;
            }
            
            if (!gameState.Field.EnemyUnitsPresent.Any() || ReinforcementsTimer == 0 || BossSpawned)
            {
                List<string> cardsToTake = null;
                int cardsElapsed = totalCards - gameState.EnemyDeck.NumberOfCardsInDeck;
                foreach (var deckPhaseInfo in deckPhases)
                {
                    if (cardsElapsed < deckPhaseInfo.elapsedCards)
                    {
                        cardsToTake = gameState.EnemyDeck.TakeCards(Random.Range(deckPhaseInfo.minReinforcements, deckPhaseInfo.maxReinforcements));
                        break;
                    }
                     
                    cardsElapsed -= deckPhaseInfo.elapsedCards;
                }
                if (cardsToTake == null)
                {
                    cardsToTake = gameState.EnemyDeck.TakeCards(4);
                }

                for(int i = 0; i < cardsToTake.Count; ++i)
                {
                    gameState.Field.AddUnit(new UnitInstance(cardLibrary.GetCardTemplate(cardsToTake[i]) as UnitCardTemplate));
                    if (cardsToTake[i] == RootOfEvilName)
                    {
                        cardsToTake.AddRange(gameState.EnemyDeck.TakeCards(2));
                        BossSpawned = true;
                        gameState.Field.WrathOfTheForestEnabled = false;
                    }
                }
                ReinforcementsTimer = ReinforcementsTime;
                OnReinforcementsTimerChanged?.Invoke();
            }
            else
            {
                --ReinforcementsTimer;
                OnReinforcementsTimerChanged?.Invoke();
            }
        }
    }
}
