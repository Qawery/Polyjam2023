using UnityEngine;

namespace Polyjam2023
{
    public abstract class CardLogic : ScriptableObject
    {
        public abstract string CardName { get; }

        public abstract void PlayCard(GameState gameState);
    }
}
