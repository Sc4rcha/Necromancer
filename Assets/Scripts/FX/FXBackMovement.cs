using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FXBackMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveMouse;
    public float MovementMultiplier;

    private Vector2 targetPosition;
    private Vector2 smoothDampVelocity;
    private Vector2 positionInitial;

    private void Awake()
    {
        positionInitial = transform.localPosition;
    }

    private void OnEnable()
    {
        moveMouse.action.Enable();
        transform.localPosition = positionInitial + (Vector2)Camera.main.ScreenToWorldPoint(moveMouse.action.ReadValue<Vector2>()) * -MovementMultiplier;
    }

    private void Update()
    {
        targetPosition = positionInitial + (Vector2)Camera.main.ScreenToWorldPoint(moveMouse.action.ReadValue<Vector2>()) * -MovementMultiplier;

        transform.localPosition = Vector2.SmoothDamp(transform.localPosition, targetPosition, ref smoothDampVelocity, 0.5f);
    }
}
