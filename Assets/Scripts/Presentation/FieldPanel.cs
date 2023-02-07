using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class FieldPanel : MonoBehaviour
    {
        private PresentationManager presentationManager;
        private CardLibrary cardLibrary;
        private GameplayManager gameplayManager;
        private UnitInstanceWidget unitInstanceWidgetPrefab;
        private FloatingText floatingTextPrefab;
        [SerializeField] private RectTransform enemyUnitWidgetsContainer;
        [SerializeField] private RectTransform playerUnitWidgetsContainer;
        private List<UnitInstanceWidget> enemyUnitWidgets = new ();
        private List<UnitInstanceWidget> playerUnitWidgets = new ();

        private void Awake()
        {
            var dependencyResolver = FindObjectOfType<DependencyResolver>();
            presentationManager = dependencyResolver.PresentationManager;
            cardLibrary = dependencyResolver.CardLibrary;
            gameplayManager = dependencyResolver.GameplayManager;
            unitInstanceWidgetPrefab = dependencyResolver.UnitInstanceWidgetPrefab;
            floatingTextPrefab = dependencyResolver.FloatingTextPrefab;
        
            Assert.IsNotNull(cardLibrary, $"Missing {nameof(cardLibrary)} on {gameObject.name}.");
            Assert.IsNotNull(gameplayManager, $"Missing {nameof(gameplayManager)} on {gameObject.name}.");
            Assert.IsNotNull(unitInstanceWidgetPrefab, $"Missing {nameof(unitInstanceWidgetPrefab)} on {gameObject.name}.");
            Assert.IsNotNull(enemyUnitWidgetsContainer, $"Missing {nameof(enemyUnitWidgetsContainer)} on {gameObject.name}.");
            Assert.IsNotNull(playerUnitWidgetsContainer, $"Missing {nameof(playerUnitWidgetsContainer)} on {gameObject.name}.");

            CleanupPresentWidgets(enemyUnitWidgetsContainer, ref enemyUnitWidgets);
            CleanupPresentWidgets(playerUnitWidgetsContainer, ref playerUnitWidgets);

            gameplayManager.GameState.Field.OnWrathOfTheForest += OnWrathOfTheForest;
            gameplayManager.GameState.Field.OnUnitAdded += OnUnitAdded;
            gameplayManager.GameState.Field.OnUnitWounded += OnUnitWounded;
            gameplayManager.GameState.Field.OnUnitKilled += OnUnitKilled;
            
            RefreshFieldData();
        }

        private void OnDestroy()
        {
            gameplayManager.GameState.Field.OnWrathOfTheForest -= OnWrathOfTheForest;
            gameplayManager.GameState.Field.OnUnitAdded -= OnUnitAdded;
            gameplayManager.GameState.Field.OnUnitWounded -= OnUnitWounded;
            gameplayManager.GameState.Field.OnUnitKilled -= OnUnitKilled;

            presentationManager = null;
            cardLibrary = null;
            gameplayManager = null;
            unitInstanceWidgetPrefab = null;
            floatingTextPrefab = null;
            enemyUnitWidgetsContainer = null;
            playerUnitWidgetsContainer = null;
            enemyUnitWidgets.Clear();
            playerUnitWidgets.Clear();
        }

        private void RefreshFieldData()
        {
            var enemyUnitsPresent = gameplayManager.GameState.Field.EnemyUnitsPresent;
            OnUnitsChanged(ref enemyUnitsPresent, ref enemyUnitWidgetsContainer, ref enemyUnitWidgets);
            var playerUnitsPresent = gameplayManager.GameState.Field.PlayerUnitsPresent;
            OnUnitsChanged(ref playerUnitsPresent, ref playerUnitWidgetsContainer, ref playerUnitWidgets);
        }
        
        private void OnWrathOfTheForest()
        {
            var newFloatingText = Instantiate(floatingTextPrefab);
            newFloatingText.AttachTo(playerUnitWidgetsContainer);
            newFloatingText.SetText("Wrath of the forest");
            newFloatingText.gameObject.SetActive(false);
            
            presentationManager.AddPresentationTask(new PresentationTask
            (() =>
                {
                    RefreshFieldData();
                    newFloatingText.gameObject.SetActive(true);
                },
                (float deltaTime) => { },
                () => { },
                () => newFloatingText == null
            ));
        }

        private void CleanupPresentWidgets(RectTransform widgetContainer, ref List<UnitInstanceWidget> widgetCollection)
        {
            int childIndex = 0;
            for (; childIndex < widgetContainer.childCount;)
            {
                var unitWidget = widgetContainer.GetChild(0).GetComponent<UnitInstanceWidget>();
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

        private void OnUnitsChanged(ref IReadOnlyList<UnitInstance> unitsPresent, ref RectTransform widgetContainer, ref List<UnitInstanceWidget> widgetCollection)
        {
            while (widgetCollection.Count > unitsPresent.Count)
            {
                Destroy(widgetCollection[0].gameObject);
                widgetCollection.RemoveAt(0);
            }
            
            while (widgetCollection.Count < unitsPresent.Count)
            {
                var newUnitWidget = Instantiate(unitInstanceWidgetPrefab, widgetContainer);
                widgetCollection.Add(newUnitWidget);
            }

            int i = 0;
            foreach (var unitInstance in unitsPresent)
            {
                widgetCollection[i].SetPresentationData(gameplayManager.GameState.Field, unitInstance);
                ++i;
            }
        }

        private void OnUnitAdded(UnitInstance unitInstance)
        {
            var newUnitWidget = Instantiate(unitInstanceWidgetPrefab, unitInstance.UnitCardTemplate.Ownership == Ownership.Player ? 
                                                                playerUnitWidgetsContainer : enemyUnitWidgetsContainer);
            newUnitWidget.SetPresentationData(gameplayManager.GameState.Field, unitInstance);
            (unitInstance.UnitCardTemplate.Ownership == Ownership.Player ? playerUnitWidgets : enemyUnitWidgets).Add(newUnitWidget);
            var newFloatingText = Instantiate(floatingTextPrefab);
            newFloatingText.SetText("Unit spawned");
            newFloatingText.gameObject.SetActive(false);
            newUnitWidget.gameObject.SetActive(false);
            
            presentationManager.AddPresentationTask(new PresentationTask
            (() =>
                {
                    newFloatingText.AttachTo(newUnitWidget.RectTransform);
                    newUnitWidget.gameObject.SetActive(true);
                    newFloatingText.gameObject.SetActive(true);
                },
                (float deltaTime) => { },
                () => { },
                () => newFloatingText == null
            ));
        }
        
        private void OnUnitWounded(UnitInstance unitInstance)
        {
            var unitWidget = (unitInstance.UnitCardTemplate.Ownership == Ownership.Player ? playerUnitWidgets : enemyUnitWidgets)
                                .FirstOrDefault(widget => widget.UnitInstance == unitInstance);
            var newFloatingText = Instantiate(floatingTextPrefab);
            newFloatingText.SetText("Wounded");
            newFloatingText.gameObject.SetActive(false);
            presentationManager.AddPresentationTask(new PresentationTask
            (() =>
                {
                    newFloatingText.AttachTo(unitWidget.RectTransform);
                    newFloatingText.gameObject.SetActive(true);
                },
                (float deltaTime) => { },
                () => { unitWidget.SetPresentationData(gameplayManager.GameState.Field, unitInstance); },
                () => newFloatingText == null
            ));
        }
        
        private void OnUnitKilled(UnitInstance unitInstance)
        {
            var newFloatingText = Instantiate(floatingTextPrefab);
            newFloatingText.gameObject.SetActive(false);
            newFloatingText.SetText("Killed");
            presentationManager.AddPresentationTask(new PresentationTask
            (() =>
                {
                    newFloatingText.gameObject.SetActive(true);
                    if (unitInstance.UnitCardTemplate.Ownership == Ownership.Player)
                    {
                        var unitWidget = playerUnitWidgets.FirstOrDefault(widget => widget.UnitInstance == unitInstance);
                        newFloatingText.transform.position = unitWidget.transform.position;
                        playerUnitWidgets.Remove(unitWidget);
                        Destroy(unitWidget.gameObject);
                    }
                    else
                    {
                        var unitWidget = enemyUnitWidgets.FirstOrDefault(widget => widget.UnitInstance == unitInstance);
                        newFloatingText.transform.position = unitWidget.transform.position;
                        enemyUnitWidgets.Remove(unitWidget);
                        Destroy(unitWidget.gameObject);
                    }
                },
                (float deltaTime) => { },
                () => { },
                () => newFloatingText == null)
            );
        }
    }
}