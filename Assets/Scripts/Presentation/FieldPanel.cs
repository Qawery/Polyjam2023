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
        [SerializeField] private RectTransform enemyUnitWidgetsContainer;
        [SerializeField] private RectTransform playerUnitWidgetsContainer;
        private List<UnitWidget> enemyUnitWidgets = new();
        private List<UnitWidget> playerUnitWidgets = new();

        private void Awake()
        {
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(unitWidgetPrefab, $"Missing {nameof(unitWidgetPrefab)} on {gameObject.name}.");
            Assert.IsNotNull(enemyUnitWidgetsContainer, $"Missing {nameof(enemyUnitWidgetsContainer)} on {gameObject.name}.");
            Assert.IsNotNull(playerUnitWidgetsContainer, $"Missing {nameof(playerUnitWidgetsContainer)} on {gameObject.name}.");

            CleanupPresentWidgets(enemyUnitWidgetsContainer, ref enemyUnitWidgets);
            CleanupPresentWidgets(playerUnitWidgetsContainer, ref playerUnitWidgets);
            
            gameplayManager.GameState.Field.OnEnemyUnitsChanged += OnEnemyUnitsChanged;
            gameplayManager.GameState.Field.OnPlayerUnitsChanged += OnPlayerUnitsChanged;
            
            OnEnemyUnitsChanged();
            OnPlayerUnitsChanged();
        }

        private void OnDestroy()
        {
            gameplayManager.GameState.Field.OnEnemyUnitsChanged -= OnEnemyUnitsChanged;
            gameplayManager.GameState.Field.OnPlayerUnitsChanged -= OnPlayerUnitsChanged;
            cardLibrary = null;
            gameplayManager = null;
            unitWidgetPrefab = null;
            enemyUnitWidgetsContainer = null;
            playerUnitWidgetsContainer = null;
            enemyUnitWidgets.Clear();
            playerUnitWidgets.Clear();
        }

        private void CleanupPresentWidgets(RectTransform widgetContainer, ref List<UnitWidget> widgetCollection)
        {
            int childIndex = 0;
            for (; childIndex < widgetContainer.childCount;)
            {
                var unitWidget = widgetContainer.GetChild(0).GetComponent<UnitWidget>();
                if (unitWidget == null)
                {
                    DestroyImmediate(widgetContainer.GetChild(0).gameObject);
                }
                else
                {
                    widgetCollection.Add(unitWidget);
                    ++childIndex;
                }
            }
        }
        
        private void OnEnemyUnitsChanged()
        {
            var enemyUnitsPresent = gameplayManager.GameState.Field.EnemyUnitsPresent;
            OnUnitsChanged(ref enemyUnitsPresent, ref enemyUnitWidgetsContainer, ref enemyUnitWidgets);
        }
        
        private void OnPlayerUnitsChanged()
        {
            var playerUnitsPresent = gameplayManager.GameState.Field.PlayerUnitsPresent;
            OnUnitsChanged(ref playerUnitsPresent, ref playerUnitWidgetsContainer, ref playerUnitWidgets);
        }

        private void OnUnitsChanged(ref IReadOnlyList<UnitInstance> unitsPresent, ref RectTransform widgetContainer, ref List<UnitWidget> widgetCollection)
        {
            while (widgetCollection.Count > unitsPresent.Count)
            {
                Destroy(widgetCollection[0].gameObject);
                widgetCollection.RemoveAt(0);
            }
            
            while (widgetCollection.Count < unitsPresent.Count)
            {
                var newUnitWidget = Instantiate(unitWidgetPrefab, widgetContainer);
                widgetCollection.Add(newUnitWidget);
            }

            int i = 0;
            foreach (var unitInstance in unitsPresent)
            {
                widgetCollection[i].SetPresentationData(unitInstance);
                ++i;
            }
        }
    }
}