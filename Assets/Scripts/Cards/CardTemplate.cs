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
    public abstract class CardTemplate : ScriptableObject
    {
        [SerializeField] private string cardName;
        [SerializeField] private Sprite image;
        
        public string CardName => cardName;
        public Sprite Image => image;
        public abstract string EffectDescription { get; }
        
        public abstract bool TryPlayCard(GameState gameState);
    }
}
