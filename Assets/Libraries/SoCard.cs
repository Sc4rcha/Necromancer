using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class SoCard : ScriptableObject
{
    public string Id;
    public GameInfo.SUIT CardSuit;
    public int CardValue;
    public Sprite CardSprite;
    public Sprite CardSpriteHand;
    [Space]
    public ChapterPlacement Placement;
    [Space]
    [TextArea(10,25)]
    public string CardStory;

    [ContextMenu("GenerateId")]
    public void GenerateId()
    {
        Id = Guid.NewGuid().ToString("N");
    }
    public GameInfo.Card GetCard() 
    {
        return new GameInfo.Card(Id, CardSuit, CardValue, CardSprite, CardSpriteHand);
    }

    public enum ChapterPlacement { Start, Middle, End }
}
