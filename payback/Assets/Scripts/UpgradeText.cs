using UnityEngine;

public class UpgradeText : MonoBehaviour
{
    private float ttl;

    void Start()
    {
        ttl = 4f;
    }

    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
