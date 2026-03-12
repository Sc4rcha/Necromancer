using Dan.Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public Button ButtonTop;
    public Button ButtonPlayer;

    [SerializeField] private TMP_Text[] _entryTextObjects;
    [SerializeField] private TMP_InputField _usernameInputField;

    // Make changes to this section according to how you're storing the player's score:
    // ------------------------------------------------------------
    [SerializeField] private ManagerGameplay manager;

    private int Score;
    private int playerIndex = 0;
    private bool onTopScore;

    public void Setup()
    {
        LoadEntriesTop();
    }

    public void LoadEntriesTop()
    {
        Leaderboards.NecromancerLeaderboard.GetEntries(entries =>
        {
            // clear board
            foreach (var t in _entryTextObjects)
                t.text = "";

            var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
            for (int i = 0; i < length; i++)
                _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";

            ButtonTop.interactable = false;
            ButtonPlayer.interactable = true;
        });
    }
    public void LoadEntriesPlayer()
    {
        // cannot load player entry if there is no name saved SHOW TOP SCORE
        if (GameSave.Instance.Name == "")
            return;

        // get player index
        Leaderboards.NecromancerLeaderboard.GetEntries(entries =>
        {
            for (int x = 0; x < entries.Length; x++)
            {
                if (entries[x].Username == GameSave.Instance.Name)
                {
                    playerIndex = x;
                    break;
                }

                // stop if player is already in top 3 SHOW TOP SCORE
                if (playerIndex < 3)
                    return;

                // clear board
                foreach (var t in _entryTextObjects)
                    t.text = "";

                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                    _entryTextObjects[i].text = $"{entries[playerIndex - 2 + i].Rank}. {entries[playerIndex - 2 + i].Username} - {entries[playerIndex - 2 + i].Score}";

                ButtonTop.interactable = true;
                ButtonPlayer.interactable = false;
            }
        });
    }

    public void UploadEntry()
    {
        // get score
        Score = GameSave.Instance.ScoreBest;

        // stop if player has not added a name
        if (_usernameInputField.text == "Type your name" || _usernameInputField.text == "")
            return;

        // save new name
        if (GameSave.Instance.Name != "")
            GameSave.Instance.SaveName(_usernameInputField.text);

        Leaderboards.NecromancerLeaderboard.UploadNewEntry(_usernameInputField.text, Score, isSuccessful =>
        {
            if (isSuccessful)
                LoadEntriesPlayer();
        });
    }
}
