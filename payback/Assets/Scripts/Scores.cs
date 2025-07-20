using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    public TMP_Text difficulty;
    public TMP_Text squashed;
    public TMP_Text saved;
    public TMP_Text destroyed;
    
    private Settings settings;
    private float pollRate;
    private float poll;
    
    void Start()
    {
        pollRate = 0.1f;
        poll = pollRate;
        settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
    }

    void Update()
    {
        difficulty.text = $"Difficulty: {settings.Difficulty}";
        squashed.text = $"Frogs Squashed: {settings.FrogsSplatted}";
        saved.text = $"Frogs Saved: {settings.FrogsSaved}";
        destroyed.text = $"Vehicles Destroyed: {settings.CarsDestroyed}";
    }
}
