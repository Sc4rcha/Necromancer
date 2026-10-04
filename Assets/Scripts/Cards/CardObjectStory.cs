using UnityEngine;

public class CardObjectStory : CardObject
{
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
    }

    public override void SetCardInfo(GameInfo.Card cardReference)
    {
        base.SetCardInfo(cardReference);
    }
}
