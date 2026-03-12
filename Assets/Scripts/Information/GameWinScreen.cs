using System.Collections;
using UnityEngine;
using static GameInfo;

public class GameWinScreen : MonoBehaviour
{
    public CardObjectScore[] Cards;
    public TMPro.TMP_Text Score;
    public TMPro.TMP_Text ScoreBest;
    public LeaderboardManager Board;

    private CardObject[] scoredCards;
    private int scoreCurrent;
    private WaitForSeconds waitShowCard;

    public void ShowScore(CardObject[] cards) 
    {
        // setup variables
        waitShowCard = new WaitForSeconds(0.3f);
        scoredCards = cards;
        scoreCurrent = 0;

        // set score best
        ScoreBest.text = "Score Best : " + GameSave.Instance.ScoreBest.ToString();

        // save Score
        if (GameSave.Instance.ScoreBest < GetFinalScore(cards))
            GameSave.Instance.SaveScore(GetFinalScore(cards));

        // show win screen
        gameObject.SetActive(true);

        // start add cards animation
        StartCoroutine(ShowScoreAnimation());
    }

    private IEnumerator ShowScoreAnimation() 
    {
        // initial wait
        yield return waitShowCard;
        yield return waitShowCard;

        // show cards and add score
        for (int i = 0; i < scoredCards.Length; i++)
        {
            Cards[i].gameObject.SetActive(true);
            Cards[i].Setup();
            Cards[i].SetCardInfo(scoredCards[i].CardReference);

            scoreCurrent += scoredCards[i].CardReference.Value;
            Score.text = "Score : " + scoreCurrent;

            yield return waitShowCard;
        }

        // final wait
        yield return waitShowCard;

        // Update Best Score
        if (GameSave.Instance.ScoreBest == scoreCurrent)
            ScoreBest.text = "Score Best : " + GameSave.Instance.ScoreBest.ToString();

    }

    private int GetFinalScore(CardObject[] cards) 
    {
        int returnValue = 0;
        for (int i = 0; i < cards.Length; i++)
            returnValue += cards[i].CardReference.Value;

        return returnValue;
    }
}
