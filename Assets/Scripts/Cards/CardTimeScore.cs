using UnityEngine;

public class CardTimeScore : CardObject
{
    [Header("Card Time Score")]
    public AudioSource SFX;
    public TMPro.TMP_Text Timer;

    public void SetScore (int time)
    {
        Timer.text = time.ToString();

        Shake.StartShake();

        SFX.Play();
    }
}
