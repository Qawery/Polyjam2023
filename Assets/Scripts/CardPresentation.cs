using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class CardPresentation : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI name;
        [SerializeField] private Image image;
        [SerializeField] private TMPro.TextMeshProUGUI description;
        
        private void Awake()
        {
            Assert.IsNotNull(name, $"Missing {nameof(name)} on {gameObject.name}.");
            Assert.IsNotNull(image, $"Missing {nameof(name)} on {gameObject.name}.");
            Assert.IsNotNull(description, $"Missing {nameof(name)} on {gameObject.name}.");
        }

        private void OnDestroy()
        {
            name = null;
            image = null;
            description = null;
        }

        public void SetPresentationData(CardPresentationData cardPresentationData)
        {
            name.text = cardPresentationData.name;
            image.sprite = cardPresentationData.image;
            description.text = cardPresentationData.description;
        }
    }
}
