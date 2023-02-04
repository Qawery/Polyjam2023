using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class Hand : ICardLocation
    {
        private List<(string name, int quantity)> cards = new ();

        public event System.Action OnChanged;
        
        public IReadOnlyList<(string name, int quantity)> Cards => cards;

        public void AddCard(string cardToAdd)
        {
            AddCards(new List<string>{cardToAdd});
        }

        public void AddCards(ICollection<string> cardsToAdd)
        {
            foreach (var cardToAdd in cardsToAdd)
            {
                bool entryNotFound = true;
                for (int i = 0; i < cards.Count; ++i)
                {
                    var previousEntry = cards[i];
                    if (previousEntry.name == cardToAdd)
                    {
                        cards.RemoveAt(i);
                        cards.Insert(i, (previousEntry.name, previousEntry.quantity + 1));
                        entryNotFound = false;
                        break;
                    }
                }

                if (entryNotFound)
                {
                    cards.Add((cardToAdd, 1));
                }
            }

            cards = cards.OrderBy(card => card.name).ToList();
            
            OnChanged?.Invoke();
        }
        
        public void RemoveCard(string cardName)
        {
            for (int i = 0; i < cards.Count; ++i)
            {
                var previousEntry = cards[i];
                if (previousEntry.name == cardName)
                {
                    cards.RemoveAt(i);
                    if (previousEntry.quantity > 1)
                    {
                        cards.Insert(i, (previousEntry.name, previousEntry.quantity - 1));
                    }
                    OnChanged?.Invoke();
                    return;
                }
            }
            
            Assert.IsFalse(true, $"Card to remove <{cardName}> not present on hand.");
        }
    }
}