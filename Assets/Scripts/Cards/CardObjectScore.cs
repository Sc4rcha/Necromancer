using UnityEngine;

public class CardObjectScore : CardObject
{
    [Header("Card Score")]
    public AudioSource SFXEnemy;
    public AudioSource SFXSpell;

    public override void SetCardInfo(GameInfo.Card cardReference)
    {
        base.SetCardInfo(cardReference);

        Shake.StartShake();

        if (CardReference.Suit == GameInfo.SUIT.Spades || CardReference.Suit == GameInfo.SUIT.Clubs)
            SFXEnemy.Play();
        else
            SFXSpell.Play();
    }
}
