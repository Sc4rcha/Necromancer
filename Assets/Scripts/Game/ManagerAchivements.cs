using System.Collections.Generic;
using UnityEngine;

public class ManagerAchivements : MonoBehaviour
{
    private bool isCanonRun;
    public void CanonRunStart() 
    {
        isCanonRun = true;
    }
    public void CanonRunFail() 
    {
        isCanonRun = false;
    }
    public void CanonRunCheck() 
    {
        if (isCanonRun)
        {
            // unlock canon run
#if NEWGROUNDS
            NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdCanon);
#endif
        }
    }

    public void BeatFirstTime()
    {
        // unlock beat First Time
#if NEWGROUNDS
        NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdWin);
#endif
    }
    public void BeatTimerCheck(int timer) 
    {
        if (timer >= 60)
        {
            // unlock beat in 60 seconds
#if NEWGROUNDS
            NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdThinkFast);
#endif
        }
    }
    public void UnlockCardCheck(GameInfo.Card cardUnlocked)
    {
        List<string> unlockedCards = GameManager.Instance.SaveData.CardsUnlocked;
        SoCardList cardsReference = GameManager.Instance.CardsReference;
        List<SoCard> suitCards = new List<SoCard>();

        // populate list of cards to check for
        if (cardUnlocked.Suit == GameInfo.SUIT.Clubs)
        {
            foreach(var card in cardsReference.Cards)
            {
                if (card.CardSuit == GameInfo.SUIT.Clubs)
                    suitCards.Add(card);
            }
        }
        else if (cardUnlocked.Suit == GameInfo.SUIT.Spades)
        {
            foreach (var card in cardsReference.Cards)
            {
                if (card.CardSuit == GameInfo.SUIT.Spades)
                    suitCards.Add(card);
            }
        }

        // check if a card is missing and stop if not unlocked
        foreach (var card in suitCards)
        {
            if (!unlockedCards.Contains(card.Id))
                return;
        }

        // unlock achivement
        if (cardUnlocked.Suit == GameInfo.SUIT.Clubs)
        {
#if NEWGROUNDS
            NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdChurch);
#endif
        }
        else if (cardUnlocked.Suit == GameInfo.SUIT.Spades)
        {
#if NEWGROUNDS
            NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdArmy);
#endif
        }
    }

    public void StorySegment(int index) 
    {
        switch (index)
        {
            case 0:
                // unlock Prologue
#if NEWGROUNDS
                NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdStory1);
#endif
                break;
            case 1:
                // unlock part 1
#if NEWGROUNDS
                NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdStory2);
#endif
                break;
            case 2:
                // unlock part 2
#if NEWGROUNDS
                NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdStory3);
#endif
                break;
            case 3:
                // unlock part 3
#if NEWGROUNDS
                NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdStory4);
#endif
                break;
            case 4:
                // unlock Lore
#if NEWGROUNDS
                NewgroundsManager.Instance?.UnlockMedal(NewgroundsManager.Instance.IdStory5);
#endif
                break;
        }
    }

}
