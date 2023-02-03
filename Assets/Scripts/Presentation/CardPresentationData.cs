using UnityEngine;

namespace Polyjam2023
{
    public class CardPresentationData
    {
        public readonly string name;
        public readonly Sprite image;
        public readonly string description;

        public CardPresentationData(string name, Sprite image, string description)
        {
            this.name = name;
            this.image = image;
            this.description = description;
        }
    }
}
