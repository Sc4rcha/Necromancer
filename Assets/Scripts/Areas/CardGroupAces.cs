using UnityEngine;

public class CardGroupAces : CardGroup
{
    [Header ("Aces")]
    public SoCard AceHearts;
    public SoCard AceDiamonds;

    public override void Setup()
    {
        base.Setup();

        CardAdd(AceHearts.GetCard());
        CardAdd(AceDiamonds.GetCard());
    }
}
