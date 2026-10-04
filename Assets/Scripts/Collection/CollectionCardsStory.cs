using UnityEngine;

public class CollectionCardsStory : CollectionCards
{
    public void ButtonSelect() 
    {
        StoryManager.Instance.CardSelectorSelect(CardSelected.CardReference);
        Show(false);
    }
}
