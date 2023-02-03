using UnityEngine;
using UnityEngine.SceneManagement;

namespace Polyjam2023
{
    public class LoadingScreen : MonoBehaviour
    {
        private static LoadingScreen instance;

        public static LoadingScreen Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<LoadingScreen>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public void LoadScene(string sceneName)
        {
            gameObject.SetActive(true);
            SceneManager.LoadScene(sceneName);
            gameObject.SetActive(false);
        }
    }
}