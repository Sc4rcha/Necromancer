using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;

    [Header("References Project")]
    public Sprite Background;
    [Header("References Scene")]
    public CollectionCardsStory CollectionCardsDefault;
    [Space]
    public TMPro.TMP_Text SegmentTitle;
    [Header("Uncomplete")]
    public Transform UncompleteHolderCards;
    public HorizontalLayoutGroup DropperGroup;
    public DropObjectStory[] Droppers;
    public GameObject UncompleteCheck;
    [Header("Complete")]
    public GameObject CompleteHolderCards;
    public CardObjectStory[] CompleteCards;
    public ParticleSystem CheckPassFX;
    public AudioSource CheckPassVFX;
    [Space]
    public TMPro.TMP_Text StoryText;

    private int segmentIndex;
    private DropObjectStory selectedDropper;
    private GameManager manager;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        manager = GameManager.Instance;

        // setup droppers
        for (int i = 0; i < Droppers.Length; i++)
            Droppers[i].Setup(this, i);
        // setup cards
        foreach (var card in CompleteCards)
            card.Setup();

        // set index to prologue
        segmentIndex = 0;

        // show first segment by default
        RefreshSegment();

        // DEBUG UNLOCK ALL
        foreach (var card in GameManager.Instance.CardsReference.Cards)
            GameManager.Instance.CardUnlock(card.GetCard());

        // set background
        GameManager.Instance.Post.BackgroundImage.sprite = Background;
    }

    public void ButtonChangeSegment(int direction) 
    {
        segmentIndex += direction;
        if (segmentIndex < 0)
            segmentIndex = manager.StorySegmentsReference.Length - 1;
        else if (segmentIndex == manager.StorySegmentsReference.Length)
            segmentIndex = 0;

        // refresh story segment
        RefreshSegment();
    }
    public void ButtonCheckSegment() 
    {
        if (manager.SaveData.CheckStorySegment(segmentIndex, manager.StorySegmentsReference[segmentIndex]))
        {
            manager.SaveData.StorySegments[segmentIndex].IsComplete = true;

            // effect
            CheckPassFX.Play();
            CheckPassVFX.Play();

            // save game
            GameManager.Instance.Save();

            // check achivement section Check Pass
            GameManager.Instance.Achivements.StorySegment(segmentIndex);

            RefreshSegment();
        }
    }
    public void ButtonExit() 
    {
        SceneManager.LoadScene("MenuNewgrounds");
    }

    private void RefreshSegment() 
    {
        SegmentTitle.text = manager.StorySegmentsReference[segmentIndex].Title;

        bool isComplete = manager.SaveData.StorySegments[segmentIndex].IsComplete;

        DropperGroup.gameObject.SetActive(!isComplete);
        UncompleteCheck.SetActive(!isComplete);
        UncompleteHolderCards.gameObject.SetActive(!isComplete);
        CompleteHolderCards.SetActive(isComplete);

        // uncomplete behaviour
        if (!isComplete)
        {
            for (int i = 0; i < Droppers.Length; i++)
                Droppers[i].gameObject.SetActive(i < manager.StorySegmentsReference[segmentIndex].CorrectOrder.Length);

            // force rebuild horizontal layout group
            LayoutRebuilder.ForceRebuildLayoutImmediate(DropperGroup.GetComponent<RectTransform>());

            for (int i = 0; i < manager.StorySegmentsReference[segmentIndex].CorrectOrder.Length; i++)
            {
                string cardId = manager.SaveData.StorySegments[segmentIndex].cardCodes[i];
                if (manager.CardsReference.GetCardById(cardId) != null)
                    Droppers[i].SelectCard(manager.CardsReference.GetCardById(cardId).GetCard());
                else
                    Droppers[i].DeselectCard();
            }
        }
        else
        {
            // activate/deactivate cards
            for (int i = 0; i < CompleteCards.Length; i++)
                CompleteCards[i].gameObject.SetActive(i < manager.StorySegmentsReference[segmentIndex].CorrectOrder.Length);

            // set card info
            for (int i = 0; i < manager.StorySegmentsReference[segmentIndex].CorrectOrder.Length; i++)
                CompleteCards[i].SetCardInfo(manager.CardsReference.GetCardById(manager.SaveData.StorySegments[segmentIndex].cardCodes[i]).GetCard());
        }
    }

    public void CardSelect(GameInfo.Card card) 
    {
        StoryText.text = manager.CardsReference.GetCardById(card.Id).CardStory;
    }
    public void CardSelectorOpen(DropObjectStory dropper) 
    {
        selectedDropper = dropper;
        CollectionCardsDefault.Show(true);
    }
    public void CardSelectorSelect(GameInfo.Card card) 
    {
        selectedDropper.SelectCard(card);
    }

    public void SaveSelectedCard(string cardId, int dropperOrder) 
    {
        manager.SaveData.StorySegments[segmentIndex].cardCodes[dropperOrder] = cardId;
    }
}
