using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class DisableObjectOnMenuClosed : MonoBehaviour
    {
        private MenuPanelManager menuPanelManager;

        private void Awake()
        {
            menuPanelManager = GetComponent<MenuPanelManager>();
            Assert.IsNotNull(menuPanelManager, $"Missing {nameof(menuPanelManager)} on {gameObject.name}.");
            menuPanelManager.OnLastPanelClosed += DisableObject;
        }

        private void OnDestroy()
        {
            menuPanelManager.OnLastPanelClosed -= DisableObject;
            menuPanelManager = null;
        }

        private void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}