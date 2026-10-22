using UnityEngine;

public class SFXTutorialBell : MonoBehaviour
{
    public float[] Pitches;
    public AudioSource Audio;

    private void OnEnable()
    {
        Audio.pitch = Pitches[Random.Range(0, Pitches.Length)];
        Audio.Play();
    }
}
