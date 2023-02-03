using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace Polyjam2023
{
    public class CardPresentationManager : MonoBehaviour
    {
        [SerializeField] private CardLibrary cardLibrary;
        [SerializeField] private GameplayManager gameplayManager;
        [FormerlySerializedAs("cardViewPrefab")] [FormerlySerializedAs("cardPresentationPrefab")] [SerializeField] private CardWidget cardWidgetPrefab;
        [SerializeField] private RectTransform handContainer;

        private void Awake()
        {
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(cardWidgetPrefab, $"Missing {nameof(cardWidgetPrefab)} on {gameObject.name}.");
            Assert.IsNotNull(handContainer, $"Missing {nameof(handContainer)} on {gameObject.name}.");
            while (handContainer.childCount > 0)
            {
                DestroyImmediate(handContainer.GetChild(0).gameObject);
            }
        }

        private void OnDestroy()
        {
            cardLibrary = null;
            gameplayManager = null;
            cardWidgetPrefab = null;
            handContainer = null;
        }

        private void Start()
        {
            foreach (var cardInHand in gameplayManager.GameState.PlayerHand)
            {
                var newCardPresentation = Instantiate(cardWidgetPrefab, handContainer);
                newCardPresentation.SetPresentationData(cardLibrary.GetCardPresentationData(cardInHand.name));
            }
        }
    }
}