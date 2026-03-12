using UnityEngine;
using UnityEngine.UI;
using static GameInfo;

public class CardObject : MonoBehaviour
{
    public GameObject Holder;
    public Image CardImage;
    public CardShake Shake;

    public Card CardReference { get; private set; }

    public virtual void Setup() 
    {

    }

    public virtual void ShowHide(bool isShow) 
    {
        Holder.SetActive(isShow);
    }

    public virtual void SetCardInfo(Card cardReference)
    {
        // set card info
        CardReference = cardReference;

        // set card image
        CardImage.sprite = CardReference.Sprite;
    }
}
