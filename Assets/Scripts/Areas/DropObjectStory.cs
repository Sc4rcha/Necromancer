using UnityEngine;
using UnityEngine.EventSystems;
using static GameInfo;

public class DropObjectStory : DropObject, IPointerEnterHandler
{
    public CardObjectMove CardReference;
    public Card CardInfo;
    public GameObject SelectHint;
    public GameObject CardChange;
    public GameObject CardRemove;
    public AudioSource HoverSFX;

    private int order;
    private StoryManager story;

    public void Setup(StoryManager story, int order) 
    {
        this.story = story;
        this.order = order;

        CardReference.Setup();
        CardReference.ShowHide(false);
        SelectHint.SetActive(false);
    }

    private void OnDisable()
    {
        // hide card reference on disable
        CardReference.ShowHide(false);
    }

    public override void DropCard(CardObjectMove card)
    {
        // card is own card
        if (card == CardReference)
        {
            SelectCard(card.CardReference);
            return;
        }

        foreach (var dropObj in story.Droppers)
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
    public void ButtonRemoveCard() 
    {
        DeselectCard();
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

        CardChange.SetActive(false);
        CardRemove.SetActive(true);
    }
    public void DeselectCard() 
    {
        CardInfo = null;

        CardReference.transform.position = transform.position;
        CardReference.transform.SetParent(story.UncompleteHolderCards);

        CardReference.ShowHide(false);

        story.SaveSelectedCard(null, order);

        SelectHint.SetActive(false);
        CardChange.SetActive(true);
        CardRemove.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardInfo != null && CardMovement.Instance.CardPickedUp == null && !SelectHint.activeInHierarchy)
        {
            story.CardSelect(CardInfo);
            HoverSFX.Play();
        }
    }
}
