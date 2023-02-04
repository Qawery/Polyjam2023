using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    [CreateAssetMenu(fileName = "CardLibrary", menuName = "ScriptableObjects/CardLibrary", order = 1)]
    public class CardLibrary : ScriptableObject
    {
        [SerializeField] private List<CardTemplate> cardTemplateLibrary = new ();
        private Dictionary<string, CardTemplate> cardTemplatesByName;

        private void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            cardTemplateLibrary = cardTemplateLibrary.OrderBy(cardDescription => cardDescription.CardName).ToList();
            
            cardTemplatesByName = new();
            foreach (var cardTemplate in cardTemplateLibrary)
            {
                Assert.IsNotNull(cardTemplate, $"Null {nameof(cardTemplate)} in {nameof(cardTemplateLibrary)}.");
                Assert.IsFalse(string.IsNullOrEmpty(cardTemplate.CardName), $"Null or empty {nameof(cardTemplate.CardName)} in {nameof(cardTemplate)}.");
                Assert.IsNotNull(cardTemplate.Image, $"Null {nameof(cardTemplate.Image)} on {nameof(cardTemplate)}.");
                Assert.IsFalse(string.IsNullOrEmpty(cardTemplate.EffectDescription), $"Null or empty {nameof(cardTemplate.EffectDescription)} in {nameof(cardTemplate)}.");
                Assert.IsFalse(cardTemplatesByName.ContainsKey(cardTemplate.CardName), $"Duplicate card name <{cardTemplate.CardName}> in library.");
                cardTemplatesByName.Add(cardTemplate.CardName, cardTemplate);
            }
        }

        public CardTemplate GetCardTemplate(string name)
        {
            bool foundKey = cardTemplatesByName.TryGetValue(name, out var cardTemplate);
            Assert.IsTrue(foundKey, $"No data in library for card name <{name}>.");
            return cardTemplate;
        }
    }
}
