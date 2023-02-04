using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Polyjam2023
{
    public class PlayerHandPanel : MonoBehaviour
    {
        [Inject] private CardLibrary cardLibrary;
        [Inject] private GameplayManager gameplayManager;
        [Inject] private CardWidget cardWidgetPrefab;
        [SerializeField] private RectTransform cardWidgetsContainer;
        private List<CardWidget> cardWidgets = new();

        private void Awake()
        {
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
                    cardWidget.OnClicked += () => { gameplayManager.PlayPlayerCard(cardWidget.CardName); };
                    ++childIndex;
                }
            }
            
            gameplayManager.GameState.PlayerHand.OnChanged += OnPlayerHandChanged;
            OnPlayerHandChanged();
        }

        private void OnDestroy()
        {
            gameplayManager.GameState.PlayerHand.OnChanged -= OnPlayerHandChanged;
            cardLibrary = null;
            gameplayManager = null;
            cardWidgetPrefab = null;
            cardWidgetsContainer = null;
            cardWidgets.Clear();
        }

        private void OnPlayerHandChanged()
        {
            while (cardWidgets.Count > gameplayManager.GameState.PlayerHand.Cards.Count)
            {
                Destroy(cardWidgets[0].gameObject);
                cardWidgets.RemoveAt(0);
            }
            
            while (cardWidgets.Count < gameplayManager.GameState.PlayerHand.Cards.Count)
            {
                var newCardWidget = Instantiate(cardWidgetPrefab, cardWidgetsContainer);
                cardWidgets.Add(newCardWidget);
                newCardWidget.OnClicked += () => { gameplayManager.PlayPlayerCard(newCardWidget.CardName); };
            }

            int i = 0;
            foreach (var cardEntry in gameplayManager.GameState.PlayerHand.Cards)
            {
                cardWidgets[i].SetPresentationData(cardLibrary.GetCardTemplate(cardEntry.name), cardEntry.quantity);
                ++i;
            }
        }
    }
}