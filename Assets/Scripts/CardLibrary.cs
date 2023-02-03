using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    [CreateAssetMenu(fileName = "CardLibrary", menuName = "ScriptableObjects/CardLibrary", order = 1)]
    public class CardLibrary : ScriptableObject
    {
        [SerializeField] private List<CardLibraryEntry> cardLibrary = new ();
        private Dictionary<string, CardLibraryEntry> cardEntriesByName;

        private void OnValidate()
        {
            GenerateCardsData();
        }

        public CardLogicData GetCardLogic(string name)
        {
            if (cardEntriesByName == null)
            {
                GenerateCardsData();
            }
            Assert.IsTrue(cardEntriesByName.TryGetValue(name, out var cardEntry), $"No data for card name <{name}>.");
            return new CardLogicData(cardEntry.name);
        }

        public CardPresentationData GetCardPresentationData(string name)
        {
            if (cardEntriesByName == null)
            {
                GenerateCardsData();
            }
            Assert.IsTrue(cardEntriesByName.TryGetValue(name, out var cardEntry), $"No data in library for card name <{name}>.");
            return new CardPresentationData(cardEntry.name, cardEntry.image, cardEntry.description);
        }

        private void GenerateCardsData()
        {
            cardEntriesByName = new();
            foreach (var cardEntry in cardLibrary)
            {
                Assert.IsNotNull(cardEntry, $"Null entry in {nameof(cardLibrary)}.");
                Assert.IsFalse(string.IsNullOrEmpty(cardEntry.name), $"Null or empty {nameof(cardEntry.name)} in {nameof(cardLibrary)}.");
                Assert.IsNotNull(cardEntry.image, $"Null {nameof(cardEntry.image)} in {nameof(cardLibrary)}.");
                Assert.IsFalse(string.IsNullOrEmpty(cardEntry.description), $"Null or empty {nameof(cardEntry.description)} in {nameof(cardLibrary)}.");
                Assert.IsFalse(cardEntriesByName.ContainsKey(cardEntry.name), $"Duplicate entry for card <{cardEntry.name}>.");
                cardEntriesByName.Add(cardEntry.name, cardEntry);
            }
        }
    }
    
    [Serializable]
    public class CardLibraryEntry
    {
        public string name;
        public Sprite image;
        public string description;
    }
}
