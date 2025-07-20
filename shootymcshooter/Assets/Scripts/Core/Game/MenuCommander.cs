using UnityEngine;

namespace Shooty.UI
{
    public class MenuCommander : MonoBehaviour
    {
        private static MenuCommander _instance;
        public static MenuCommander Instance { get { return _instance; }}

        [SerializeField] private GameObject pagesContainer;
        [SerializeField] private GameObject playPage;
        [SerializeField] private GameObject volumePage;
        [SerializeField] private GameObject highScorePage;
        [SerializeField] private GameObject dataManagementPage;
        [SerializeField] private GameObject[] pageArray;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }

            Instance.pagesContainer.SetActive(true);
            CloseAll();
            Instance.volumePage.SetActive(true);
        }
        
        private static void CloseAll()
        {
            Instance.pagesContainer.SetActive(true);
            foreach (var page in Instance.pageArray)
            {
                page.SetActive(false);
            }
        }

        public static void FlipVolume()
        {
            CloseAll();
            Instance.volumePage.SetActive(true);
        }

        public static void FlipDataManagement()
        {
            CloseAll();
            Instance.dataManagementPage.SetActive(true);
        }

        public static void FlipPlay()
        {
            CloseAll();
            Instance.playPage.SetActive(true);
        }

        public static void FlipHighScore()
        {
            CloseAll();
            Instance.highScorePage.SetActive(true);
        }
    }
}