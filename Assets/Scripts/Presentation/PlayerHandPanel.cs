using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class PlayerHandPanel : MonoBehaviour
    {
        [SerializeField] private CardLibrary cardLibrary;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private CardWidget cardWidgetPrefab;
        [SerializeField] private RectTransform handContainer;

        private void Awake()
        {
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(cardWidgetPrefab, $"Missing {nameof(cardWidgetPrefab)} on {gameObject.name}.");
            Assert.IsNotNull(handContainer, $"Missing {nameof(handContainer)} on {gameObject.name}.");

            gameplayManager.GameState.PlayerHand.OnChanged += OnPlayerHandChanged;
            OnPlayerHandChanged();
        }

        private void OnDestroy()
        {
            gameplayManager.GameState.PlayerHand.OnChanged -= OnPlayerHandChanged;
            cardLibrary = null;
            gameplayManager = null;
            cardWidgetPrefab = null;
            handContainer = null;
        }

        private void OnPlayerHandChanged()
        {
            while (handContainer.childCount > 0)
            {
                DestroyImmediate(handContainer.GetChild(0).gameObject);
            }
            foreach (var cardInHand in gameplayManager.GameState.PlayerHand.Cards)
            {
                var newCardPresentation = Instantiate(cardWidgetPrefab, handContainer);
                newCardPresentation.SetPresentationData(cardLibrary.GetCardPresentationData(cardInHand.name));
            }
        }
    }
}