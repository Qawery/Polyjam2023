using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class CardPresentationManager : MonoBehaviour
    {
        [SerializeField] private CardLibrary cardLibrary;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private CardPresentation cardPresentationPrefab;

        private void Awake()
        {
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(cardPresentationPrefab, $"Missing {nameof(cardPresentationPrefab)} on {gameObject.name}.");
        }

        private void OnDestroy()
        {
            cardLibrary = null;
            gameplayManager = null;
            cardPresentationPrefab = null;
        }

        private void Start()
        {
            foreach (var cardInHand in gameplayManager.GameWorld.PlayerHand)
            {
                var newCardPresentation = Instantiate(cardPresentationPrefab);
                newCardPresentation.SetPresentationData(cardLibrary.GetCardPresentationData(cardInHand.name));
            }
        }
    }
}