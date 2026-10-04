using UnityEngine;

public class ButtonRestart : MonoBehaviour
{
    public void ButtonInput() 
    {
        GameManager.Instance.GameStart();
    }
}
