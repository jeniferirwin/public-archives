using UnityEngine;
using Shooty.Core;
using TMPro;

namespace Shooty.UI
{
    public class InputExistingName : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameText;

        private void Start()
        {
            if (NameValidation.IsValidAsPlayerName(Game.Data.Prefs.PlayerName))
            {
                nameText.text = Game.Data.Prefs.PlayerName;
            }
            else
            {
                nameText.text = Game.DEFAULT_PLAYER_NAME;
            }

            nameText.Select();
            nameText.DeactivateInputField();
        }
        
        public void NameDataErased()
        {
            nameText.text = Game.DEFAULT_PLAYER_NAME;
        }
    }
}
