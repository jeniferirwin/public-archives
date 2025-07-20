using UnityEngine;

public class UserVolume : MonoBehaviour
{
    public GameObject audioObject;
    public AudioSource source;
    public float volume;

    void Awake()
    {
        volume = 0.05f;
        source = audioObject.GetComponent<AudioSource>();
        source.volume = volume;
        OnVolumeChanged();
    }
    
    public void OnVolumeChanged()
    {
        source.volume = volume;
    }
    
    public void SetVolume(float value)
    {
        volume = value;
        OnVolumeChanged();
    }
}
