using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject UpgradeOne;
    public GameObject UpgradeTwo;
    public GameObject UpgradeThree;
    public GameObject UpgradeFour;
    public GameObject UpgradeFive;

    public GameObject sun;

    public int CarsDestroyed { get; private set; }
    public int FrogsSplatted { get; private set; }
    public int FrogsSaved { get; private set; }
    public int Difficulty { get; private set; }
    public int weaponType { get; private set; }

    void Awake()
    {
        CarsDestroyed = 0;
        FrogsSplatted = 0;
        FrogsSaved = 0;
        Difficulty = 1;
        UpgradeOne.SetActive(true);
    }
    
    public void OnCarDestroyed()
    {
        CarsDestroyed++;
        if (CarsDestroyed == 30) UpgradeTwo.SetActive(true);
        if (CarsDestroyed == 60) UpgradeThree.SetActive(true);
        if (CarsDestroyed == 90) UpgradeFour.SetActive(true);
        if (CarsDestroyed == 120) UpgradeFive.SetActive(true);
    }
    
    public void OnFrogSplatted()
    {
        FrogsSplatted++;
        if (FrogsSplatted >= 25)
        {
            SceneManager.LoadScene("dead");
        }
    }
    
    public void OnFrogSaved()
    {
        FrogsSaved++;
        if (FrogsSaved >= 50) SceneManager.LoadScene("win");
        Difficulty = Mathf.Max(1, (Mathf.Min(5, FrogsSaved / 10)));
        var light = sun.GetComponent<Light>();
        light.intensity = GetLightIntensity();
    }

    private float GetLightIntensity()
    {
        switch (Difficulty)
        {
            case 1: return 0.5f;
            case 2: return 0.4f;
            case 3: return 0.25f;
            case 4: return 0.1f;
            default: return 0.0f;
        }
    }
}