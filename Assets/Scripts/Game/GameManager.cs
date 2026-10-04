using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SAVE.GameSave SaveData;

    [Header("Cards")]
    public SoCardList CardsReference;
    public SoStorySegment[] StorySegmentsReference;

    [Header ("Audio")]
    public AudioMixer Mixer;
    public float[] Volumes;

    private int volumesIndex;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Setup();
        }
        else
            Destroy(this);
    }

    private void Setup()
    {
        GetSave();
        volumesIndex = 2;

        Mixer.SetFloat("Master", Volumes[volumesIndex]);
    }

    private void GetSave()
    {
        if (SAVE.SaveManager.CheckSave())
            SaveData = SAVE.SaveManager.Load();
        else
        {
            SaveData = new SAVE.GameSave();
            SaveData.SetupStorySegments(StorySegmentsReference);
        }
    }
    public void Save()
    {
        SAVE.SaveManager.Save(SaveData);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }
    public void AudioCycle() 
    {
        volumesIndex = (volumesIndex + 1) % Volumes.Length;
        Mixer.SetFloat("Master", Volumes[volumesIndex]);
    }

    public void CardUnlock(GameInfo.Card card) 
    {
        if (SaveData.CardsUnlocked.Contains(card.Id))
            return;

        SaveData.CardsUnlocked.Add(card.Id);
    }
}
