using UnityEngine;
using UnityEngine.EventSystems;

public class DropObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardObjectMove Card { get; private set; }

    private bool isLocked;

    public virtual void DropCard(CardObjectMove card)
    {
        Card = card;
    }


    public void Lock (bool isLocked)
    {
        this.isLocked = isLocked;
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isLocked) return;

        ManagerGameplay.Instance.CardMovement.DropperEnter(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isLocked) return;

        ManagerGameplay.Instance.CardMovement.DropperExit(this);
    }


}
