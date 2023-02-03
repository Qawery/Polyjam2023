using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class PauseMenu : MenuPanel
    {
        [SerializeField] private Button howToPlayButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button returnToMainMenuButton;
        
        [SerializeField] private MenuPanel howToPlayPanel;
        [SerializeField] private MenuPanel creditsPanel;

        protected override void Awake()
        {
            base.Awake();
            
            Assert.IsNotNull(howToPlayButton, $"Missing {nameof(howToPlayButton)} on {gameObject.name}.");
            Assert.IsNotNull(howToPlayPanel, $"Missing {nameof(howToPlayPanel)} on {gameObject.name}.");
            howToPlayButton.onClick.AddListener(() => { InvokeOnRequestTransitionToPanel(howToPlayPanel); });
            
            Assert.IsNotNull(creditsButton, $"Missing {nameof(creditsButton)} on {gameObject.name}.");
            Assert.IsNotNull(creditsPanel, $"Missing {nameof(creditsPanel)} on {gameObject.name}.");
            creditsButton.onClick.AddListener(() => { InvokeOnRequestTransitionToPanel(creditsPanel); });
            
            Assert.IsNotNull(returnToMainMenuButton, $"Missing {nameof(returnToMainMenuButton)} on {gameObject.name}.");
            returnToMainMenuButton.onClick.AddListener(() => { LoadingScreen.Instance.LoadScene("MainMenu");} );
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            howToPlayButton.onClick.RemoveAllListeners();
            howToPlayButton = null;
            howToPlayPanel = null;
            
            creditsButton.onClick.RemoveAllListeners();
            creditsButton = null;
            creditsPanel = null;
            
            returnToMainMenuButton.onClick.RemoveAllListeners();
            returnToMainMenuButton = null;
        }
    }
}