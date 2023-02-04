using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class Hand : ICardLocation
    {
        private List<(string name, int quantity)> cards = new ();

        public event System.Action<(string name, int quantity)> OnCardAdded;
        public event System.Action<(string name, int quantity)> OnCardRemoved;
        
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
                (string name, int quantity) existingEntry = ("", 0);
                for (int i = 0; i < cards.Count; ++i)
                {
                    existingEntry = cards[i];
                    if (existingEntry.name == cardToAdd)
                    {
                        cards.RemoveAt(i);
                        existingEntry = (existingEntry.name, existingEntry.quantity + 1);
                        cards.Insert(i, existingEntry);
                        entryNotFound = false;
                        break;
                    }
                }

                if (entryNotFound)
                {
                    existingEntry = (cardToAdd, 1);
                    cards.Add(existingEntry);
                }
                
                OnCardAdded?.Invoke(existingEntry);
            }

            cards = cards.OrderBy(card => card.name).ToList();
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
                    OnCardRemoved?.Invoke((previousEntry.name, previousEntry.quantity - 1));
                    return;
                }
            }
            
            Assert.IsFalse(true, $"Card to remove <{cardName}> not present on hand.");
        }
    }
}