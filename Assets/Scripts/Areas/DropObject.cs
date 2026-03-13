using UnityEngine;
using UnityEngine.EventSystems;

public class DropObject : MonoBehaviour
{
    public CardObjectMove Card { get; private set; }

    public bool IsLocked;

    public virtual void DropCard(CardObjectMove card)
    {
        Card = card;
    }

    public void Lock (bool isLocked)
    {
        IsLocked = isLocked;
    }

}
