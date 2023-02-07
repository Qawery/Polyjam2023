using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class ReinforcementsTimer : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI text;
        private GameplayManager gameplayManager;
        
        private void OnEnable()
        {
            Assert.IsNotNull(text);
            gameplayManager = FindObjectOfType<DependencyResolver>().GameplayManager;
            Assert.IsNotNull(gameplayManager);
            gameplayManager.EnemyManager.OnReinforcementsTimerChanged += OnReinforcementsTimerChanged;
            OnReinforcementsTimerChanged();
        }

        private void OnDisable()
        {
            gameplayManager.EnemyManager.OnReinforcementsTimerChanged -= OnReinforcementsTimerChanged;
            gameplayManager = null;
            text = null;
        }

        private void OnReinforcementsTimerChanged()
        {
            gameObject.SetActive(gameplayManager.GameState.EnemyDeck.NumberOfCardsInDeck > 0);
            text.text = $"New enemies in: {gameplayManager.EnemyManager.ReinforcementsTimer}";
        }
    }
}
