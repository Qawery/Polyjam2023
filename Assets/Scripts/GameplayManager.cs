using UnityEngine;

namespace Polyjam2023
{
    public class GameplayManager : MonoBehaviour
    {
        public GameWorld GameWorld { get; private set; } = new ();
    }
}
