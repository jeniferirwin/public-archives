using UnityEngine;

namespace Shooty.Gameplay
{
    public class GameplayStarter : MonoBehaviour
    {
        [SerializeField] private GameObject[] enabledOnStart;
        [SerializeField] private GameObject[] disabledOnStart;

        private void OnEnable()
        {
            // This code is here to make 100% sure that all of
            // the required elements have their active status
            // properly set between rounds
            
            foreach (var obj in enabledOnStart)
            {
                obj.SetActive(true);
            }
            foreach (var obj in disabledOnStart)
            {
                obj.SetActive(false);
            }
        }
    }
}