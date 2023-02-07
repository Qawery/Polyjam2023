using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Polyjam2023
{
    public class HowToPlayPanel : MonoBehaviour
    {
        [SerializeField] private GameObject pagesContainer;
        [SerializeField] private Button previousPageButton;
        [SerializeField] private Button nextPageButton;
        private List<GameObject> pages = new ();
        private int activePageIndex = 0;

        private void Awake()
        {
            Assert.IsNotNull(pagesContainer);
            Assert.IsNotNull(previousPageButton);
            Assert.IsNotNull(nextPageButton);

            for(int i = 0; i < pagesContainer.transform.childCount; ++i)
            {
                var page = pagesContainer.transform.GetChild(i).gameObject;
                pages.Add(page);
                page.SetActive(false);
            }
            
            previousPageButton.onClick.AddListener(() => { SwitchPage(activePageIndex - 1); });
            nextPageButton.onClick.AddListener(() => { SwitchPage(activePageIndex + 1); });
        }

        private void OnEnable()
        {
            SwitchPage(0);
        }
        
        private void OnDestroy()
        {
            previousPageButton.onClick.RemoveAllListeners();
            nextPageButton.onClick.RemoveAllListeners();
            pagesContainer = null;
            previousPageButton = null;
            nextPageButton = null;
        }

        private void SwitchPage(int newPageNumber)
        {
            if (newPageNumber < 0 || newPageNumber >= pages.Count)
            {
                return;
            }
            
            var pageScroll = pages[activePageIndex].GetComponent<ScrollRect>();
            if(pageScroll != null)
            {
                pageScroll.verticalNormalizedPosition = 1.0f;
            }
            pages[activePageIndex].SetActive(false);
            activePageIndex = newPageNumber;
            pages[activePageIndex].SetActive(true);
            
            previousPageButton.interactable = newPageNumber > 0;
            nextPageButton.interactable = newPageNumber < pages.Count - 1;
        }
    }
}