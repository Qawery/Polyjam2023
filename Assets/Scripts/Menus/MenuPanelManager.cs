using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class MenuPanelManager : MonoBehaviour
    {
        [SerializeField] private List<MenuPanel> menuPanels = new ();
        private Stack<MenuPanel> activeMenuPanels = new ();

        public event System.Action OnLastPanelClosed;

        private void Awake()
        {
            Assert.IsNotNull(menuPanels);
            Assert.IsTrue(menuPanels.Count > 0, $"No panels in MenuPanelManager on {gameObject.name}.");
            Assert.IsTrue(menuPanels.All(panel => panel != null), $"Null panels present in MenuPanelManager on {gameObject.name}.");
            
            foreach(var menuPanel in menuPanels)
            {
                menuPanel.gameObject.SetActive(false);
                menuPanel.OnRequestTransitionToPanel += SwitchToPanel;
            }
        }

        private void OnEnable()
        {
            SwitchToPanel(menuPanels[0]);
        }

        private void OnDisable()
        {
            foreach (var activeMenuPanel in activeMenuPanels)
            {
                activeMenuPanel.gameObject.SetActive(false);
            }
            activeMenuPanels.Clear();
        }

        private void OnDestroy()
        {
            foreach(var menuPanel in menuPanels)
            {
                menuPanel.gameObject.SetActive(false);
                menuPanel.OnRequestTransitionToPanel -= SwitchToPanel;
            }
            menuPanels.Clear();
            activeMenuPanels.Clear();
        }
        
        private void SwitchToPanel(MenuPanel menuPanel)
        {
            if (menuPanel == null)
            {
                Assert.IsTrue(activeMenuPanels.Count > 0, 
                    $"Requested closing panel while non are open in MenuPanelManager on {gameObject.name}.");
                
                activeMenuPanels.Pop().gameObject.SetActive(false);
                if (activeMenuPanels.Count > 0)
                {
                    activeMenuPanels.Peek().gameObject.SetActive(true);
                }
                else
                {
                    OnLastPanelClosed?.Invoke();
                }
            }
            else
            {
                Assert.IsFalse(activeMenuPanels.Contains(menuPanel), 
                    $"Requested transition to panel <{menuPanel.gameObject.name}> that is already open in MenuPanelManager on {gameObject.name}.");
                Assert.IsTrue(menuPanels.Contains(menuPanel), 
                    $"Requested transition to panel <{menuPanel.gameObject.name}> not present in MenuPanelManager on {gameObject.name}.");
                
                if (activeMenuPanels.Count > 0)
                {
                    activeMenuPanels.Peek().gameObject.SetActive(false);
                }
                activeMenuPanels.Push(menuPanel);
                menuPanel.gameObject.SetActive(true);
            }
        }
    }
}