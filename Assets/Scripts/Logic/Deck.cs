using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2023
{
    public class Deck
    {
        private Stack<CardLogicData> cards = new ();

        public event System.Action OnChanged;
        
        public int NumberOfCardsInDeck => cards.Count;

        public void AddCards(ICollection<CardLogicData> cardsToAdd)
        {
            foreach(var card in cardsToAdd)
            {
                cards.Push(card);
            }
            OnChanged?.Invoke();
        }

        public List<CardLogicData> TakeCards(int numberOfCards)
        {
            var result = new List<CardLogicData>();
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
            var cardList = new List<CardLogicData>();
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
