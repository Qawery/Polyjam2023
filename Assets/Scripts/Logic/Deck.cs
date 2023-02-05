using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Polyjam2023
{
    public class Deck
    {
        private Stack<string> cards = new ();

        public event System.Action OnChanged;
        
        public IReadOnlyList<string> Cards => cards.ToList();
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
            var tempList = cards.ToList();
            cards.Clear();
            ShuffleCardList(ref tempList);
            foreach (var tempCard in tempList)
            {
                cards.Push(tempCard);
            }
            OnChanged?.Invoke();
        }

        public static void ShuffleCardList(ref List<string> cardList)
        {
            var tempList = new List<string>();
            tempList.AddRange(cardList);
            cardList.Clear();

            while (tempList.Count > 0)
            {
                int randomIndex = Random.Range(0, tempList.Count);
                cardList.Add(tempList[randomIndex]);
                tempList.RemoveAt(randomIndex);
            }
        }
    }
}
