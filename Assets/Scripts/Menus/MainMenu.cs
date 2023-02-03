using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class MainMenu : MenuPanel
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button howToPlayButton;
        [SerializeField] private Button creditsButton;
        
        [SerializeField] private MenuPanel howToPlayPanel;
        [SerializeField] private MenuPanel creditsPanel;

        protected override void Awake()
        {
            base.Awake();
            
            Assert.IsNotNull(newGameButton, $"Missing {nameof(newGameButton)} on {gameObject.name}.");
            newGameButton.onClick.AddListener(() => { SceneManager.LoadScene("Gameplay"); });
            
            Assert.IsNotNull(howToPlayButton, $"Missing {nameof(howToPlayButton)} on {gameObject.name}.");
            Assert.IsNotNull(howToPlayPanel, $"Missing {nameof(howToPlayPanel)} on {gameObject.name}.");
            howToPlayButton.onClick.AddListener(() => { InvokeOnRequestTransitionToPanel(howToPlayPanel); });
            
            Assert.IsNotNull(creditsButton, $"Missing {nameof(creditsButton)} on {gameObject.name}.");
            Assert.IsNotNull(creditsPanel, $"Missing {nameof(creditsPanel)} on {gameObject.name}.");
            creditsButton.onClick.AddListener(() => { InvokeOnRequestTransitionToPanel(creditsPanel); });
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            newGameButton.onClick.RemoveAllListeners();
            newGameButton = null;
            
            howToPlayButton.onClick.RemoveAllListeners();
            howToPlayButton = null;
            howToPlayPanel = null;
            
            creditsButton.onClick.RemoveAllListeners();
            creditsButton = null;
            creditsPanel = null;
        }
    }
}