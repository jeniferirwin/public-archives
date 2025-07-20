using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public Settings settings;
    public GameObject[] carPrefabs;
    private float cooldown;

    private void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0f)
        {
            cooldown = Random.Range(GetMinCooldown(), GetMaxCooldown());
            if (Random.Range(1,101) <= GetSpawnChance()) SpawnCar();
        }
    }
    
    private int GetSpawnChance()
    {
        switch (settings.Difficulty)
        {
            case 1: return 50;
            case 2: return 60;
            case 3: return 70;
            case 4: return 80;
            case 5: return 90;
            default: return 100;
        }
    }
    
    private float GetMinCooldown()
    {
        switch (settings.Difficulty)
        {
            case 1: return 3f;
            case 2: return 2.5f;
            case 3: return 2f;
            case 4: return 1.5f;
            case 5: return 1f;
            default: return 0.5f;
        }
    }
    
    private float GetMaxCooldown()
    {
        switch (settings.Difficulty)
        {
            case 1: return 6f;
            case 2: return 5f;
            case 3: return 4f;
            case 4: return 3f;
            case 5: return 2f;
            default: return 1f;
        }
    }
    
    private void SpawnCar()
    {
        var type = Random.Range(0, carPrefabs.Length);
        var newCar = GameObject.Instantiate(carPrefabs[type],transform.position,transform.rotation);
        newCar.SetActive(true);
    }
}
