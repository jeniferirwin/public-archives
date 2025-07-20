using UnityEngine;

namespace Shooty.Core
{
    public class Initialize : MonoBehaviour
    {
        [SerializeField] private Transform audioContainer;
        [SerializeField] private AudioClip backgroundMusic;

        private void Awake()
        {
            Game.Initialize(audioContainer, backgroundMusic);
        }
    }
}