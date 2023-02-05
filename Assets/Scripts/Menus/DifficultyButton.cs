using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class DifficultyButton : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private TMPro.TextMeshProUGUI text;
        
        private void Awake()
        {
            Assert.IsNotNull(gameSettings);
            Assert.IsNotNull(text);
            var button = GetComponent<Button>();
            Assert.IsNotNull(button);
            button.onClick.AddListener(() =>
            {
                gameSettings.difficulty = (Difficulty) (((int) gameSettings.difficulty + 1) % Enum.GetValues(typeof(Difficulty)).Length);
                text.text = $"Difficulty:\n{gameSettings.difficulty.ToString()}";
            });
        }

        private void OnDestroy()
        {
            gameSettings = null;
        }
    }
}
