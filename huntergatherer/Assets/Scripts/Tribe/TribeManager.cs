using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeManager : MonoBehaviour
{
    private static TribeManager _instance;
    public static TribeManager Instance { get { return _instance; } }
    
    public static bool campPlaced;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
