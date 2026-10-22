using NewgroundsIODotNet.DataModels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewgroundsManager : MonoBehaviour
{
    public static NewgroundsManager Instance;

    public int IdWin;
    public int IdLose;
    public int IdCanon;
    public int IdThinkFast;
    public int IdArmy;
    public int IdChurch;
    public int IdStory1;
    public int IdStory2;
    public int IdStory3;
    public int IdStory4;
    public int IdStory5;
    public int IdStory6;
    [Space]
    public int IdScore;

    private void Start()
    {
        Instance = this;
    }

    public void NewgroundsLoginComplete() 
    {
        SceneManager.LoadScene("Menu");
    }

    public void UnlockMedal(int medalID)
    {
        var medal = NGIONet.Engine.Comms.GetMedal(medalID);

        if (medal == null)
        {
            UnityEngine.Debug.LogWarning( $"Newgrounds medal {medalID} could not be found.");
            return;
        }

        NGIONet.Engine.Comms.UnlockMedal(medal.Value);
    }
    public void UploadHighScore(int score)
    {
        var scoreboard = NGIONet.Engine.Comms.GetScoreboard(IdScore);
        NGIONet.Engine.Comms.PostScore(scoreboard.Value, score, null);
    }
}
