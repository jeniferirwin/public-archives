using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void OnGameStart()
    {
        SceneManager.LoadScene("main",LoadSceneMode.Single);
    }
}
