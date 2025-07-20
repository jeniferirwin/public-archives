using UnityEngine;
using Shooty.Core;
using UnityEngine.UI;

namespace Shooty.UI
{
    public class NameInputFeedback : MonoBehaviour
    {
        [SerializeField] private GameObject[] invalidNotice;
        [SerializeField] private Button[] buttons;

        public void PerformValidation(string name)
        {
            if (NameValidation.IsValidAsPlayerName(name))
            {
                NameValidFeedback();
                Game.Data.Prefs.SetPlayerName(name);
                DataManagement.SaveDataToFile(Game.Data);
            }
            else
            {
                NameInvalidFeedback();
            }
        }
        
        public void NameInvalidFeedback()
        {
            foreach (var obj in invalidNotice)
            {
                obj.SetActive(true);
            }
            foreach (var button in buttons)
            {
                button.interactable = false;
            }
        }

        public void NameValidFeedback()
        {
            foreach (var obj in invalidNotice)
            {
                obj.SetActive(false);
            }
            foreach (var button in buttons)
            {
                button.interactable = true;
            }
        }
    }
}
