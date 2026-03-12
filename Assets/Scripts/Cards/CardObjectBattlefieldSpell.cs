using UnityEngine;

public class CardObjectBattlefieldSpell : CardObjectBattlefield
{
    public void TakeCard() 
    {
        // add card to hand
        ManagerGameplay.Instance.GroupHand.CardAdd(CardReference);

        // deactivate card
        ShowHide(false);

        // visual
        ManagerGameplay.Instance.CardVisuals.MoveCardPickup(GetComponent<RectTransform>(), ManagerGameplay.Instance.GroupHand);

        // clear slot
        battlefieldSlot.ClearSlot();
    }
}
