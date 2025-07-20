using System.Collections;
using UnityEngine;
using TMPro;
using Shooty.Core;
using UnityEngine.InputSystem;

namespace Shooty.Gameplay
{
    public class StartTimer : MonoBehaviour
    {
        [SerializeField] private int timer;
        [SerializeField] private TMP_Text countdownText;
        [SerializeField] private GameObject countdownObject;
        [SerializeField] private GameObject[] delayedActivation;
        [SerializeField] private PlayerInput playerInput;

        private void OnEnable()
        {
            RoundData.ResetScore();
            countdownObject.SetActive(true);
            StartCoroutine(DelayedStart());
        }

        private IEnumerator DelayedStart()
        {
            int countdown = timer;
            while (countdown > 0)
            {
                countdownText.text = countdown.ToString();
                yield return new WaitForSeconds(1);
                countdown--;
            }
            countdownObject.SetActive(false);
            foreach (var obj in delayedActivation)
            {
                obj.SetActive(true);    
            }
            playerInput.ActivateInput();
        }
    }
}
