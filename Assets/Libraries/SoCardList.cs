using UnityEngine;

[CreateAssetMenu(fileName = "Card List", menuName = "Scriptable Objects/Card List")]
public class SoCardList : ScriptableObject
{
    public SoCard[] Cards;

    public SoCard GetCardById(string id)
    {
        foreach (var card in Cards)
        {
            if (card.Id == id)
                return card;
        }
        return null;
    }
}
