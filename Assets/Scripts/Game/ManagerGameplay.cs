using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static GameInfo;
using System.Collections;

public class ManagerGameplay : MonoBehaviour
{
    public static ManagerGameplay Instance;

    public float TimeMax;
    public int MaxHandSize;

    [Header("Project References")]
    public SoCardList ReferenceLibrary;
    public Sprite Background;

    [Header("Scene References")]
    public PileLibrary Library;
    public PileDiscard Discard;
    public CardGroupHand GroupHand;
    public CardGroup GroupAces;
    [Space]
    public BattlefieldSlot[] Battlefield;
    public GameObject DiscardMessage;
    [Space]
    public CardVisuals CardVisuals;
    public InfoRounds Rounds;
    [Space]
    public TMPro.TMP_Text TimeText;
    public AudioSource TimeSFX;
    [Space]
    public GameWinScreen GameWinScreen;
    public GameObject GameLoseScreen;

    // score extra
    private bool gameOver;
    private float timeTotal;
    private int secondLast;
    private int secondCurrent;
    private List<Card> scoredCards;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Setup();

        StartCoroutine(WaitStart());
    }

    protected void Setup() 
    {
        // setup Piles
        Library.Setup();
        Discard.Setup();

        // setup groups
        GroupHand.Setup();
        GroupAces.Setup();

        // setup slots
        foreach (BattlefieldSlot slot in Battlefield)
            slot.Setup();

        // setup card visuals
        CardVisuals.Setup();

        // setup backgrond
        GameManager.Instance.Post.BackgroundImage.sprite = Background;

        // start counting time;
        timeTotal = TimeMax;
        secondLast = 0;
        gameOver = false;

        // achivement canon run setup
        GameManager.Instance.Achivements.CanonRunStart();
    }
    protected void StartGame() 
    {
        // initial draw
        DrawInitial();

        // initial Battlefield reveal
        NewBattlefield();

        // start counting time;
        scoredCards = new List<Card>();
    }

    private void Update()
    {
        if (gameOver) return;

        timeTotal -= Time.deltaTime;
        timeTotal = Mathf.Max (timeTotal, 0);
        secondCurrent = Mathf.CeilToInt(timeTotal);

        TimeText.text = secondCurrent.ToString("000");

        if (secondCurrent != secondLast)
        {
            secondLast = secondCurrent;
            TimeSFX.Play();
        }
    }

    private void DrawInitial() 
    {
        List<Card> initialHand = Library.Draw();

        // order army list
        initialHand = initialHand.OrderByDescending(x => x.Value).ToList();

        // add army to player
        foreach(Card card in initialHand)
        {
            GroupHand.CardAdd(card);
            CardVisuals.MoveCardDraw(Library.transform as RectTransform, GroupHand);
        }
    }
    public void RevealBattlefield() 
    {
        // check if there are still enemies in the battlefield
        foreach (BattlefieldSlot slot in Battlefield)
        {
            if (!slot.IsEmpty && slot.IsEnemy) return;
        }

        // pickup all remaining spells
        foreach (BattlefieldSlot slot in Battlefield)
        {
            if (!slot.IsEmpty && !slot.IsEnemy) slot.Spell.TakeCard();
        }

        // WIN GAME IF NO MORE CARDS IN LIBRARY
        if (Library.LibraryCleared())
        {
            GameWin();
            return;
        }

        // check if player has more than 4 cards
        if (GroupHand.Cards.Count > MaxHandSize)
        {
            DiscardMessage.SetActive(true);
            Discard.LockDropper(false);
            return;
        }
        else 
        {
            DiscardMessage.SetActive(false);
            Discard.LockDropper(true);
        }

        NewBattlefield();
    }

    protected virtual void NewBattlefield()
    {
        // draw first 4 cards
        List<Card> battlefield = Library.Draw();
        for (int i = 0; i < Battlefield.Length; i++)
        {
            Battlefield[i].SetCard(battlefield[i]);
            CardVisuals.MoveCard(Library.transform as RectTransform, Battlefield[i].transform as RectTransform, Battlefield[i].SelectedCard);
        }

        // refresh INFO round count
        Rounds.RefreshRounds(Library.Cards.Count);

        // start reveal battlefield coroutine to check
        StartCoroutine(WaitRevealBattlefield());
    }

    public void AddCardToScore(Card card) 
    {
        scoredCards.Add(card);
    }

    public void CheckGameOver() 
    {
        // check for Hand Empty
        if (GroupHand.GetLastCard() != null || GroupAces.GetLastCard() != null)
            return;

        // check for enemies remeining
        foreach (BattlefieldSlot slot in Battlefield)
        {
            if (!slot.IsEmpty && slot.IsEnemy)
            {
                GameLose();
                return;
            }
        }
    }
    public virtual void GameWin() 
    {
        gameOver = true;

        GroupAces.Lock(true);
        GroupHand.Lock(true);

        foreach (var card in GroupAces.Cards)
            AddCardToScore(card.CardReference);
        foreach (var card in GroupHand.Cards)
            AddCardToScore(card.CardReference);

        GameWinScreen.ShowScore(scoredCards.ToArray(), Mathf.CeilToInt(timeTotal));

    }
    private void GameLose() 
    {
        gameOver = true;

        GameLoseScreen.SetActive(true);
    }

    private IEnumerator WaitStart() 
    {
        yield return new WaitForSeconds(0.5f);

        StartGame();
    }
    private IEnumerator WaitRevealBattlefield() 
    {
        while (CardVisuals.Queue.Count > 0)
            yield return null;

        yield return new WaitForSeconds(1);

        RevealBattlefield();
    }
}
