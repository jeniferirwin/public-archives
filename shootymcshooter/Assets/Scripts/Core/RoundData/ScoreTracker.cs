using UnityEngine;

namespace Shooty.Core
{
    public class ScoreTracker : MonoBehaviour
    {
        private void Start()
        {
            RoundData.ResetScore();
        }
    }
}
