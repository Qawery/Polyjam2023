using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    [CreateAssetMenu(fileName = "CardLibrary", menuName = "ScriptableObjects/CardLibrary", order = 1)]
    public class CardLibrary : ScriptableObject
    {
        [SerializeField] private List<CardDescription> cardLibrary = new ();
        private Dictionary<string, CardDescription> cardDescriptionsByName;

        private void OnValidate()
        {
            cardLibrary = cardLibrary.OrderBy(cardDescription => cardDescription.CardName).ToList();
            
            cardDescriptionsByName = new();
            foreach (var cardDescription in cardLibrary)
            {
                Assert.IsNotNull(cardDescription, $"Null {nameof(cardDescription)} in {nameof(cardLibrary)}.");
                Assert.IsFalse(string.IsNullOrEmpty(cardDescription.CardName), $"Null or empty {nameof(cardDescription.CardName)} in {nameof(cardDescription)}.");
                Assert.IsNotNull(cardDescription.Image, $"Null {nameof(cardDescription.Image)} on {nameof(cardDescription)}.");
                Assert.IsFalse(string.IsNullOrEmpty(cardDescription.EffectDescription), $"Null or empty {nameof(cardDescription.EffectDescription)} in {nameof(cardDescription)}.");
                Assert.IsFalse(cardDescriptionsByName.ContainsKey(cardDescription.CardName), $"Duplicate card name <{cardDescription.CardName}> in library.");
                cardDescriptionsByName.Add(cardDescription.CardName, cardDescription);
            }
        }

        public CardDescription GetCardDescription(string name)
        {
            Assert.IsTrue(cardDescriptionsByName.TryGetValue(name, out var cardEntry), $"No data in library for card name <{name}>.");
            return cardEntry;
        }

        public CardPresentationData GetCardPresentationData(string name)
        {
            Assert.IsTrue(cardDescriptionsByName.TryGetValue(name, out var cardEntry), $"No data in library for card name <{name}>.");
            return new CardPresentationData(cardEntry.CardName, cardEntry.Image, cardEntry.EffectDescription, cardEntry.FluffDescription);
        }
    }
}
