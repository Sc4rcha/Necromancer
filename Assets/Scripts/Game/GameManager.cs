using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SAVE.GameSave SaveData;

    [Header("Achivements")]
    public ManagerAchivements Achivements;

    [Header("Cards")]
    public SoCardList CardsReference;
    public SoStorySegment[] StorySegmentsReference;

    [Header ("Audio")]
    public AudioMixer Mixer;
    public float[] Volumes;
    [Space]
    public AudioSource SFXSource;

    [Header("FX")]
    public FXPost Post;

    public int VolumesIndex { get; private set; }

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Setup();
        }
        else
            Destroy(gameObject);
    }

    private void Setup()
    {
        GetSave();
        VolumesIndex = 2;

        Mixer.SetFloat("Master", Volumes[VolumesIndex]);

        GameManager.Instance.Post.ChangeMoveStrength(FXPost.MovementStrength.Medium);
    }

    private void GetSave()
    {
        if (SAVE.SaveManager.CheckSave())
            SaveData = SAVE.SaveManager.Load();
        else
            NewSave();
    }
    public void Save()
    {
        SAVE.SaveManager.Save(SaveData);
    }
    public void NewSave() 
    {
        SaveData = new SAVE.GameSave();
        SaveData.SetupStorySegments(StorySegmentsReference);
        SAVE.SaveManager.Save(SaveData);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void AudioCycle() 
    {
        VolumesIndex = (VolumesIndex + 1) % Volumes.Length;
        Mixer.SetFloat("Master", Volumes[VolumesIndex]);
    }
    public void AudioSfxPlay(AudioResource resource) 
    {
        SFXSource.resource = resource;
        SFXSource.Play();
    }

    public void CardUnlock(GameInfo.Card card) 
    {
        if (SaveData.CardsUnlocked.Contains(card.Id))
            return;

        SaveData.CardsUnlocked.Add(card.Id);

        // check achivement card Unlock
        Achivements.UnlockCardCheck(card);
    }
}
