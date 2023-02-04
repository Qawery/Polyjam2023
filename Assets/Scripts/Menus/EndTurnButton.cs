using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class EndTurnButton : MonoBehaviour
    {
        private InputManager inputManager;
        private Button button;

        private void Awake()
        {
            inputManager = FindObjectOfType<DependencyResolver>().InputManager;
            Assert.IsNotNull(inputManager, $"Missing {nameof(inputManager)} on {gameObject.name}.");
            button = GetComponent<Button>();
            Assert.IsNotNull(button, $"Missing {nameof(button)} on {gameObject.name}.");
            button.onClick.AddListener(() => { inputManager.EndPlayerTurn(); });
        }

        private void OnDestroy()
        {
            inputManager = null;
            button.onClick.RemoveAllListeners();
            button = null;
        }
    }
}