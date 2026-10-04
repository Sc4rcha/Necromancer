using UnityEngine;

public class ButtonAudioCycle : MonoBehaviour
{
    public void ButtonInteract()
    {
        GameManager.Instance.AudioCycle();
    }
}
