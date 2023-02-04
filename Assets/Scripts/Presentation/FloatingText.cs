using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam2023
{
    public class FloatingText : MonoBehaviour
    {
        private RectTransform rectTransform;
        private TMPro.TextMeshProUGUI text;
        private float lifeTime = 1.0f;
        private const float speed = 20.0f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            Assert.IsNotNull(rectTransform);
            text = GetComponent<TMPro.TextMeshProUGUI>();
            Assert.IsNotNull(text);
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
