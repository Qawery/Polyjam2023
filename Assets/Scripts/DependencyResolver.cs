using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace Polyjam2023
{
    public class DependencyResolver : MonoBehaviour
    {
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private PresentationManager presentationManager;
        [SerializeField] private CardLibrary cardLibrary;
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private CardWidget cardWidgetPrefab;
        [SerializeField] private UnitInstanceWidget unitInstanceWidgetPrefab;
        [SerializeField] private MenuPanelManager pauseMenu;
        [SerializeField] private FloatingText floatingTextPrefab;
        [SerializeField] private RectTransform defaultFloatingTextParent;
        
        public GameplayManager GameplayManager => gameplayManager;
        public PresentationManager PresentationManager => presentationManager;
        public CardLibrary CardLibrary => cardLibrary;
        public GameSettings GameSettings => gameSettings;
        public CardWidget CardWidgetPrefab => cardWidgetPrefab;
        public UnitInstanceWidget UnitInstanceWidgetPrefab => unitInstanceWidgetPrefab;
        public MenuPanelManager PauseMenu => pauseMenu;
        public FloatingText FloatingTextPrefab => floatingTextPrefab;
        public RectTransform DefaultFloatingTextParent => defaultFloatingTextParent;

        private void Awake()
        {
            Assert.IsNotNull(gameplayManager);
            Assert.IsNotNull(presentationManager);
            Assert.IsNotNull(cardLibrary);
            Assert.IsNotNull(gameSettings);
            Assert.IsNotNull(cardWidgetPrefab);
            Assert.IsNotNull(unitInstanceWidgetPrefab);
            Assert.IsNotNull(pauseMenu);
            Assert.IsNotNull(floatingTextPrefab);
            Assert.IsNotNull(defaultFloatingTextParent);
        }

        private void OnDestroy()
        {
            gameplayManager = null;
            presentationManager = null;
            cardLibrary = null;
            gameSettings = null;
            cardWidgetPrefab = null;
            unitInstanceWidgetPrefab = null;
            pauseMenu = null;
            floatingTextPrefab = null;
            defaultFloatingTextParent = null;
        }
    }
}
