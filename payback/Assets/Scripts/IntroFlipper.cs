using UnityEngine;

public class IntroFlipper : MonoBehaviour
{
    public GameObject[] cards;
    private int currentCard;

    void Start()
    {
        currentCard = -1;
        NextCard();
    }

    public void NextCard()
    {
        currentCard++;
        foreach (var card in cards)
        {
            card.SetActive(false);
        }
        cards[currentCard].SetActive(true);
    }
}
