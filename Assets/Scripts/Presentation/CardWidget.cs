using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class CardWidget : MonoBehaviour
    {
        [FormerlySerializedAs("name")] [SerializeField] private TMPro.TextMeshProUGUI nametext;
        [SerializeField] private Image image;
        [FormerlySerializedAs("description")] [SerializeField] private TMPro.TextMeshProUGUI descriptionText;
        
        private void Awake()
        {
            Assert.IsNotNull(nametext, $"Missing {nameof(nametext)} on {gameObject.name}.");
            Assert.IsNotNull(image, $"Missing {nameof(nametext)} on {gameObject.name}.");
            Assert.IsNotNull(descriptionText, $"Missing {nameof(nametext)} on {gameObject.name}.");
        }

        private void OnDestroy()
        {
            nametext = null;
            image = null;
            descriptionText = null;
        }

        public void SetPresentationData(CardPresentationData cardPresentationData)
        {
            nametext.text = cardPresentationData.name;
            image.sprite = cardPresentationData.image;
            descriptionText.text = cardPresentationData.description;
        }
    }
}
