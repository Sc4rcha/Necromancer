using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class CollectionProgress : MonoBehaviour
{
    public GameInfo.SUIT Suit;
    private Image progressBar;

    private void Start()
    {
        progressBar = GetComponent<Image>();

        List<SoCard> cardReferences = GameManager.Instance.CardsReference.Cards.Where(item => item.CardSuit == Suit).ToList();
        int unlocked = 0;

        foreach (var unlockedCard in GameManager.Instance.SaveData.CardsUnlocked)
        {
            if (cardReferences.Find(x => x.Id == unlockedCard)) unlocked++;
        }

        progressBar.fillAmount = (float)unlocked / (float)cardReferences.Count;
    }
}
