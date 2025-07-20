using System;
using System.Collections;
using Shooty.Core;
using UnityEngine;
using Shooty.UI;
using UnityEngine.InputSystem;

namespace Shooty.Gameplay
{
    public class GameOverTimer : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private GameObject gameOverText;
        [SerializeField] private GameObject gameplay;
        [SerializeField] private GameObject endInstructions;
        [SerializeField] private PlayerInput playerInput;

        private void Start()
        {
            RoundData.GameOver += GameOver;
        }

        public void DisableGameOverText()
        {
            gameOverText.SetActive(false);
            endInstructions.SetActive(true);
        }

        public void EnableGameOverText()
        {
            gameOverText.SetActive(true);
            endInstructions.SetActive(false);
        }

        private void GameOver(object sender, EventArgs e)
        {
            playerInput.DeactivateInput();
            gameOverText.SetActive(true);
            endInstructions.SetActive(false);
            StartCoroutine(GameOverDelay());
        }

        public void ForceGameOver(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                RoundData.ForceGameOver();
            }
        }

        private IEnumerator GameOverDelay()
        {
            Game.Data.HighScores.AddScore(Game.Data.Prefs.PlayerName, RoundData.Score);
            DataManagement.SaveDataToFile(Game.Data);
            EnableGameOverText();
            yield return new WaitForSeconds(delay);
            DisableGameOverText();
            MenuCommander.FlipHighScore();
            gameplay.SetActive(false);
        }
    }
}
