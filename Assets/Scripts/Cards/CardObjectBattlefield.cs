using UnityEngine;

public class CardObjectBattlefield : CardObject
{
    protected BattlefieldSlot battlefieldSlot;

    public virtual void Setup(BattlefieldSlot battlefieldSlot)
    {
        base.Setup();

        this.battlefieldSlot = battlefieldSlot;
    }

    public virtual void Activate() 
    {
        ShowHide(true);
    }
}
