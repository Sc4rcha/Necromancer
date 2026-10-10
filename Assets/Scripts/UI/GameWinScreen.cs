using System.Collections;
using UnityEngine;

public class GameWinScreen : MonoBehaviour
{
    public CardObjectScore[] Cards;
    public TMPro.TMP_Text Score;
    public TMPro.TMP_Text ScoreBest;

    private GameInfo.Card[] scoredCards;
    private int time;
    private int scoreCurrent;
    private WaitForSeconds waitShowCard;

    public void ShowScore(GameInfo.Card[] cards, int time)
    {
        // setup variables
        waitShowCard = new WaitForSeconds(0.3f);
        scoredCards = cards;
        this.time = time;
        scoreCurrent = 0;

        // set score best
        ScoreBest.text = "Score Best : " + GameManager.Instance.SaveData.ScoreBest.ToString();

        // check achivement BEAT FOR FIRST TIME
        GameManager.Instance.Achivements.BeatFirstTime();

        // check achivement Timer
        GameManager.Instance.Achivements.BeatTimerCheck(time);

        // check achivement Canon Run
        GameManager.Instance.Achivements.CanonRunCheck();

        // save Score
        if (GameManager.Instance.SaveData.ScoreBest < GetFinalScore(cards) + time)
        {
            // update best score
            GameManager.Instance.SaveData.ScoreBest = GetFinalScore(cards) + time;

#if NEWGROUNDS
            // send High score
            NewgroundsManager.Instance?.UploadHighScore(GameManager.Instance.SaveData.ScoreBest);
#endif

        }
        // save game
        GameManager.Instance.Save();

        // show win screen
        gameObject.SetActive(true);

        // start add cards animation
        StartCoroutine(ShowScoreAnimation());
    }

    private IEnumerator ShowScoreAnimation() 
    {
        // initial animation delay
        yield return new WaitForSeconds(1);

        // initial wait
        yield return waitShowCard;
        yield return waitShowCard;

        // show cards and add score
        for (int i = 0; i < scoredCards.Length; i++)
        {
            Cards[i].gameObject.SetActive(true);
            Cards[i].Setup();
            Cards[i].SetCardInfo(scoredCards[i]);

            scoreCurrent += scoredCards[i].Value;
            Score.text = "Score : " + scoreCurrent;

            yield return waitShowCard;
        }

        // time multiplier
        yield return waitShowCard;

        // ADD TIME SCORE
        scoreCurrent += (int)time;
        Score.text = "Score : " + scoreCurrent;

        // final wait
        yield return waitShowCard;

        // Update Best Score
        if (GameManager.Instance.SaveData.ScoreBest == scoreCurrent)
            ScoreBest.text = "Score Best : " + scoreCurrent;

    }

    private int GetFinalScore(GameInfo.Card[] cards) 
    {
        int returnValue = 0;
        for (int i = 0; i < cards.Length; i++)
            returnValue += cards[i].Value;

        return returnValue;
    }
}
