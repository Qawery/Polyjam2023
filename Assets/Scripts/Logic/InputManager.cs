using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class InputManager : MonoBehaviour
    {
        private GameplayManager gameplayManager;

        private void Awake()
        {
            gameplayManager = FindObjectOfType<DependencyResolver>().GameplayManager;
            Assert.IsNotNull(gameplayManager);
            CardWidget.OnCardWidgetClicked += OnCardWidgetClicked;
            UnitInstanceWidget.OnUnitInstanceWidgetClicked += OnUnitInstanceWidgetClicked;
        }

        private void OnDestroy()
        {
            CardWidget.OnCardWidgetClicked -= OnCardWidgetClicked;
            UnitInstanceWidget.OnUnitInstanceWidgetClicked -= OnUnitInstanceWidgetClicked;
        }

        private void OnCardWidgetClicked(CardWidget cardWidget)
        {
            gameplayManager.PlayPlayerCard(cardWidget.CardName);
        }
        
        private void OnUnitInstanceWidgetClicked(UnitInstanceWidget unitInstanceWidget)
        {
        }
    }
}