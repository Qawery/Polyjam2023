using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class DependencyResolver : MonoBehaviour
    {
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private CardLibrary cardLibrary;
        [SerializeField] private CardWidget cardWidgetPrefab;
        [SerializeField] private UnitInstanceWidget unitInstanceWidgetPrefab;
        
        public GameplayManager GameplayManager => gameplayManager;
        public InputManager InputManager => inputManager;
        public CardLibrary CardLibrary => cardLibrary;
        public CardWidget CardWidgetPrefab => cardWidgetPrefab;
        public UnitInstanceWidget UnitInstanceWidgetPrefab => unitInstanceWidgetPrefab;

        private void Awake()
        {
            Assert.IsNotNull(gameplayManager);
            Assert.IsNotNull(inputManager);
            Assert.IsNotNull(cardLibrary);
            Assert.IsNotNull(cardWidgetPrefab);
            Assert.IsNotNull(unitInstanceWidgetPrefab);
        }

        private void OnDestroy()
        {
            gameplayManager = null;
            inputManager = null;
            cardLibrary = null;
            cardWidgetPrefab = null;
            unitInstanceWidgetPrefab = null;
        }
    }
}
