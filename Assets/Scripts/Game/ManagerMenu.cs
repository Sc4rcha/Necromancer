using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour
{
    public Sprite Background;
    [Space]
    public GameObject MenuDesktop;
    public GameObject MenuMobile;
    [Space]
    public GameObject TutorialButton;

    private void Start()
    {
        GameManager.Instance.Post.ChangeMoveStrength(FXPost.MovementStrength.Low);
        GameManager.Instance.Post.BackgroundImage.sprite = Background;

        if (Application.isMobilePlatform)
        {
            // Your phone-specific code here
            MenuMobile.SetActive(true);
        }
        else
        {
            // Your desktop-specific code here
            MenuDesktop.SetActive(true);
        }
#if UNITY_EDITOR
        MenuDesktop.SetActive(true);
#endif

        TutorialButton.SetActive(GameManager.Instance.SaveData.TutorialComplete);
    }

    public void ButtonStart() 
    {
        if (GameManager.Instance.SaveData.TutorialComplete)
            GameManager.Instance.GameStart();
        else
            SceneManager.LoadScene("Tutorial");
    }
    public void ButtonCollection() 
    {
        SceneManager.LoadScene("Story");
    }

    public void ButtonUnlock() 
    {
        foreach (var card in GameManager.Instance.CardsReference.Cards)
            GameManager.Instance.CardUnlock(card.GetCard());
    }
    public void ButtonReset() 
    {
        GameManager.Instance.NewSave();
    }
    public void ButtonMedal() 
    {
        GameManager.Instance.Achivements.BeatFirstTime();
    }
    public void ButtonTutorial() 
    {
        SceneManager.LoadScene("Tutorial");
    }
}
