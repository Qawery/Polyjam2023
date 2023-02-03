using System.Collections.Generic;

namespace Polyjam2023
{
    public class GameState
    {
        private Deck playerDeck = new ();
        
        private List<CardLogic> playerHand = new (){new CardLogic("Test Card 3")};

        public Deck PlayerDeck => playerDeck;
        public IReadOnlyList<CardLogic> PlayerHand => playerHand;
    }
}
