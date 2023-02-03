using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class CardWidget : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI cardName;
        [SerializeField] private Image cardImage;
        [SerializeField] private TMPro.TextMeshProUGUI quantityText;
        [SerializeField] private TMPro.TextMeshProUGUI cardDescription;
        [SerializeField] private Button button;

        public event System.Action OnClicked;

        public string CardName => cardName.text;
        
        private void Awake()
        {
            Assert.IsNotNull(cardName, $"Missing {nameof(cardName)} on {gameObject.name}.");
            Assert.IsNotNull(cardImage, $"Missing {nameof(cardImage)} on {gameObject.name}.");
            Assert.IsNotNull(quantityText, $"Missing {nameof(quantityText)} on {gameObject.name}.");
            Assert.IsNotNull(cardDescription, $"Missing {nameof(cardDescription)} on {gameObject.name}.");
            Assert.IsNotNull(button, $"Missing {nameof(button)} on {gameObject.name}.");
            button.onClick.AddListener(() => { OnClicked?.Invoke(); });
        }

        private void OnDestroy()
        {
            cardName = null;
            cardImage = null;
            cardDescription = null;
            button.onClick.RemoveAllListeners();
            button = null;
        }

        public void SetPresentationData(CardPresentationData cardPresentationData, int quantity)
        {
            cardName.text = cardPresentationData.name;
            cardImage.sprite = cardPresentationData.image;
            Assert.IsTrue(quantity > 0, "Trying to write card quantity not above zero.");
            quantityText.text = quantity > 1 ? $"x{quantity.ToString()}" : "";
            cardDescription.text = cardPresentationData.description;
        }
    }
}
