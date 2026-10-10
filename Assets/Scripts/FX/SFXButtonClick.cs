using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXButtonClick : MonoBehaviour
{
    [Header("PUT INSIDE BUTTON!")]
    public AudioResource Clip;
    private Button button;

    private void Awake()
    {
        button = transform.parent.GetComponent<Button>();
        button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        GameManager.Instance.AudioSfxPlay(Clip);
    }
}
