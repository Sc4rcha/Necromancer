using UnityEngine;
using UnityEngine.UI;

public class FXPost : MonoBehaviour
{
    [Header("Light")]
    public FXBackMovement MovementLight;
    [Space]
    public float MovementLightLow;
    public float MovementLightMedium;
    public float MovementLightHigh;

    [Header("Background")]
    public SpriteRenderer BackgroundImage;


    public enum MovementStrength {Low, Medium, High }

    public void ChangeMoveStrength(MovementStrength strength)
    {
        switch (strength)
        {
            case MovementStrength.Low:
                MovementLight.MovementMultiplier = MovementLightLow;
                break;
            case MovementStrength.Medium:
                MovementLight.MovementMultiplier = MovementLightMedium;
                break;
            case MovementStrength.High:
                MovementLight.MovementMultiplier = MovementLightHigh;
                break;
        }
    }

    public void SetBackground(Sprite backgroundNew) 
    {
        BackgroundImage.sprite = backgroundNew;
    }
}
