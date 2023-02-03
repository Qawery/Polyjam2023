using UnityEngine;

namespace Polyjam2023
{
    /*
     * Helper class for displaying what code version is running.
     * While working on code increment number in message to make sure that Unity is running recompiled code.
     * Logs error because it should be turned off when not working on code.
     */
    public class CodeVersionHelper : MonoBehaviour
    {
        private void Start()
        {
            Debug.LogError("CodeVersionHelper Activated! Code version: 1");
        }
    }
}