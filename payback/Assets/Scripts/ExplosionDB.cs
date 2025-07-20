using UnityEngine;

public class ExplosionDB : MonoBehaviour
{
    public GameObject[] explosions;
    
    public GameObject GetRandomExplosion()
    {
        var rand = Random.Range(0,explosions.Length);
        return explosions[rand];
    }
}
