using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class GameEndScreen : MonoBehaviour
    {
        private GameplayManager gameplayManager;
        private PresentationManager presentationManager;
        [SerializeField] private Image image;
        [SerializeField] private TMPro.TextMeshProUGUI gameEndReasonText;
        [SerializeField] private Button returnToMainMenuButton;
        [SerializeField] private Sprite defeatSprite;
        [SerializeField] private Sprite victorySprite;

        private void Start()
        {
            var dependencyResolver = FindObjectOfType<DependencyResolver>();
            gameplayManager = dependencyResolver.GameplayManager;
            presentationManager = dependencyResolver.PresentationManager;
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(presentationManager, $"Missing {nameof(presentationManager)} on {gameObject.name}.");
            gameplayManager.OnGameEnded += OnGameEnded;
            Assert.IsNotNull(image, $"Missing {nameof(image)} on {gameObject.name}.");
            Assert.IsNotNull(gameEndReasonText, $"Missing {nameof(gameEndReasonText)} on {gameObject.name}.");
            Assert.IsNotNull(returnToMainMenuButton, $"Missing {nameof(returnToMainMenuButton)} on {gameObject.name}.");
            Assert.IsNotNull(defeatSprite, $"Missing {nameof(defeatSprite)} on {gameObject.name}.");
            Assert.IsNotNull(victorySprite, $"Missing {nameof(victorySprite)} on {gameObject.name}.");
            
            gameObject.SetActive(false);
            returnToMainMenuButton.onClick.AddListener(() => { SceneManager.LoadScene("MainMenu"); });
        }

        private void OnDestroy()
        {
            gameplayManager.OnGameEnded -= OnGameEnded;
            gameplayManager = null;
            presentationManager = null;
            image = null;
            gameEndReasonText = null;
            returnToMainMenuButton.onClick.RemoveAllListeners();
            returnToMainMenuButton = null;
        }

        private void OnGameEnded(GameEndReason reason)
        {
            gameplayManager.OnGameEnded -= OnGameEnded;
            float timer = 2.0f;
            presentationManager.AddPresentationTask(new PresentationTask
            (
                () => { },
                (float deltaTime) => { timer -= deltaTime; },
                () => 
                {
                    gameObject.SetActive(true);
                    switch (reason)
                    {
                        case GameEndReason.DeckEnded:
                        {
                            gameEndReasonText.text = "Defeat! \nDeck has run out of cards!";
                            gameEndReasonText.color = Color.red;
                            image.sprite = defeatSprite;
                            break;
                        }
                        case GameEndReason.Defeat:
                        {
                            gameEndReasonText.text = "Defeat!";
                            gameEndReasonText.color = Color.red;
                            image.sprite = defeatSprite;
                            break;
                        }
                        case GameEndReason.Victory:
                        {
                            gameEndReasonText.text = "Victory! \nRoot of Evil has been vanquished!";
                            gameEndReasonText.color = Color.green;
                            image.sprite = victorySprite;
                            break;
                        }
                    } },
                () => { return timer <= 0.0f; }
            ));
        }
    }
}