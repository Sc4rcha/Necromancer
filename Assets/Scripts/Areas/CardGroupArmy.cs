using UnityEngine;

public class CardGroupArmy : CardGroup
{
    public override void CardAdd(GameInfo.Card cardInfo)
    {
        base.CardAdd(cardInfo);

        // lock second last card
        if (Cards.Count > 1)
            Cards[Cards.Count - 2].Lock(true);
    }

    public override void CardRemove(CardObjectMove card)
    {
        base.CardRemove(card);

        // unlock last card
        if (Cards.Count > 0)
            Cards[Cards.Count - 1].Lock(false);
    }
}
