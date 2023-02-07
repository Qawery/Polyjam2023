using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class PresentationManager : MonoBehaviour
    {
        private List<PresentationTask> ongoingTasks = new ();
        private PresentationTask currentTask = null;
        
        private GameplayManager gameplayManager;
        private MenuPanelManager pauseMenu;
        private FloatingText floatingTextPrefab;

        public System.Action OnBusyChanged;
        
        public bool IsBlocked => currentTask != null || ongoingTasks.Count > 0;
        
        private void Awake()
        {
            var dependencyResolver = FindObjectOfType<DependencyResolver>();
            gameplayManager = dependencyResolver.GameplayManager;
            Assert.IsNotNull(gameplayManager);
            pauseMenu = dependencyResolver.PauseMenu;
            Assert.IsNotNull(pauseMenu);
            floatingTextPrefab = dependencyResolver.FloatingTextPrefab;
            Assert.IsNotNull(floatingTextPrefab);
            
            CardWidget.OnCardWidgetClicked += OnCardWidgetClicked;
            UnitInstanceWidget.OnUnitInstanceWidgetClicked += OnUnitInstanceWidgetClicked;
            
            gameplayManager.OnPlayerTurnStarted += () =>
            {
                var newFloatingText = Instantiate(floatingTextPrefab);
                newFloatingText.SetText("Player phase");
                newFloatingText.gameObject.SetActive(false);
                AddPresentationTask(new PresentationTask
                (() =>
                    {
                        newFloatingText.gameObject.SetActive(true);
                    },
                    (float deltaTime) => { },
                    () => { },
                    () => newFloatingText == null
                ));
            };
            
            gameplayManager.OnPlayerTurnEnded += () =>
            {
                var newFloatingText = Instantiate(floatingTextPrefab);
                newFloatingText.SetText("Combat phase");
                newFloatingText.gameObject.SetActive(false);
                AddPresentationTask(new PresentationTask
                (() =>
                    {
                        newFloatingText.gameObject.SetActive(true);
                    },
                    (float deltaTime) => { },
                    () => { },
                    () => newFloatingText == null
                ));
            };
        }

        private void OnDestroy()
        {
            CardWidget.OnCardWidgetClicked -= OnCardWidgetClicked;
            UnitInstanceWidget.OnUnitInstanceWidgetClicked -= OnUnitInstanceWidgetClicked;
            gameplayManager = null;
            pauseMenu = null;
        }

        private void Update()
        {
            if (currentTask != null)
            {
                if (!currentTask.IsFinished())
                {
                    currentTask.Update(Time.deltaTime);
                    return;
                }
                
                currentTask.Finish();
            }

            if (ongoingTasks.Count > 0)
            {
                currentTask = ongoingTasks[0];
                ongoingTasks.RemoveAt(0);
                currentTask.Start();
                OnBusyChanged?.Invoke();
            }
            else
            {
                currentTask = null;
                OnBusyChanged?.Invoke();
            }
        }

        public void AddPresentationTask(PresentationTask presentationTask)
        {
            ongoingTasks.Add(presentationTask);
            OnBusyChanged?.Invoke();
        }

        private void OnCardWidgetClicked(CardWidget cardWidget)
        {
            if (IsBlocked)
            {
                return;
            }
            gameplayManager.PlayPlayerCard(cardWidget.CardName);
        }
        
        private void OnUnitInstanceWidgetClicked(UnitInstanceWidget unitInstanceWidget)
        {
            if (IsBlocked)
            {
                return;
            }
        }

        public void EndPlayerTurn()
        {
            if (IsBlocked)
            {
                return;
            }
            gameplayManager.EndPlayerTurn();
        }

        public void ShowPauseMenu()
        {
            if (IsBlocked)
            {
                return;
            }
            pauseMenu.gameObject.SetActive(true);
        }
    }
}