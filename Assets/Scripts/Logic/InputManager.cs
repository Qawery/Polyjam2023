using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class InputManager : MonoBehaviour
    {
        private GameplayManager gameplayManager;
        private MenuPanelManager pauseMenu;

        private void Awake()
        {
            var dependencyResolver = FindObjectOfType<DependencyResolver>();
            gameplayManager = dependencyResolver.GameplayManager;
            Assert.IsNotNull(gameplayManager);
            pauseMenu = dependencyResolver.PauseMenu;
            Assert.IsNotNull(pauseMenu);
            CardWidget.OnCardWidgetClicked += OnCardWidgetClicked;
            UnitInstanceWidget.OnUnitInstanceWidgetClicked += OnUnitInstanceWidgetClicked;
        }

        private void OnDestroy()
        {
            CardWidget.OnCardWidgetClicked -= OnCardWidgetClicked;
            UnitInstanceWidget.OnUnitInstanceWidgetClicked -= OnUnitInstanceWidgetClicked;
            gameplayManager = null;
            pauseMenu = null;
        }

        private void OnCardWidgetClicked(CardWidget cardWidget)
        {
            gameplayManager.PlayPlayerCard(cardWidget.CardName);
        }
        
        private void OnUnitInstanceWidgetClicked(UnitInstanceWidget unitInstanceWidget)
        {
        }

        public void EndPlayerTurn()
        {
            gameplayManager.EndPlayerTurn();
        }

        public void ShowPauseMenu()
        {
            pauseMenu.gameObject.SetActive(true);
        }
    }
}