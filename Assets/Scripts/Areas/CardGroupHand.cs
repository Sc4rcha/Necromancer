using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardGroupHand : CardGroup
{
    private float spacing4 = 16;
    private float spacing5 = -38;
    private float spacing6 = -70.5f;
    private float spacing7 = -92f;
    private float spacing8 = -107.5f;

    private HorizontalLayoutGroup layoutGroup;

    public override void Setup()
    {
        base.Setup();

        layoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    public override void CardAdd(GameInfo.Card cardInfo)
    {
        base.CardAdd(cardInfo);
        RefreshCardSpacing();
    }
    public override void CardRemove(CardObjectMove card)
    {
        base.CardRemove(card);
        RefreshCardSpacing();
    }

    private void RefreshCardSpacing() 
    {
        if (Cards.Count <= 4)
            layoutGroup.spacing = spacing4;
        else if (Cards.Count == 5)
            layoutGroup.spacing = spacing5;
        else if (Cards.Count == 6)
            layoutGroup.spacing = spacing6;
        else if (Cards.Count == 7)
            layoutGroup.spacing = spacing7;
        else
            layoutGroup.spacing = spacing8;
    }
}
