using UnityEngine;

namespace Polyjam2023
{
    public class CardPresentationData
    {
        public readonly string name;
        public readonly Sprite image;
        public readonly string effectDescription;
        public readonly string fluffDescription;

        public CardPresentationData(string name, Sprite image, string effectDescription, string fluffDescription)
        {
            this.name = name;
            this.image = image;
            this.effectDescription = effectDescription;
            this.fluffDescription = fluffDescription;
        }
    }
}
