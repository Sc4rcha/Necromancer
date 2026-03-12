using UnityEngine;

public class BattlefieldSlot : MonoBehaviour
{
    public CardObjectBattlefieldEnemy Enemy;
    public CardObjectBattlefieldSpell Spell;

    public bool IsEmpty { get; private set; }
    public bool IsEnemy { get; private set; }
    public CardObjectBattlefield SelectedCard { get; private set; }

    public void Setup() 
    {
        Spell.Setup(this);
        Enemy.Setup(this);

        // clear
        Spell.ShowHide(false);
        Enemy.ShowHide(false);

        IsEmpty = true;
    }

    public void SetCard(GameInfo.Card cardInfo) 
    {
        if (cardInfo.Suit == GameInfo.SUIT.Hearts || cardInfo.Suit == GameInfo.SUIT.Diamonds)
        {
            SelectedCard = Spell;
            IsEnemy = false;
        }
        else if (cardInfo.Suit == GameInfo.SUIT.Spades || cardInfo.Suit == GameInfo.SUIT.Clubs)
        {
            SelectedCard = Enemy;
            IsEnemy = true;
        }

        SelectedCard.SetCardInfo(cardInfo);
        SelectedCard.Activate();

        IsEmpty = false;
    }
    public void ClearSlot()
    {
        Spell.ShowHide(false);
        Enemy.ShowHide(false);

        IsEmpty = true;
    }
}
