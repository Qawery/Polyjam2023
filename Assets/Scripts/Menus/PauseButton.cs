using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private MenuPanelManager pauseMenu;
        
        private Button button;

        private void Awake()
        {
            Assert.IsNotNull(pauseMenu, $"Missing {nameof(pauseMenu)} on {gameObject.name}.");
            button = GetComponent<Button>();
            Assert.IsNotNull(button, $"Missing {nameof(button)} on {gameObject.name}.");
            button.onClick.AddListener(() => { pauseMenu.gameObject.SetActive(true); });
        }

        private void OnDestroy()
        {
            pauseMenu = null;
            button.onClick.RemoveAllListeners();
            button = null;
        }
    }
}