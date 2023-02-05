namespace Polyjam2023
{
    public class GameState
    {
        public int playerHandLimit = 10;
        
        public Deck EnemyDeck { get; private set; } = new ();
        public Deck PlayerDeck { get; private set; } = new ();
        public Hand PlayerHand { get; private set; } = new ();

        public Field Field { get; private set; } = new ();
    }
}
