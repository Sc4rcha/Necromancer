using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGroup : MonoBehaviour
{
    [Header("Project References")]
    public CardObjectMove CardPrefab;

    [Header ("Scene References")]
    public RectTransform CardHolder;

    public List<CardObjectMove> Cards { get; private set; }

    public virtual void Setup() 
    {
        Cards = new List<CardObjectMove>();
    }

    public CardObject GetLastCard() 
    {
        if (Cards.Count == 0) return null;

        return Cards[Cards.Count - 1];
    }

    public void Lock(bool isLock) 
    {
        foreach (CardObjectMove card in Cards)
            card.Lock(isLock);
    }

    public virtual void CardAdd(GameInfo.Card cardInfo) 
    {
        // Instantiate and setup card
        Cards.Add(Instantiate(CardPrefab, CardHolder));
        Cards[Cards.Count - 1].Setup();
        Cards[Cards.Count - 1].SetCardInfo(cardInfo);

        // set card card group 
        Cards[Cards.Count-1].CardGroup = this;
    }
    public virtual void CardRemove(CardObjectMove card) 
    {
        Cards.Remove(card);
    }
}
