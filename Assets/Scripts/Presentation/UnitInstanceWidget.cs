using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class UnitInstanceWidget : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI cardName;
        [SerializeField] private Image cardImage;
        [SerializeField] private TMPro.TextMeshProUGUI attackValue;
        [SerializeField] private TMPro.TextMeshProUGUI initiativeValue;
        [SerializeField] private TMPro.TextMeshProUGUI healthValue;
        
        private void Awake()
        {
            Assert.IsNotNull(cardName, $"Missing {nameof(cardName)} on {gameObject.name}.");
            Assert.IsNotNull(cardImage, $"Missing {nameof(cardImage)} on {gameObject.name}.");
            Assert.IsNotNull(attackValue, $"Missing {nameof(attackValue)} on {gameObject.name}.");
            Assert.IsNotNull(initiativeValue, $"Missing {nameof(initiativeValue)} on {gameObject.name}.");
            Assert.IsNotNull(healthValue, $"Missing {nameof(healthValue)} on {gameObject.name}.");
        }

        private void OnDestroy()
        {
            cardName = null;
            cardImage = null;
            attackValue = null;
            initiativeValue = null;
            healthValue = null;
        }

        public void SetPresentationData(UnitInstance unitInstance)
        {
            cardName.text = unitInstance.UnitCardTemplate.CardName;
            cardImage.sprite = unitInstance.UnitCardTemplate.Image;
            attackValue.text = unitInstance.currentAttack.ToString();
            initiativeValue.text = unitInstance.currentInitiative.ToString();
            healthValue.text = unitInstance.currentHealth.ToString();
        }
    }
}