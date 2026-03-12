using UnityEngine;

public class CardTilt : MonoBehaviour
{
    public AudioSource SFXWoosh;

    private const float TiltMax = 15;
    private const float MoveStepMax = 0.4f;

    private Vector2 positionPrevious;

    private float moveStep;
    private float moveStepSmoothDamp = 0.05f;
    private float moveStepChangeSpeed;

    private void OnEnable()
    {
        // set initial position
        positionPrevious = transform.position;
        moveStep = 0;
        moveStepChangeSpeed = 0;
        transform.eulerAngles = Vector3.zero;
    }

    private void FixedUpdate()
    {
        // calculate move step value
        moveStep = Mathf.SmoothDamp (moveStep, transform.position.x - positionPrevious.x, ref moveStepChangeSpeed, moveStepSmoothDamp);
        moveStep = Mathf.Clamp(moveStep, -MoveStepMax, MoveStepMax);

        // play sfx
        // if (!SFXWoosh.isPlaying && Mathf.Abs(moveStep) == MoveStepMax)
        //    SFXWoosh.Play();

        // tilt card
        transform.eulerAngles = Vector3.forward * -TiltMax * moveStep / MoveStepMax;

        // save position for next frame
        positionPrevious = transform.position;
    }
}
