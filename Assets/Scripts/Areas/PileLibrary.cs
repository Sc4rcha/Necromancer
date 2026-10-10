using UnityEngine;
using System.Collections.Generic;
using static GameInfo;
using System.Linq;
using UnityEngine.UI;

public class PileLibrary : Pile
{
    [Header("Library")]
    private List<GameInfo.Card> drawnCards;
    private bool drawAllEnemies;
    private bool libraryAllEnemies;

    public override void Setup()
    {
        base.Setup();

        // setup pile list
        foreach (SoCard card in ManagerGameplay.Instance.ReferenceLibrary.Cards)
            Cards.Add(card.GetCard());

        // shuffle pile
        Shuffle();

        // set library pile height
        CardTransformUp.anchoredPosition = new Vector2(0, Cards.Count * CardWidth);
    }

    public bool LibraryCleared() 
    {
        return Cards.Count == 0;
    }

    public List<GameInfo.Card> Draw() 
    {
        // stop if no more cards in library
        if (Cards.Count == 0)
            return null;

        drawnCards = new List<GameInfo.Card> ();
        drawnCards = Cards.Take(4).ToList();

        while (AllSame(drawnCards) && !AllSame(Cards))
        {
            Shuffle();
            drawnCards = Cards.Take(4).ToList();
        }

        // remove drawn cards
        foreach (var cardInfo in drawnCards)
            Cards.Remove(cardInfo);


        // set library pile height
        CardTransformUp.anchoredPosition = new Vector2(0, Cards.Count * CardWidth);
        if (Cards.Count == 0)
        {
            CardObjectBot.SetActive(false);
            CardTransformUp.gameObject.SetActive(false);
        }

        // give card
        return drawnCards;
    }

    private bool AllEnemies(IEnumerable<Card> cards)
    {
        return cards.All(c => c.IsEnemy);
    }
    private bool AllSpells(IEnumerable<Card> cards)
    {
        return cards.All(c => !c.IsEnemy);
    }
    private bool AllSame(IEnumerable<Card> cards)
    {
        return AllEnemies(cards) || AllSpells(cards);
    }
}
