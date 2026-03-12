using System.Collections;
using UnityEngine;

public class CardShake : MonoBehaviour
{
    public AnimationCurve SizeCurve;

    private const float shakeTime = 0.15f;
    private float shakeTimeCurrent;

    private const float shakeSizeAmount = 0.4f;

    private WaitForEndOfFrame waitEndOfFrame;

    public void StartShake() 
    {
        // stop if card is deactivated
        if (!gameObject.activeInHierarchy)
            return;

        waitEndOfFrame = new WaitForEndOfFrame();

        // stop previous coroutine
        StopAllCoroutines();
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;

        // start coroutine
        StartCoroutine(Shake());
    }

    private IEnumerator Shake() 
    {
        shakeTimeCurrent = 0;

        while (shakeTimeCurrent < shakeTime) 
        {
            transform.localScale = Vector3.one + Vector3.one * shakeSizeAmount * SizeCurve.Evaluate(shakeTimeCurrent / shakeTime);
            shakeTimeCurrent += Time.deltaTime;
            yield return waitEndOfFrame;
        }

        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }

}
