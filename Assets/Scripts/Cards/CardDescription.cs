using UnityEngine;

namespace Polyjam2023
{
    /*
     * Class representing card in game.
     * Contains its name (serves also as unique ID), presentation elements (image, effects description and fluff description)
     * and logic (play effect). 
     * TODO: Make nice inspector window with:
     * -Image preview.
     * -Effect description preview.
     */
    public abstract class CardDescription : ScriptableObject
    {
        [SerializeField] private string cardName;
        [SerializeField] private Sprite image;
        [SerializeField] private string fluffDescription;
        
        public string CardName => cardName;
        public Sprite Image => image;
        public abstract string EffectDescription { get; }
        public string FluffDescription => fluffDescription;
        
        public abstract bool TryPlayCard(GameState gameState);
    }
}
