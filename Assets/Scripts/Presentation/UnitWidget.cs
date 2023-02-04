using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class UnitWidget : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI cardName;
        [SerializeField] private Image cardImage;

        
        private void Awake()
        {
            Assert.IsNotNull(cardName, $"Missing {nameof(cardName)} on {gameObject.name}.");
            Assert.IsNotNull(cardImage, $"Missing {nameof(cardImage)} on {gameObject.name}.");
        }

        private void OnDestroy()
        {
            cardName = null;
            cardImage = null;
        }

        public void SetPresentationData(UnitInstance unitInstance)
        {
            cardName.text = unitInstance.UnitCardTemplate.CardName;
            cardImage.sprite = unitInstance.UnitCardTemplate.Image;
        }
    }
}