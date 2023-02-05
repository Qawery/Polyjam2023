using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class BusyCursor : MonoBehaviour
    {
        [SerializeField] private Texture2D normalCursor;
        [SerializeField] private Texture2D busyCursor;
        private PresentationManager presentationManager;
        
        private void Awake()
        {
            Assert.IsNotNull(normalCursor);
            Assert.IsNotNull(busyCursor);
            presentationManager = FindObjectOfType<DependencyResolver>().PresentationManager;
            Assert.IsNotNull(presentationManager);
            presentationManager.OnBusyChanged += UpdateCursor;
        }

        private void OnDestroy()
        {
            presentationManager.OnBusyChanged -= UpdateCursor;
            presentationManager = null;
            Cursor.SetCursor(normalCursor, new Vector2(0, 1), CursorMode.Auto);
            normalCursor = null;
            busyCursor = null;
        }

        private void UpdateCursor()
        {
            Cursor.SetCursor(presentationManager.IsBlocked ? busyCursor : normalCursor, new Vector2(0, 1), CursorMode.Auto);
        }
    }
}
