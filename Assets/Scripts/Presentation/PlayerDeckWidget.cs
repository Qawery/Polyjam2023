using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Zenject;

namespace Polyjam2023
{
    public class PlayerDeckWidget : MonoBehaviour
    {
        [Inject] private GameplayManager gameplayManager;
        [SerializeField] private TMPro.TextMeshProUGUI cardsCounter;
        [SerializeField] private Image deckImage;
        [SerializeField] private Sprite cardsPresentImage;
        [SerializeField] private Sprite cardsAbsentImage;
        
        private void Awake()
        {
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(cardsCounter, $"Missing {nameof(cardsCounter)} on {gameObject.name}.");
            Assert.IsNotNull(deckImage, $"Missing {nameof(deckImage)} on {gameObject.name}.");
            Assert.IsNotNull(cardsPresentImage, $"Missing {nameof(cardsPresentImage)} on {gameObject.name}.");
            Assert.IsNotNull(cardsAbsentImage, $"Missing {nameof(cardsAbsentImage)} on {gameObject.name}.");
            
            gameplayManager.GameState.PlayerDeck.OnChanged += OnDeckChanged;
            OnDeckChanged();
        }

        private void OnDestroy()
        {
            gameplayManager.GameState.PlayerDeck.OnChanged -= OnDeckChanged;
            gameplayManager = null;
            cardsCounter = null;
            deckImage = null;
            cardsPresentImage = null;
            cardsAbsentImage = null;
        }

        private void OnDeckChanged()
        {
            cardsCounter.text = $"Cards left: {gameplayManager.GameState.PlayerDeck.NumberOfCardsInDeck}";
            deckImage.sprite = gameplayManager.GameState.PlayerDeck.NumberOfCardsInDeck == 0 ? cardsAbsentImage : cardsPresentImage;
        }
    }
}
