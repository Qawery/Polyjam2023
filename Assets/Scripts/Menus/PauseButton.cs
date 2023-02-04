using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class PauseButton : MonoBehaviour
    {
        private PresentationManager presentationManager;
        
        private Button button;

        private void Awake()
        {
            presentationManager = FindObjectOfType<DependencyResolver>().PresentationManager;
            Assert.IsNotNull(presentationManager, $"Missing {nameof(presentationManager)} on {gameObject.name}.");
            button = GetComponent<Button>();
            Assert.IsNotNull(button, $"Missing {nameof(button)} on {gameObject.name}.");
            button.onClick.AddListener(() => { presentationManager.ShowPauseMenu(); });
        }

        private void OnDestroy()
        {
            presentationManager = null;
            button.onClick.RemoveAllListeners();
            button = null;
        }
    }
}