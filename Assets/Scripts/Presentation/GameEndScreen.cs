using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Polyjam2023
{
    public class GameEndScreen : MonoBehaviour
    {
        [Inject] private GameplayManager gameplayManager;
        [SerializeField] private Image image;
        [SerializeField] private TMPro.TextMeshProUGUI gameEndReasonText;
        [SerializeField] private Button returnToMainMenuButton;
        [SerializeField] private Sprite outOfCardsSprite;
        [SerializeField] private Sprite defeatSprite;
        [SerializeField] private Sprite victorySprite;

        private void Awake()
        {
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            gameplayManager.OnGameEnded += OnGameEnded;
            Assert.IsNotNull(image, $"Missing {nameof(image)} on {gameObject.name}.");
            Assert.IsNotNull(gameEndReasonText, $"Missing {nameof(gameEndReasonText)} on {gameObject.name}.");
            Assert.IsNotNull(returnToMainMenuButton, $"Missing {nameof(returnToMainMenuButton)} on {gameObject.name}.");
            Assert.IsNotNull(outOfCardsSprite, $"Missing {nameof(outOfCardsSprite)} on {gameObject.name}.");
            Assert.IsNotNull(defeatSprite, $"Missing {nameof(defeatSprite)} on {gameObject.name}.");
            Assert.IsNotNull(victorySprite, $"Missing {nameof(victorySprite)} on {gameObject.name}.");
            
            gameObject.SetActive(false);
            returnToMainMenuButton.onClick.AddListener(() => { SceneManager.LoadScene("MainMenu"); });
        }

        private void OnDestroy()
        {
            gameplayManager.OnGameEnded -= OnGameEnded;
            gameplayManager = null;
            image = null;
            gameEndReasonText = null;
            returnToMainMenuButton.onClick.RemoveAllListeners();
            returnToMainMenuButton = null;
        }

        private void OnGameEnded(GameEndReason reason)
        {
            gameObject.SetActive(true);
            switch (reason)
            {
                case GameEndReason.DeckEnded:
                {
                    gameEndReasonText.text = "Out of cards!";
                    image.sprite = outOfCardsSprite;
                    break;
                }
                case GameEndReason.Defeat:
                {
                    gameEndReasonText.text = "Defeat!";
                    image.sprite = defeatSprite;
                    break;
                }
                case GameEndReason.Victory:
                {
                    gameEndReasonText.text = "Victory!";
                    image.sprite = victorySprite;
                    break;
                }
            }
        }
    }
}