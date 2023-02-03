using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private MenuPanelManager pauseMenu;
        
        private Button pauseButton;

        private void Awake()
        {
            Assert.IsNotNull(pauseMenu, $"Missing {nameof(pauseMenu)} on {gameObject.name}.");
            pauseButton = GetComponent<Button>();
            Assert.IsNotNull(pauseButton, $"Missing {nameof(pauseButton)} on {gameObject.name}.");
            pauseButton.onClick.AddListener(ShowPauseMenu);
        }

        private void OnDestroy()
        {
            pauseButton.onClick.RemoveAllListeners();
            pauseButton = null;
        }

        private void ShowPauseMenu()
        {
            pauseMenu.gameObject.SetActive(true);
        }
    }
}