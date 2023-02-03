using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace Polyjam2023
{
    [CreateAssetMenu(fileName = "CardLibrary", menuName = "ScriptableObjects/CardLibrary", order = 1)]
    public class CardLibrary : ScriptableObject
    {
        [SerializeField] private List<CardLibraryEntry> cardLibrary = new ();
        private Dictionary<string, CardLibraryEntry> cardEntriesByName;

        private void OnValidate()
        {
            cardEntriesByName = new();
            foreach (var cardEntry in cardLibrary)
            {
                Assert.IsNotNull(cardEntry, $"Null {nameof(cardEntry)} in {nameof(cardLibrary)}.");
                Assert.IsNotNull(cardEntry.cardLogic, $"Null {nameof(cardEntry.cardLogic)} in {nameof(cardEntry)}.");
                Assert.IsFalse(string.IsNullOrEmpty(cardEntry.cardLogic.CardName), $"Null or empty {nameof(cardEntry.cardLogic.CardName)} in {nameof(cardEntry)}.");
                Assert.IsNotNull(cardEntry.image, $"Null {nameof(cardEntry.image)} in {nameof(cardEntry)}.");
                Assert.IsFalse(string.IsNullOrEmpty(cardEntry.description), $"Null or empty {nameof(cardEntry.description)} in {nameof(cardEntry)}.");
                Assert.IsFalse(cardEntriesByName.ContainsKey(cardEntry.cardLogic.name), $"Duplicate entry for card <{cardEntry.cardLogic.name}>.");
                cardEntry.cardName = cardEntry.cardLogic.CardName;
                cardEntriesByName.Add(cardEntry.cardLogic.CardName, cardEntry);
            }
        }

        public CardLogic GetCardLogic(string name)
        {
            Assert.IsTrue(cardEntriesByName.TryGetValue(name, out var cardEntry), $"No data in library for card name <{name}>.");
            return cardEntry.cardLogic;
        }

        public CardPresentationData GetCardPresentationData(string name)
        {
            Assert.IsTrue(cardEntriesByName.TryGetValue(name, out var cardEntry), $"No data in library for card name <{name}>.");
            return new CardPresentationData(cardEntry.cardLogic.CardName, cardEntry.image, cardEntry.description);
        }
    }
    
    [Serializable]
    public class CardLibraryEntry
    {
        public string cardName;
        public Sprite image;
        public string description;
        public CardLogic cardLogic;
    }
}
