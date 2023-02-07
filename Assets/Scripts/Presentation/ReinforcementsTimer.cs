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
            if (gameplayManager?.EnemyManager != null)
            {
                gameplayManager.EnemyManager.OnReinforcementsTimerChanged -= OnReinforcementsTimerChanged;
            }
            gameplayManager = null;
            text = null;
        }

        private void OnReinforcementsTimerChanged()
        {
            text.text = $"New enemies in: {gameplayManager.EnemyManager.ReinforcementsTimer}";
            gameObject.SetActive(!gameplayManager.EnemyManager.BossSpawned && gameplayManager.GameState.EnemyDeck.NumberOfCardsInDeck > 0);
        }
    }
}
