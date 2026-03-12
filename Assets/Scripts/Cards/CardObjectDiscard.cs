using UnityEngine;
using static GameInfo;

public class CardObjectDiscard : CardObject
{
    public CardObject CardSecondFromTop;

    private PileDiscard discard;

    public override void Setup()
    {
        base.Setup();

        // set discard reference
        discard = ManagerGameplay.Instance.Discard;

        // setup second from top card
        CardSecondFromTop.Setup();
        CardSecondFromTop.ShowHide(false);
    }

    public void SetCards(CardObjectVisual visual)
    {
        // set second from top card
        CardSecondFromTop.SetCardInfo(visual.CardReference);
        CardSecondFromTop.CardImage.sprite = visual.CardImage.sprite;

        CardSecondFromTop.ShowHide(true);

        // set discard pile height
        discard.CardTransformUp.anchoredPosition = new Vector2(0, discard.Cards.Count * discard.CardWidth);
        discard.CardObjectBot.SetActive(true);
    }
}
