using UnityEngine;
using UnityEngine.UI;

public class CardObjectStory : CardObject
{
    public AudioSource SFXInteract;

    private StoryManager story;

    public override void Setup()
    {
        base.Setup();

        story = StoryManager.Instance;
    }

    public void ButtonSelect() 
    {
        story.CardSelect(CardReference);

        Shake.StartShake();
        SFXInteract.Play();
    }

    public override void SetCardInfo(GameInfo.Card cardReference)
    {
        base.SetCardInfo(cardReference);

        Holder.GetComponent<CardTilt>().ForceTilt(Vector2.right);
    }
}
