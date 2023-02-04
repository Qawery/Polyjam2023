using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class FieldPanel : MonoBehaviour
    {
        [SerializeField] private CardLibrary cardLibrary;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private UnitWidget unitWidgetPrefab;
        [SerializeField] private RectTransform unitWidgetsContainer;
        private List<UnitWidget> unitWidgets = new();

        private void Awake()
        {
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(unitWidgetPrefab, $"Missing {nameof(unitWidgetPrefab)} on {gameObject.name}.");
            Assert.IsNotNull(unitWidgetsContainer, $"Missing {nameof(unitWidgetsContainer)} on {gameObject.name}.");

            int childIndex = 0;
            for (; childIndex < unitWidgetsContainer.childCount;)
            {
                var unitWidget = unitWidgetsContainer.GetChild(0).GetComponent<UnitWidget>();
                if (unitWidget == null)
                {
                    DestroyImmediate(unitWidgetsContainer.GetChild(0).gameObject);
                }
                else
                {
                    unitWidgets.Add(unitWidget);
                    ++childIndex;
                }
            }
            
            gameplayManager.GameState.Field.OnChanged += OnFieldChanged;
            OnFieldChanged();
        }

        private void OnDestroy()
        {
            gameplayManager.GameState.PlayerHand.OnChanged -= OnFieldChanged;
            cardLibrary = null;
            gameplayManager = null;
            unitWidgetPrefab = null;
            unitWidgetsContainer = null;
            unitWidgets.Clear();
        }

        private void OnFieldChanged()
        {
            while (unitWidgets.Count > gameplayManager.GameState.Field.UnitsPresent.Count)
            {
                Destroy(unitWidgets[0].gameObject);
                unitWidgets.RemoveAt(0);
            }
            
            while (unitWidgets.Count < gameplayManager.GameState.Field.UnitsPresent.Count)
            {
                var newUnitWidget = Instantiate(unitWidgetPrefab, unitWidgetsContainer);
                unitWidgets.Add(newUnitWidget);
            }

            int i = 0;
            foreach (var unitInstance in gameplayManager.GameState.Field.UnitsPresent)
            {
                unitWidgets[i].SetPresentationData(unitInstance);
                ++i;
            }
        }
    }
}