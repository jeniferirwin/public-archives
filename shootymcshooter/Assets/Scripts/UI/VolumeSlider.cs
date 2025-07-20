using UnityEngine;
using UnityEngine.UI;
using Shooty.Core;

namespace Shooty.UI
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private VolumeType type;
        [SerializeField] private Slider slider;
        [SerializeField] private AudioClip sfxTestClip;
        
        // small hack to keep the shot sound from playing unprompted if
        // we've erased prefs and then gone back into the volume panel
        private bool _started;

        private void OnEnable()
        {
            if (type == VolumeType.Music)
            {
                slider.value = Game.MusicPlayer.volume;
            }
            else if (type == VolumeType.SFX)
            {
                slider.value = Game.SFXPlayer.volume;
            }
            _started = true;
        }
        
        private void OnDisable()
        {
            _started = false;
        }

        public void SetSourceVolume(float value)
        {
            if (type == VolumeType.Music)
            {
                Game.MusicPlayer.volume = value;
                Game.Data.Prefs.MusicVolume = value;
            }
            else if (type == VolumeType.SFX)
            {
                Game.SFXPlayer.volume = value;
                Game.Data.Prefs.SFXVolume = value;
                if (Game.SFXPlayer.isPlaying || !_started) return;
                Game.SFXPlayer.PlayOneShot(sfxTestClip);
            }
        }
    }
}
