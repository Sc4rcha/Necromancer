using UnityEngine;

public class PileDiscard : Pile
{
    [Header ("Discard")]
    public CardObject Card;
    public DropObjectDiscard Dropper;

    public override void Setup() 
    {
        base.Setup();
        // setup elements
        Dropper.Setup(this);
        Dropper.gameObject.SetActive(false);

        // deactivate top card
        Card.Setup();
        Card.ShowHide(false);

        CardObjectBot.SetActive(false);
    }

    public void LockDropper(bool isLocked)
    {
        Dropper.gameObject.SetActive(!isLocked);
    }

    public void DiscardCard(CardObject card)
    {
        // add card to discard list
        Cards.Add(card.CardReference);
        Card.SetCardInfo(card.CardReference);
        Card.CardImage.sprite = card.CardImage.sprite;

        // visual card
        ManagerGameplay.Instance.CardVisuals.MoveCardDiscard(card, Card);
        ManagerGameplay.Instance.RevealBattlefield();
    }
}
