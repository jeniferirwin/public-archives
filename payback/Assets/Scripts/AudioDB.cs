using UnityEngine;

public class AudioDB : MonoBehaviour
{
    public AudioClip rifle;
    public AudioClip shotgun;
    public AudioClip machine;
    public AudioClip grenade;
    public AudioClip rocket;
    public AudioClip drive;
    public AudioClip upgrade;
    public AudioClip explode;
    public AudioClip squish;
    public AudioClip helicopter;

    [SerializeField] private GameObject audioObject;
    private AudioSource source;

    void Start()
    {
        source = audioObject.GetComponent<AudioSource>();
    }
    
    public void PlayRifle() => source.PlayOneShot(rifle);
    public void PlayShotgun() => source.PlayOneShot(shotgun);
    public void PlayMachineGun() => source.PlayOneShot(machine);
    public void PlayGrenade() => source.PlayOneShot(grenade);
    public void PlayRocket() => source.PlayOneShot(rocket);
    public void PlayUpgrade() => source.PlayOneShot(upgrade);
    public void PlayExplosion() => source.PlayOneShot(explode);
    public void PlaySquish() => source.PlayOneShot(squish);
}
