using UnityEngine;

public class DropObjectEnemy : DropObject
{
    private CardObjectBattlefieldEnemy enemy;

    public void Setup(CardObjectBattlefieldEnemy enemy)
    {
        this.enemy = enemy;
    }

    public override void DropCard(CardObjectMove card)
    {
        base.DropCard(card);

        enemy.AddAttack(card);
    }
}
