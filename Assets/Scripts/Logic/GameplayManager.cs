using UnityEngine;

namespace Polyjam2023
{
    public class GameplayManager : MonoBehaviour
    {
        public GameState GameState { get; private set; } = new ();
    }
}
