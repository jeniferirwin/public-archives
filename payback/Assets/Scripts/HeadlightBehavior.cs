using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlightBehavior : MonoBehaviour
{
    void Start()
    {
        var settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
        if (settings.Difficulty < 3) gameObject.SetActive(false);
    }
}
