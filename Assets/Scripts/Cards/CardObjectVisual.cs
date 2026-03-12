using System.Collections;
using UnityEngine;
using static GameInfo;

public class CardObjectVisual : CardObject
{
    [Header ("Card Visual")]
    public AudioSource SFXWoosh;
    public bool IsAvailable { get; private set; }

    private const float timeToMove = 0.3f;
    private float timeToMoveCurrent;

    private Vector2 positionStart;
    private Vector2 positionEnd;

    private RectTransform rectTransform;
    private WaitForEndOfFrame waifForEndOfFrame;

    private CardObject cardObjectReference;

    public override void Setup()
    {
        base.Setup();

        waifForEndOfFrame = new WaitForEndOfFrame();
        rectTransform = GetComponent<RectTransform>();

        IsAvailable = true;
    }

    public void MoveSetup(Vector2 start, Vector2 end, CardObject cardObjectReference)
    {
        // set card reference
        this.cardObjectReference = cardObjectReference;
        this.cardObjectReference.ShowHide(false);

        // show card information
        SetCardInfo(cardObjectReference.CardReference);
        CardImage.sprite = cardObjectReference.CardImage.sprite;

        // set positions
        positionStart = start;
        positionEnd = end;
        rectTransform.position = start;

        // set card as unavailable
        IsAvailable = false;
    }
    public void MoveStart() 
    {
        // activate and start movement coroutine
        gameObject.SetActive(true);
        StartCoroutine(MoveToPosition());

        // play sfx
        SFXWoosh.Play();
    }
    public void MoveEnd() 
    {
        // show reference card object
        cardObjectReference.ShowHide(true);

        // if card is discard send card information
        if (cardObjectReference is CardObjectDiscard discard)
        {
            discard.SetCards(this);
            cardObjectReference.ShowHide(false);
        }

        // disable card object
        gameObject.SetActive(false);
        IsAvailable = true;
    }

    private IEnumerator MoveToPosition() 
    {
        // setup movevement time
        timeToMoveCurrent = 0;

        // move card
        while (timeToMoveCurrent < timeToMove)
        {
            timeToMoveCurrent += Time.deltaTime;
            rectTransform.position = Vector2.Lerp(positionStart, positionEnd, timeToMoveCurrent / timeToMove);

            yield return waifForEndOfFrame;
        }

        // end
        MoveEnd();
    }
}
