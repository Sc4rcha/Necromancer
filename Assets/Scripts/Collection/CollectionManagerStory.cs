using UnityEngine;

public class CollectionManagerStory : CollectionManager
{
    public override void ButtonSelectClubs()
    {
        base.ButtonSelectClubs();

        foreach (var collection in Collections)
        {
            if (collection.Suit == GameInfo.SUIT.Spades)
                collection.Show(false);
        }

        foreach (var collection in Collections)
        {
            if (collection.Suit == GameInfo.SUIT.Clubs)
            {
                StoryManager.Instance.CollectionCardsDefault = (collection as CollectionCardsStory);
                collection.Show(true);
            }
        }

        GameManager.Instance.SFXSource.Play();
    }
    public override void ButtonSelectSpades()
    {
        base.ButtonSelectSpades();

        foreach (var collection in Collections)
        {
            if (collection.Suit == GameInfo.SUIT.Clubs)
                collection.Show(false);
        }

        foreach (var collection in Collections)
        {
            if (collection.Suit == GameInfo.SUIT.Spades)
            {
                StoryManager.Instance.CollectionCardsDefault = (collection as CollectionCardsStory);
                collection.Show(true);
            }
        }

        GameManager.Instance.SFXSource.Play();
    }
}
