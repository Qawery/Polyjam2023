using System.Collections.Generic;

namespace Polyjam2023
{
    public class GameWorld
    {
        private List<CardLogic> playerHand = new ();
        
        public IReadOnlyList<CardLogic> PlayerHand => playerHand;
    }
}
