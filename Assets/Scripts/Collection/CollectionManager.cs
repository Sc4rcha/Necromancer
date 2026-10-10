using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public CollectionCards[] Collections;

    private void Start()
    {
        foreach (var collection in Collections)
        {
            collection.Setup();
            collection.Show(false);
        }
    }

    public void ButtonSelectHearts() 
    {
        foreach(var collection in Collections)
        {
            if (collection.Suit == GameInfo.SUIT.Hearts)
                collection.Show(true);
        }
    }
    public void ButtonSelectDiamonds()
    {
        foreach (var collection in Collections)
        {
            if (collection.Suit == GameInfo.SUIT.Diamonds)
                collection.Show(true);
        }
    }
    public virtual void ButtonSelectSpades()
    {
        foreach (var collection in Collections)
        {
            if (collection.Suit == GameInfo.SUIT.Spades)
                collection.Show(true);
        }
    }
    public virtual void ButtonSelectClubs()
    {
        foreach (var collection in Collections)
        {
            if (collection.Suit == GameInfo.SUIT.Clubs)
                collection.Show(true);
        }
    }
}
