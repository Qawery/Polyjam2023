using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class EnemyDeckWidget : MonoBehaviour
    {
        private GameplayManager gameplayManager;
        [SerializeField] private TMPro.TextMeshProUGUI deckTitle;
        [SerializeField] private TMPro.TextMeshProUGUI cardsCounter;
        [SerializeField] private Image deckImage;
        [SerializeField] private Sprite cardsPresentImage;
        [SerializeField] private Sprite rootOfEvilPresentImage;
        
        private void Start()
        {
            gameplayManager = FindObjectOfType<DependencyResolver>().GameplayManager;
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(deckTitle, $"Missing {nameof(deckTitle)} on {gameObject.name}.");
            Assert.IsNotNull(cardsCounter, $"Missing {nameof(cardsCounter)} on {gameObject.name}.");
            Assert.IsNotNull(deckImage, $"Missing {nameof(deckImage)} on {gameObject.name}.");
            Assert.IsNotNull(cardsPresentImage, $"Missing {nameof(cardsPresentImage)} on {gameObject.name}.");
            Assert.IsNotNull(rootOfEvilPresentImage, $"Missing {nameof(rootOfEvilPresentImage)} on {gameObject.name}.");
            
            gameplayManager.GameState.EnemyDeck.OnChanged += OnDeckChanged;
            OnDeckChanged();
        }

        private void OnDestroy()
        {
            gameplayManager.GameState.EnemyDeck.OnChanged -= OnDeckChanged;
            gameplayManager = null;
            deckTitle = null;
            cardsCounter = null;
            deckImage = null;
            cardsPresentImage = null;
            rootOfEvilPresentImage = null;
        }

        private void OnDeckChanged()
        {
            if (gameplayManager.GameState.EnemyDeck.Cards.Any(card => card == "Root of Evil"))
            {
                cardsCounter.text = $"Cards left: {gameplayManager.GameState.EnemyDeck.NumberOfCardsInDeck}";
                deckImage.sprite = cardsPresentImage;
            }
            else
            {
                deckTitle.text = "";
                cardsCounter.text = "";
                deckImage.sprite = rootOfEvilPresentImage;
            }
        }
    }
}
