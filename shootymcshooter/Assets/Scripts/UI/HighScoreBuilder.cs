using UnityEngine;
using System.Collections.Generic;
using Shooty.Core;

namespace Shooty.UI
{
    public class HighScoreBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject highScorePrefab;
        [SerializeField] private GameObject noticePrefab;
        [SerializeField] private GameObject doneButton;
        
        private List<GameObject> created = new List<GameObject>();
        
        private void OnEnable()
        {
            if (Game.Data.HighScores.Slots.Count == 0)
            {
                created.Add(GameObject.Instantiate(noticePrefab, Vector3.zero, Quaternion.identity, gameObject.transform));
                doneButton.transform.SetAsLastSibling();
                return;
            }
            var highScores = Game.Data.HighScores;
            for (int i = 0; i < highScores.Slots.Count && i < 5; i++)
            {
                var scoreLine = GameObject.Instantiate(highScorePrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
                var scoreControl = scoreLine.GetComponent<HighScoreSlotControl>();
                scoreControl.SetNameText(highScores.Slots[i].PlayerName);
                scoreControl.SetScoreText(highScores.Slots[i].FinalScore.ToString());
                created.Add(scoreLine);
                scoreLine.transform.SetSiblingIndex(i);
                doneButton.transform.SetAsLastSibling();
            }
        }
        
        private void OnDisable()
        {
            foreach (var obj in created)
            {
                Destroy(obj);
            }
        }
    }
}