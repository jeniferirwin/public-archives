using UnityEngine;

public class GutsCleanup : MonoBehaviour
{
    private float ttl;
    
    void Start()
    {
        ttl = 15f;
    }

    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0)
        {
            GameObject.Destroy(gameObject);
            GameObject.Destroy(this);
        }
        
    }
}
