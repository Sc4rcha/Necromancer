using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class SoCard : ScriptableObject
{
    public GameInfo.SUIT CardSuit;
    public int CardValue;
    public Sprite CardSprite;
    public Sprite CardSpriteHand;
    public Sprite SuitSprite;

    public GameInfo.Card GetCard() 
    {
        return new GameInfo.Card(CardSuit, CardValue, CardSprite, CardSpriteHand, SuitSprite);
    }
}
