using UnityEngine;

public class CardMovement : MonoBehaviour
{
    [Header("Scene References")]
    public Transform CanvasTop;


    public CardObjectMove CardPickedUp { get; private set; }
    public DropObject DropperHovering { get; private set; }


    public void CardPickup(CardObjectMove card)
    {
        CardPickedUp = card;

        CardPickedUp.transform.SetParent(CanvasTop);
    }
    public void CardDrop()
    {
        // do nothing if no card picked up
        if (CardPickedUp == null)
            return;

        // Card hovering over empty dropper
        if (DropperHovering != null)
        {
            // picked card drop
            CardPickedUp.DropDropper(DropperHovering);

            // hovering dropper card add
            DropperHovering.DropCard(CardPickedUp);
        }
        // Card hovering over nothing
        else
        {
            CardPickedUp.DropCancel();
        }

        DropperHovering = null;
        CardPickedUp = null;
    }

    public void DropperEnter(DropObject dropper)
    {
        DropperHovering = dropper;
    }
    public void DropperExit(DropObject dropper)
    {
        if (DropperHovering == dropper)
            DropperHovering = null;
    }

}
