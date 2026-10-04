using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardObjectMove : CardObject, IPointerDownHandler, IPointerUpHandler
{
    [Header ("Card Move")]
    public AudioSource SFXPickupDrop;
    public AudioSource SFXHitSpell;
    public AudioSource SFXHitZombie;

    public CardGroup CardGroup { private get; set; }

    private bool isLocked;

    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRect;
    private CanvasGroup canvasGroup;
    private Vector2 mousePosition;
    private Vector2 mouseOffset;


    private Transform parentPrevious;
    private Vector2 positionPrevious;

    private bool isPointerDown;

    private GraphicRaycaster raycaster;
    private PointerEventData pointerData;
    private List<RaycastResult> results;

    private void Start()
    {
        Setup();
    }

    public override void Setup()
    {
        base.Setup();

        // setup card drag movement
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.transform as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();

        raycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerData = new PointerEventData(EventSystem.current);

    }
    public override void SetCardInfo(GameInfo.Card cardReference)
    {
        base.SetCardInfo(cardReference);

        if (cardReference.IsEnemy)
            CardImage.sprite = cardReference.SpriteHand;
    }

    private void Update()
    {
        // drag movement
        if (isPointerDown)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out mousePosition))
                rectTransform.anchoredPosition = mousePosition + mouseOffset;
        }
    }


    protected virtual void PickUp() 
    {
        // play sfx
        SFXPickupDrop.Play();

        // pointer variables
        isPointerDown = true;
        canvasGroup.blocksRaycasts = false;

        // save current position for later position reset
        parentPrevious = transform.parent;
        positionPrevious = transform.position;

        // send event to manager
        CardMovement.Instance.CardPickup(this);

        // set mouse offset
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out mouseOffset);
        mouseOffset = (Vector2)rectTransform.anchoredPosition - mouseOffset;

        // shake card
        Shake.StartShake();
    }
    protected virtual void Drop()
    {
        // play sfx
        SFXPickupDrop.Play();

        pointerData.position = Input.mousePosition;
        results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        DropObject dropObj = null;
        if (results.Count > 0)
        {
            foreach (RaycastResult obj in results)
            {
                if (obj.gameObject.transform.parent.GetComponent<DropObject>())
                {
                    dropObj = obj.gameObject.transform.parent.GetComponent<DropObject>();
                    if (!dropObj.IsLocked)
                        CardMovement.Instance.DropperEnter(dropObj);

                    break;
                }
            }
        }

        // pointer variables
        isPointerDown = false;
        canvasGroup.blocksRaycasts = true;

        // send event to manager
        CardMovement.Instance.CardDrop();

        // shake card
        Shake.StartShake();

        // drop clear
        if (dropObj != null)
            CardMovement.Instance.DropperExit(dropObj);
    }


    public void Lock (bool isLock) 
    {
        isLocked = isLock;
    }
    public void DropCancel() 
    {
        transform.SetParent(parentPrevious);
        transform.position = positionPrevious;
    }
    public void DropDropper(DropObject dropper)
    {
        if (CardGroup != null)
        {
            CardGroup.CardRemove(this);
            CardGroup = null;
        }

        transform.position = dropper.transform.position;
    }

    public void SFXPlayAttack()
    {
        if (CardReference.Suit == GameInfo.SUIT.Spades || CardReference.Suit == GameInfo.SUIT.Clubs)
            SFXHitZombie.Play();
        else
            SFXHitSpell.Play();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (isLocked)
            return;

        PickUp();
        isPointerDown = true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (isLocked)
            return;

        if (isPointerDown)
            Drop();
    }
}

