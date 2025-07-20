using UnityEngine;
using Shooty.Core;

namespace Shooty
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private AudioClip shot;

        private void Awake()
        {
            var scale = 1.5f * RoundData.ScalePercentage;
            transform.localScale = new Vector3(scale, scale, scale);
        }

        public void GetShot()
        {
            RoundData.IncrementScore();
            Game.SFXPlayer.PlayOneShot(shot);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            RoundData.IncrementEscaped();
            Destroy(gameObject);
        }
    }
}
