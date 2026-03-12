using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public TMPro.TMP_Text VFXValue;
    public Slider VFXSlider;

    public void UpdateVolumeVFX(Slider slider) 
    {
        GameSave.Instance.VolumeChange(slider.value);
        VFXValue.text = GameSave.Instance.VolumeVFX.ToString();
    }

    public void ButtonOpen() 
    {
        VFXSlider.value = GameSave.Instance.VolumeVFX;
        VFXValue.text = GameSave.Instance.VolumeVFX.ToString();

        gameObject.SetActive(true);
    }
    public void ButtonClose() 
    {
        GameSave.Instance.SaveVolume();
        gameObject.SetActive(false);
    }

}
