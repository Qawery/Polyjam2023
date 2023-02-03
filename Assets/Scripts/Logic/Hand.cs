using System.Collections.Generic;

namespace Polyjam2023
{
    public class Hand
    {
        private List<CardLogicData> cards = new ();

        public event System.Action OnChanged;
        
        public IReadOnlyList<CardLogicData> Cards => cards;

        public void AddCards(ICollection<CardLogicData> cardsToAdd)
        {
            cards.AddRange(cardsToAdd);
            OnChanged?.Invoke();
        }
        
        public void RemoveCards(ICollection<CardLogicData> cardsToRemove)
        {
            foreach (var cardToRemove in cardsToRemove)
            {
                int i = 0;
                for (; i < cards.Count; ++i)
                {
                    if (cards[i].name == cardToRemove.name)
                    {
                        cards.RemoveAt(i);
                        break;
                    }
                }
            }
            OnChanged?.Invoke();
        }
    }
}