using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class UnitInstanceWidget : MonoBehaviour
    {
        private PresentationManager presentationManager;
        [SerializeField] private TMPro.TextMeshProUGUI cardName;
        [SerializeField] private Image cardImage;
        [SerializeField] private TMPro.TextMeshProUGUI attackValue;
        [SerializeField] private TMPro.TextMeshProUGUI initiativeValue;
        [SerializeField] private TMPro.TextMeshProUGUI healthValue;
        [SerializeField] private Button button;
        private UnitInstance unitInstance;

        public static event System.Action<UnitInstanceWidget> OnUnitInstanceWidgetClicked;
        
        public UnitInstance UnitInstance => unitInstance;
        private ICardLocation CardLocation { get; set; }
        
        private void Awake()
        {
            presentationManager = FindObjectOfType<DependencyResolver>().PresentationManager;
            Assert.IsNotNull(presentationManager);
            Assert.IsNotNull(cardName, $"Missing {nameof(cardName)} on {gameObject.name}.");
            Assert.IsNotNull(cardImage, $"Missing {nameof(cardImage)} on {gameObject.name}.");
            Assert.IsNotNull(attackValue, $"Missing {nameof(attackValue)} on {gameObject.name}.");
            Assert.IsNotNull(initiativeValue, $"Missing {nameof(initiativeValue)} on {gameObject.name}.");
            Assert.IsNotNull(healthValue, $"Missing {nameof(healthValue)} on {gameObject.name}.");
            Assert.IsNotNull(button, $"Missing {nameof(button)} on {gameObject.name}.");
            button.onClick.AddListener(() => { OnUnitInstanceWidgetClicked?.Invoke(this); } );
        }

        private void OnDestroy()
        {
            presentationManager = null;
            cardName = null;
            cardImage = null;
            attackValue = null;
            initiativeValue = null;
            healthValue = null;
            button.onClick.RemoveAllListeners();
            button = null;
            unitInstance = null;
        }

        public void SetPresentationData(ICardLocation cardLocation, UnitInstance unitInstance)
        {
            this.unitInstance = unitInstance;
            Assert.IsNotNull(cardLocation, $"Trying to assign null {nameof(cardLocation)} to {nameof(CardWidget)}.");
            CardLocation = cardLocation;
            
            cardName.text = unitInstance.UnitCardTemplate.CardName;
            cardImage.sprite = unitInstance.UnitCardTemplate.Image;
            attackValue.text = unitInstance.currentAttack.ToString();
            initiativeValue.text = unitInstance.currentInitiative.ToString();
            healthValue.text = unitInstance.currentHealth.ToString();
        }
    }
}