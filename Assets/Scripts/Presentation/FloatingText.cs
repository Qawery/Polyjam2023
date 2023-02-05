using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class FloatingText : MonoBehaviour
    {
        private RectTransform rectTransform;
        private RectTransform defaultParent;
        private TMPro.TextMeshProUGUI text;
        private float lifeTime = 0.75f;
        private const float speed = 40.0f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            Assert.IsNotNull(rectTransform);
            text = GetComponent<TMPro.TextMeshProUGUI>();
            Assert.IsNotNull(text);
            defaultParent = FindObjectOfType<DependencyResolver>().DefaultFloatingTextParent;
            Assert.IsNotNull(defaultParent);
            AttachTo(null);
        }

        private void Update()
        {
            lifeTime -= Time.deltaTime;
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + speed * Time.deltaTime, rectTransform.position.z);
            if (lifeTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            rectTransform = null;
            text = null;
        }

        public void AttachTo(RectTransform parent)
        {
            if (parent != null)
            {
                rectTransform.SetParent(parent);
                rectTransform.position = parent.position;
            }
            else
            {
                rectTransform.SetParent(defaultParent);
                rectTransform.position = defaultParent.position;
            }
        }
        
        public void SetText(string text)
        {
            this.text.text = text;
        }

        public void SetColor(Color color)
        {
            text.color = color;
        }
    }
}
