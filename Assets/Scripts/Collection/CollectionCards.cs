using System.Collections.Generic;
using UnityEngine;

public class CollectionCards : MonoBehaviour
{
    public GameInfo.SUIT Suit;
    public CardObjectCollection[] Cards;

    [Header("Selected")]
    public GameObject Selected;
    public CardObject CardSelected;
    [Space]
    public GameObject DividerTop;
    public GameObject DividerBot;
    public StoryText Text;


    protected List<SoCard> suitCards = new List<SoCard>();
    protected SAVE.GameSave save;

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
        {
            SetupCards();
            GameManager.Instance.Post.ChangeMoveStrength(FXPost.MovementStrength.High);
        }
        else
            GameManager.Instance.Post.ChangeMoveStrength(FXPost.MovementStrength.Low);
    }

    public void Select(GameInfo.Card card)
    {
        Selected.SetActive(true);
        CardSelected.SetCardInfo(card);
        CardSelected.CardImage.sprite = card.SpriteHand;

        SoCard cardObject = GameManager.Instance.CardsReference.GetCardById(card.Id);

        DividerTop.SetActive(cardObject.Placement == SoCard.ChapterPlacement.Start);
        DividerBot.SetActive(cardObject.Placement == SoCard.ChapterPlacement.End);

        Text.SetFormattedText (cardObject.CardStory);
    }
    protected virtual void SetupCards() 
    {
        SoCardList cardsReference = GameManager.Instance.CardsReference;
        suitCards = new List<SoCard>();
        save = GameManager.Instance.SaveData;

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
