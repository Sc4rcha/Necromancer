using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerMenu : MonoBehaviour
{
    public Sprite Background;
    [Space]
    public Button ButtonPlay;
    public Button ButtonStory;

    private void Start()
    {
        GameManager.Instance.Post.ChangeMoveStrength(FXPost.MovementStrength.Low);
        GameManager.Instance.Post.BackgroundImage.sprite = Background;
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
}
