using System.Collections.Generic;
using UnityEngine;

public class CollectionCards : MonoBehaviour
{
    public GameInfo.SUIT Suit;
    public CardObjectCollection[] Cards;

    [Header("Selected")]
    public GameObject Selected;
    public CardObject CardSelected;
    public TMPro.TMP_Text CardText;

    public void Setup() 
    {
        for (int i = 0; i < Cards.Length; i++)
            Cards[i].Setup(this);

        SetupCards();

        gameObject.SetActive(false);
    }

    public virtual void Show(bool isShow) 
    {
        gameObject.SetActive(isShow);
        Selected.SetActive(false);

        if (isShow)
            SetupCards();
    }

    public void Select(GameInfo.Card card)
    {
        Selected.SetActive(true);
        CardSelected.SetCardInfo(card);
        CardSelected.CardImage.sprite = card.SpriteHand;
        CardText.text = GameManager.Instance.CardsReference.GetCardById(card.Id).CardStory;
    }
    private void SetupCards() 
    {
        SoCardList cardsReference = GameManager.Instance.CardsReference;
        List<SoCard> suitCards = new List<SoCard>();
        SAVE.GameSave save = GameManager.Instance.SaveData;

        foreach (var card in cardsReference.Cards)
        {
            if (card.CardSuit == Suit)
                suitCards.Add(card);
        }

        for (int i = 0; i < suitCards.Count; i++)
        {
            if (save.CardsUnlocked.Contains(suitCards[i].Id))
            {
                Cards[i].SetCardInfo(suitCards[i].GetCard());
                Cards[i].ShowHide(true);
            }
        }
    }
}
