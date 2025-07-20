using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shooty.Core
{
    public class SceneLoader : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(FrameWait());
        }
        
        private IEnumerator FrameWait()
        {
            yield return new WaitForEndOfFrame();
            SceneManager.LoadScene("Main");
        }
    }
}
