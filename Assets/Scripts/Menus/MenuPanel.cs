using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] protected Button backButton;
        
        public event System.Action<MenuPanel> OnRequestTransitionToPanel;
        
        protected virtual void Awake()
        {
            Assert.IsNotNull(backButton, $"Missing {nameof(backButton)} on {gameObject.name}.");
            backButton.onClick.AddListener(() => { InvokeOnRequestTransitionToPanel(null); });
        }

        protected virtual void OnDestroy()
        {
            backButton.onClick.RemoveAllListeners();
            backButton = null;
        }

        protected void InvokeOnRequestTransitionToPanel(MenuPanel menuPanel)
        {
            OnRequestTransitionToPanel?.Invoke(menuPanel);
        }
    }
}