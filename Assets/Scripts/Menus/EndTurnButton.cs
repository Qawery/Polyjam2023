using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class EndTurnButton : MonoBehaviour
    {
        private GameplayManager gameplayManager;
        private Button button;

        private void Awake()
        {
            gameplayManager = FindObjectOfType<DependencyResolver>().GameplayManager;
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            button = GetComponent<Button>();
            Assert.IsNotNull(button, $"Missing {nameof(button)} on {gameObject.name}.");
            button.onClick.AddListener(() => { gameplayManager.EndPlayerTurn(); });
        }

        private void OnDestroy()
        {
            gameplayManager = null;
            button.onClick.RemoveAllListeners();
            button = null;
        }
    }
}