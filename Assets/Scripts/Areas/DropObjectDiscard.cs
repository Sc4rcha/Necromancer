using UnityEngine;

public class DropObjectDiscard : DropObject
{
    public AudioSource SFXCardDrop;

    private PileDiscard discardPile;

    public void Setup(PileDiscard discardPile) 
    {
        this.discardPile = discardPile;
    }

    public override void DropCard(CardObjectMove card)
    {
        base.DropCard(card);

        card.gameObject.SetActive(false);
        discardPile.DiscardCard(card);

        // play sfx
        SFXCardDrop.Play();

        // check game over
        ManagerGameplay.Instance.CheckGameOver();
        ManagerGameplay.Instance.AddCardToScore(card.CardReference);
    }
}
