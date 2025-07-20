using UnityEngine;

namespace Shooty.Core
{
    public class MakeIndestructible : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}