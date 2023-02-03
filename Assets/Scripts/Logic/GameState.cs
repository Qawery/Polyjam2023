using System.Collections.Generic;

namespace Polyjam2023
{
    public class GameState
    {
        private List<CardLogic> playerDeck = new (){new CardLogic("Test Card"), new CardLogic("Test Card"), 
                                                    new CardLogic("Test Card"), new CardLogic("Test Card"), 
                                                    new CardLogic("Test Card"), new CardLogic("Test Card"), 
                                                    new CardLogic("Test Card"), new CardLogic("Test Card"), 
                                                    new CardLogic("Test Card"), new CardLogic("Test Card")};
        
        private List<CardLogic> playerHand = new (){new CardLogic("Test Card")};
        
        public IReadOnlyList<CardLogic> PlayerHand => playerHand;

        public int NumberOfCardsInDeck => playerDeck.Count;
    }
}
