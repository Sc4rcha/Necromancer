using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FXLightFlutter : MonoBehaviour
{
    public float IntensityMax;
    public float IntensityMin;


    public float TimeToChange = 0.5f;
    public float TimerVariance;

    private Light2D lightToChange;

    private float intensityPast;
    private float intensityTarget;

    private float timer;

    private void Start()
    {
        lightToChange = GetComponent<Light2D>();
        intensityPast = lightToChange.intensity;
        intensityTarget = Random.Range(IntensityMin, IntensityMax);
    }

    void Update()
    {
        lightToChange.intensity = Mathf.Lerp(intensityPast, intensityTarget, timer / TimeToChange);

        timer += Time.deltaTime;
        if (timer > TimeToChange)
        {
            intensityPast = intensityTarget;
            intensityTarget = Random.Range(IntensityMin, IntensityMax);

            timer = Random.Range (0, TimerVariance);
        }
    }
}
