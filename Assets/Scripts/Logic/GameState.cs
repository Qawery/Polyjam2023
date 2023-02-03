namespace Polyjam2023
{
    public class GameState
    {
        public Deck PlayerDeck { get; private set; } = new();
        public Hand PlayerHand { get; private set; } = new();
    }
}
