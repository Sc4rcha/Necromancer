using UnityEngine;
using static GameInfo;

public class DropObjectStory : DropObject
{
    public CardObjectMove CardReference;
    public Card CardInfo;

    private int order;
    private StoryManager story;

    public void Setup(StoryManager story, int order) 
    {
        this.story = story;
        this.order = order;

        CardReference.Setup();
        CardReference.ShowHide(false);
    }

    private void OnDisable()
    {
        // hide card reference on disable
        CardReference.ShowHide(false);
    }

    public override void DropCard(CardObjectMove card)
    {
        foreach(var dropObj in story.Droppers)
        {
            // if drop object is self skip
            if (dropObj == this) continue;

            // if drop object is the one the card is originating from set info
            if (dropObj.CardReference == card)
            {
                // save card info of the card being dropped
                Card cardInfo = card.CardReference;

                // set card info of other dropper
                if (CardInfo != null)
                    dropObj.SelectCard(CardInfo);
                else
                    dropObj.DeselectCard();

                // set card info of self
                SelectCard(cardInfo);
            }
        }

        base.DropCard(card);

        // save game
        GameManager.Instance.Save();
    }

    public void ButtonSelectorOpen() 
    {
        story.CardSelectorOpen(this);
    }

    public void SelectCard(Card card) 
    {
        if (card == null)
            return;

        CardInfo = card;

        CardReference.SetCardInfo(card);
        CardReference.ShowHide(true);

        CardReference.transform.position = transform.position;
        CardReference.transform.SetParent(story.UncompleteHolderCards);
        CardReference.Shake.StartShake();

        story.SaveSelectedCard(card.Id, order);
        story.CardSelect(card);
    }
    public void DeselectCard() 
    {
        CardInfo = null;

        CardReference.transform.position = transform.position;
        CardReference.transform.SetParent(story.UncompleteHolderCards);

        CardReference.ShowHide(false);

        story.SaveSelectedCard(null, order);
    }
}
