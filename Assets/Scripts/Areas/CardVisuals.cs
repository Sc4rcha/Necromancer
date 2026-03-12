using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static GameInfo;

public class CardVisuals : MonoBehaviour
{
    public CardObjectVisual[] Cards;
    public List<CardObjectVisual> Queue { get; private set; }

    private CardObjectVisual cardInstance;
    private Vector2 positionStart;
    private Vector2 positionEnd;

    private const float timeMoveCardDelay = 0.2f;
    private WaitForSeconds MoveCardDelayWait;

    public void Setup() 
    {
        foreach (CardObjectVisual card in Cards)
            card.Setup();

        MoveCardDelayWait = new WaitForSeconds(timeMoveCardDelay);
        Queue = new List<CardObjectVisual>();
    }


    public void MoveCardDiscard (CardObject cardToDiscard, CardObject cardDiscardPile)
    {
        MoveCard(cardToDiscard.transform.GetComponent<RectTransform>(), cardDiscardPile.transform.GetComponent<RectTransform>(), cardDiscardPile);
        cardInstance.gameObject.SetActive(true);
    }
    public void MoveCardPickup(RectTransform transformStart, CardGroup group)
    {
        MoveCardDraw(transformStart, group);
        cardInstance.gameObject.SetActive(true);
    }
    public void MoveCardDraw (RectTransform transformStart, CardGroup group)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(group.CardHolder);

        // Move card to last position of card group
        MoveCard(transformStart, group.GetLastCard().transform as RectTransform, group.GetLastCard());
    }

    public void MoveCard(RectTransform transformStart, RectTransform transformEnd, CardObject card) 
    {
        // get avaiable card
        cardInstance = GetAvailableCard();
        // setup card
        cardInstance.MoveSetup(transformStart.position , transformEnd.position, card);

        // add card to queue
        Queue.Add(cardInstance);

        // start Coroutine if first card added
        if (Queue.Count == 1)
            StartCoroutine(MoveCardDelay());
    }
    
    private CardObjectVisual GetAvailableCard() 
    {
        // go though cards and find deacitvated card
        for (int i = Cards.Length - 1; i >= 0; i--)
        {
            if (Cards[i].IsAvailable)
                return Cards[i];
        }

        return null;
    }


    private IEnumerator MoveCardDelay() 
    {
        // start movement of FIRST card in queue
        Queue[0].MoveStart();

        yield return MoveCardDelayWait;

        // advance queue
        QueueAdvance();
    }
    private void QueueAdvance() 
    {
        // remove first card from list (already moved by coroutine)
        Queue.RemoveAt(0);

        // if queue is not finished go to next card in queue.
        if (Queue.Count > 0)
            StartCoroutine(MoveCardDelay());
    }
}
