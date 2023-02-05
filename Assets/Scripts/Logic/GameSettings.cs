using UnityEngine;

namespace Polyjam2023
{
    [CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public Difficulty difficulty = Difficulty.Medium;
    }

    public enum Difficulty
    {
        Easy = 0, 
        Medium, 
        Hard
    }
}
