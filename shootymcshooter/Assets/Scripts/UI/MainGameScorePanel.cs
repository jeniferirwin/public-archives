using System;
using UnityEngine;
using TMPro;
using Shooty.Core;

namespace Shooty.UI
{
    public class MainGameScorePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text hits;
        [SerializeField] private TMP_Text scale;
        [SerializeField] private TMP_Text escapes;
        

        private void OnEnable()
        {
            UpdateScore(null, EventArgs.Empty);
            RoundData.StatsChanged += UpdateScore;
        }
        
        private void OnDestroy()
        {
            RoundData.StatsChanged -= UpdateScore;
        }

        private void UpdateScore(object sender, EventArgs e)
        {
            var currentScale = 1.5f * RoundData.ScalePercentage;
            hits.text = $"Hits: {RoundData.Score}";
            scale.text = $"Scale: {currentScale.ToString("#.##")}";
            escapes.text = $"Escaped: {RoundData.Escaped}";
        }
    }
}
