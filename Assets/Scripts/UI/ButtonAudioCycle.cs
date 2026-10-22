using UnityEngine;

public class ButtonAudioCycle : MonoBehaviour
{
    public GameObject[] VolumeStages;

    private void Start()
    {
        for (int i = 0; i < VolumeStages.Length; i++)
            VolumeStages[i].SetActive(i == GameManager.Instance.VolumesIndex);
    }

    public void ButtonInteract()
    {
        GameManager.Instance.AudioCycle();

        for (int i = 0; i < VolumeStages.Length; i++)
            VolumeStages[i].SetActive(i == GameManager.Instance.VolumesIndex);
    }
}
