using System.Linq;
using UnityEngine;

public class CollectionCardsStory : CollectionCards
{
    public void ButtonSelect() 
    {
        StoryManager.Instance.CardSelectorSelect(CardSelected.CardReference);
        Show(false);
    }

    protected override void SetupCards()
    {
        base.SetupCards();

        foreach (var card in Cards)
        {
            card.CardButton.interactable = CheckCardMarked(card.CardReference);
        }
    }


    private bool CheckCardMarked(GameInfo.Card card)
    {
        foreach (var storysegment in save.StorySegments)
        {
            if (storysegment.IsComplete && storysegment.cardCodes.Contains(card.Id))
                return false;
        }

        return true;
    }
}
