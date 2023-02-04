using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class PlayerHandPanel : MonoBehaviour
    {
        private PresentationManager presentationManager;
        private FloatingText floatingTextPrefab;
        private CardLibrary cardLibrary;
        private GameplayManager gameplayManager;
        private CardWidget cardWidgetPrefab;
        [SerializeField] private RectTransform cardWidgetsContainer;
        private List<CardWidget> cardWidgets = new();

        private void Awake()
        {
            var dependencyResolver = FindObjectOfType<DependencyResolver>();
            presentationManager = dependencyResolver.PresentationManager;
            floatingTextPrefab = dependencyResolver.FloatingTextPrefab;
            cardLibrary = dependencyResolver.CardLibrary;
            gameplayManager = dependencyResolver.GameplayManager;
            cardWidgetPrefab = dependencyResolver.CardWidgetPrefab;
            
            Assert.IsNotNull(presentationManager, $"Missing {nameof(presentationManager)} on {gameObject.name}.");
            Assert.IsNotNull(floatingTextPrefab, $"Missing {nameof(floatingTextPrefab)} on {gameObject.name}.");
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(cardWidgetPrefab, $"Missing {nameof(cardWidgetPrefab)} on {gameObject.name}.");
            Assert.IsNotNull(cardWidgetsContainer, $"Missing {nameof(cardWidgetsContainer)} on {gameObject.name}.");

            int childIndex = 0;
            for (; childIndex < cardWidgetsContainer.childCount;)
            {
                var cardWidget = cardWidgetsContainer.GetChild(0).GetComponent<CardWidget>();
                if (cardWidget == null)
                {
                    DestroyImmediate(cardWidgetsContainer.GetChild(0).gameObject);
                }
                else
                {
                    cardWidgets.Add(cardWidget);
                    ++childIndex;
                }
            }
            
            while (cardWidgets.Count > gameplayManager.GameState.PlayerHand.Cards.Count)
            {
                Destroy(cardWidgets[0].gameObject);
                cardWidgets.RemoveAt(0);
            }
            
            while (cardWidgets.Count < gameplayManager.GameState.PlayerHand.Cards.Count)
            {
                var newCardWidget = Instantiate(cardWidgetPrefab, cardWidgetsContainer);
                cardWidgets.Add(newCardWidget);
            }

            int i = 0;
            foreach (var cardEntry in gameplayManager.GameState.PlayerHand.Cards)
            {
                cardWidgets[i].SetPresentationData(gameplayManager.GameState.PlayerHand, cardLibrary.GetCardTemplate(cardEntry.name), cardEntry.quantity);
                ++i;
            }
            
            gameplayManager.GameState.PlayerHand.OnCardAdded += OnCardAdded;
            gameplayManager.GameState.PlayerHand.OnCardRemoved += OnCardRemoved;
        }

        private void OnDestroy()
        {
            gameplayManager.GameState.PlayerHand.OnCardAdded -= OnCardAdded;
            gameplayManager.GameState.PlayerHand.OnCardRemoved -= OnCardRemoved;
            presentationManager = null;
            floatingTextPrefab = null;
            cardLibrary = null;
            gameplayManager = null;
            cardWidgetPrefab = null;
            cardWidgetsContainer = null;
            cardWidgets.Clear();
        }

        private void OnCardAdded(string addedCardName)
        {
            var newFloatingText = Instantiate(floatingTextPrefab);
            newFloatingText.gameObject.SetActive(false);
            presentationManager.AddPresentationTask(new PresentationTask
            (() =>
                {
                    CardWidget foundCardWidget = null;
                    bool entryNotFound = true;
                    for (int i = 0; i < cardWidgets.Count; ++i)
                    {
                        foundCardWidget = cardWidgets[i];
                        if (foundCardWidget.CardName == addedCardName)
                        {
                            entryNotFound = false;
                            break;
                        }
                    }

                    if (entryNotFound)
                    {
                        foundCardWidget = Instantiate(cardWidgetPrefab, cardWidgetsContainer);
                        cardWidgets.Add(foundCardWidget);
                    }

                    var cardEntry = gameplayManager.GameState.PlayerHand.Cards.FirstOrDefault(entry => entry.name == addedCardName);
                    foundCardWidget.SetPresentationData(gameplayManager.GameState.PlayerHand, cardLibrary.GetCardTemplate(cardEntry.name), cardEntry.quantity);
                    cardWidgets.ForEach(cardWidget => cardWidget.transform.SetParent(null));
                    cardWidgets = cardWidgets.OrderBy(widget => widget.CardName).ToList();
                    cardWidgets.ForEach(cardWidget => cardWidget.transform.SetParent(cardWidgetsContainer));
                    
                    newFloatingText.transform.SetParent(foundCardWidget.transform);
                    newFloatingText.transform.position = foundCardWidget.transform.position;
                    newFloatingText.SetText("Card added");
                    newFloatingText.gameObject.SetActive(true);
                },
                (float deltaTime) => { },
                () => { },
                () => newFloatingText == null
            ));
        }
        
        private void OnCardRemoved(string removedCardName)
        {
            var newFloatingText = Instantiate(floatingTextPrefab);
            newFloatingText.gameObject.SetActive(false);
            presentationManager.AddPresentationTask(new PresentationTask
            (() =>
                {
                    var foundCardWidget = cardWidgets.FirstOrDefault(widget => widget.CardName == removedCardName);
                    newFloatingText.SetText("Card Removed");
                    newFloatingText.gameObject.SetActive(true);

                    if (gameplayManager.GameState.PlayerHand.Cards.Any(entry => entry.name == removedCardName))
                    {
                        var cardEntry = gameplayManager.GameState.PlayerHand.Cards.FirstOrDefault(entry =>
                            entry.name == removedCardName);
                        foundCardWidget.SetPresentationData(gameplayManager.GameState.PlayerHand, cardLibrary.GetCardTemplate(cardEntry.name), cardEntry.quantity);
                        newFloatingText.transform.SetParent(foundCardWidget.transform);
                        newFloatingText.transform.position = foundCardWidget.transform.position;
                    }
                    else
                    {
                        newFloatingText.transform.SetParent(cardWidgetsContainer);
                        newFloatingText.transform.position = foundCardWidget.transform.position;
                        cardWidgets.Remove(foundCardWidget);
                        Destroy(foundCardWidget.gameObject);
                    }
                },
                (float deltaTime) => { },
                () => { },
                () => newFloatingText == null
            ));
        }
    }
}