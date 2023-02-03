using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2023
{
    public class Deck
    {
        private Stack<string> cards = new ();

        public event System.Action OnChanged;
        
        public int NumberOfCardsInDeck => cards.Count;

        public void AddCards(ICollection<string> cardsToAdd)
        {
            foreach(var card in cardsToAdd)
            {
                cards.Push(card);
            }
            OnChanged?.Invoke();
        }

        public List<string> TakeCards(int numberOfCards)
        {
            var result = new List<string>();
            while (numberOfCards > 0 && NumberOfCardsInDeck > 0)
            {
                result.Add(cards.Pop());
                --numberOfCards;
            }
            OnChanged?.Invoke();
            return result;
        }

        public void Shuffle()
        {
            var cardList = new List<string>();
            foreach(var card in cards)
            {
                cardList.Add(card);
            }
            cards.Clear();
            
            for (int i = 0; i < cardList.Count; ++i) 
            {
                var temp = cardList[i];
                int randomIndex = Random.Range(i, cardList.Count);
                cardList[i] = cardList[randomIndex];
                cardList[randomIndex] = temp;
            }
            
            foreach(var card in cardList)
            {
                cards.Push(card);
            }
            
            OnChanged?.Invoke();
        }
    }
}
