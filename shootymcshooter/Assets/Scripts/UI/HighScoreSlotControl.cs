using UnityEngine;
using TMPro;

namespace Shooty.UI
{
    public class HighScoreSlotControl : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text scoreText;

        public void SetNameText(string name)
        {
            nameText.text = name;
        }
        
        public void SetScoreText(string score)
        {
            scoreText.text = score;
        }
    }
}
