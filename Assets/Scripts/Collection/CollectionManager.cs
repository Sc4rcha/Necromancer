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
            collection.Show(collection.Suit == GameInfo.SUIT.Hearts);
    }
    public void ButtonSelectDiamonds()
    {
        foreach (var collection in Collections)
            collection.Show(collection.Suit == GameInfo.SUIT.Diamonds);
    }
    public virtual void ButtonSelectSpades()
    {
        foreach (var collection in Collections)
            collection.Show(collection.Suit == GameInfo.SUIT.Spades);
    }
    public virtual void ButtonSelectClubs()
    {
        foreach (var collection in Collections)
            collection.Show(collection.Suit == GameInfo.SUIT.Clubs);
    }
}
