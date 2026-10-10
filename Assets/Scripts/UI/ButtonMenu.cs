using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    public void ButtonInput()
    {
        SceneManager.LoadScene("Menu");
    }
}
