using UnityEngine;
using UnityEngine.EventSystems;

public class CardObjectMove : CardObject, IPointerEnterHandler, IPointerExitHandler
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

    private bool isPointerInside;
    private bool isPointerDown;

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
    }
    public override void SetCardInfo(GameInfo.Card cardReference)
    {
        base.SetCardInfo(cardReference);

        if (cardReference.IsEnemy)
            CardImage.sprite = cardReference.SpriteHand;
    }

    private void Update()
    {
        // pickup and drop inputs
        if (Input.GetMouseButtonDown(0) && isPointerInside)
            PickUp();
        if (Input.GetMouseButtonUp(0) && isPointerDown)
            Drop();

        // drag movement
        if (isPointerDown)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out mousePosition))
                rectTransform.anchoredPosition = mousePosition + mouseOffset;
        }
    }


    private void PickUp() 
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
        ManagerGameplay.Instance.CardMovement.CardPickup(this);

        // set mouse offset
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out mouseOffset);
        mouseOffset = (Vector2)rectTransform.anchoredPosition - mouseOffset;

        // shake card
        Shake.StartShake();
    }
    private void Drop()
    {
        // play sfx
        SFXPickupDrop.Play();

        // pointer variables
        isPointerDown = false;
        canvasGroup.blocksRaycasts = true;

        // send event to manager
        ManagerGameplay.Instance.CardMovement.CardDrop(this);

        // shake card
        Shake.StartShake();
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
        CardGroup.CardRemove(this);
        CardGroup = null;

        transform.position = dropper.transform.position;
    }

    public void SFXPlayAttack()
    {
        if (CardReference.Suit == GameInfo.SUIT.Spades || CardReference.Suit == GameInfo.SUIT.Clubs)
            SFXHitZombie.Play();
        else
            SFXHitSpell.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isLocked)
            return;

        isPointerInside = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isLocked)
            return;

        isPointerInside = false;
    }
}

