using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour
{
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
}
