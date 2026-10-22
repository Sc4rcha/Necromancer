using UnityEngine;
using UnityEngine.UI;

public class CardObjectCollection : CardObject
{
    public AudioSource SFXInteract;
    public Button CardButton;

    private CollectionCards collection;

    public void Setup (CollectionCards collection)
    {
        this.collection = collection;
        ShowHide(false);
    }

    public override void ShowHide(bool isShow)
    {
        base.ShowHide(isShow);
    }

    public void ButtonInput() 
    {
        collection.Select(CardReference);

        Shake.StartShake();
        SFXInteract.Play();
    }
}
