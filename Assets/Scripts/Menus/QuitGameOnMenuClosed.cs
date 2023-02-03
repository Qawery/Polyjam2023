using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class QuitGameOnMenuClosed : MonoBehaviour
    {
        private MenuPanelManager menuPanelManager;

        private void Awake()
        {
            menuPanelManager = GetComponent<MenuPanelManager>();
            Assert.IsNotNull(menuPanelManager, $"Missing {nameof(menuPanelManager)} on {gameObject.name}.");
            menuPanelManager.OnLastPanelClosed += QuitGame;
        }

        private void OnDestroy()
        {
            menuPanelManager.OnLastPanelClosed -= QuitGame;
            menuPanelManager = null;
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}