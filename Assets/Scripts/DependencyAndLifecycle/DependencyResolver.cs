using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class DependencyResolver : MonoBehaviour
    {
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private PresentationManager presentationManager;
        [SerializeField] private CardLibrary cardLibrary;
        [SerializeField] private CardWidget cardWidgetPrefab;
        [SerializeField] private UnitInstanceWidget unitInstanceWidgetPrefab;
        [SerializeField] private MenuPanelManager pauseMenu;
        [SerializeField] private FloatingText floatingTextPrefab;
        
        public GameplayManager GameplayManager => gameplayManager;
        public PresentationManager PresentationManager => presentationManager;
        public CardLibrary CardLibrary => cardLibrary;
        public CardWidget CardWidgetPrefab => cardWidgetPrefab;
        public UnitInstanceWidget UnitInstanceWidgetPrefab => unitInstanceWidgetPrefab;
        public MenuPanelManager PauseMenu => pauseMenu;
        public FloatingText FloatingTextPrefab => floatingTextPrefab;

        private void Awake()
        {
            Assert.IsNotNull(gameplayManager);
            Assert.IsNotNull(presentationManager);
            Assert.IsNotNull(cardLibrary);
            Assert.IsNotNull(cardWidgetPrefab);
            Assert.IsNotNull(unitInstanceWidgetPrefab);
            Assert.IsNotNull(pauseMenu);
            Assert.IsNotNull(floatingTextPrefab);
        }

        private void OnDestroy()
        {
            gameplayManager = null;
            presentationManager = null;
            cardLibrary = null;
            cardWidgetPrefab = null;
            unitInstanceWidgetPrefab = null;
            pauseMenu = null;
            floatingTextPrefab = null;
        }
    }
}
