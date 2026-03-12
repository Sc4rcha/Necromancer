using UnityEngine;
using UnityEngine.Audio;

public class GameSave : MonoBehaviour
{
    public static GameSave Instance;

    public int VolumeVFX = 5;
    [Space]
    public int TutorialComplete = 0;
    [Space]
    public string CollectionProgress = "00000000000000000000000000";
    public int ScoreBest = 0;
    [Space]
    public string Name;
    [Header ("Audio Mixer")]
    public AudioMixer Mixer;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
            Destroy(this);
    }


    private void Load()
    {
        if (PlayerPrefs.HasKey("VolumeVFX"))
            VolumeVFX = PlayerPrefs.GetInt("VolumeVFX");

        if (PlayerPrefs.HasKey("TutorialComplete"))
            TutorialComplete = PlayerPrefs.GetInt("TutorialComplete");

        if (PlayerPrefs.HasKey("CollectionProgress"))
            CollectionProgress = PlayerPrefs.GetString("CollectionProgress");
        if (PlayerPrefs.HasKey("ScoreBest"))
            ScoreBest = PlayerPrefs.GetInt("ScoreBest");

        if (PlayerPrefs.HasKey("Name"))
            Name = PlayerPrefs.GetString("Name");
        else
            Name = "";

        // set vfx Volume
        VolumeChange(VolumeVFX);
    }

    public void VolumeChange(float value) 
    {
        VolumeVFX = (int)value;

        value /= 10;
        if (value == 0)
            Mixer.SetFloat("Master", -80);
        else
            Mixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }
    public void CardAddProgress(GameInfo.Card card) 
    {
        int index = 0;
        if (card.Suit == GameInfo.SUIT.Clubs)
            index = 13;

        index += card.Value - 2;

        char[] charArr = CollectionProgress.ToCharArray();
        charArr[index] = '1';
        CollectionProgress = new string(charArr);
    }

    public void SaveScore(int scoreBest)
    {
        ScoreBest = scoreBest;

        PlayerPrefs.SetInt("ScoreBest", ScoreBest);

        PlayerPrefs.Save();
    }
    public void SaveProgress() 
    {
        PlayerPrefs.SetString("CollectionProgress", CollectionProgress);

        PlayerPrefs.Save();
    }
    public void SaveVolume() 
    {
        PlayerPrefs.SetInt("VolumeVFX", VolumeVFX);

        PlayerPrefs.Save();
    }
    public void SaveTutorialComplete() 
    {
        TutorialComplete = 1;

        PlayerPrefs.SetInt("TutorialComplete", TutorialComplete);

        PlayerPrefs.Save();
    }

    public void SaveName (string name)
    {
        Name = name;

        PlayerPrefs.SetString("Name", Name);

        PlayerPrefs.Save();
    }
}
