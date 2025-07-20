using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] private GameObject hp_one;
    [SerializeField] private GameObject hp_two;
    [SerializeField] private GameObject hp_three;

    public void SetHP(int hp)
    {
        switch (hp)
        {
            case 0:
                hp_one.SetActive(false);
                hp_two.SetActive(false);
                hp_three.SetActive(false);
                break;
            case 1:
                hp_one.SetActive(true);
                hp_two.SetActive(false);
                hp_three.SetActive(false);
                break;
            case 2:
                hp_one.SetActive(true);
                hp_two.SetActive(true);
                hp_three.SetActive(false);
                break;
            default:
                hp_one.SetActive(true);
                hp_two.SetActive(true);
                hp_three.SetActive(true);
                break;
        }
    }
}
