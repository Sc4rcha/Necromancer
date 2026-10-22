using UnityEngine;

public class FXHand : MonoBehaviour
{
    public static FXHand Instance;

    public Sprite[] Idle;
    public Sprite Drop;
    public Sprite Pickup;
    public Sprite[] Capture;
    public Sprite[] Spell;
    public Sprite[] Zombie;
    public Sprite[] Kill;
    [Space]
    public AnimationCurve OpacityCurve;
    [Space]
    public SpriteRenderer HandRenderer;

    private enum HandState { Idle, Pickup}
    private HandState state;

    private float timerTotalMin = 0.75f;
    private float timerTotalMax = 1.75f;
    private float timer;

    private void Start()
    {
        Instance = this;

        PickupEnd();
    }

    private void Update()
    {
        switch (state)
        {
            case HandState.Idle:
                UpdateIdle();
                break;
            case HandState.Pickup:
                UpdatePickup();
                break;
        }
    }

    private void UpdateIdle() 
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = Random.Range (timerTotalMin,timerTotalMax);
            HandRenderer.sprite = Idle[Random.Range(0, Idle.Length)];
            HandRenderer.flipX = Random.Range(0, 2) == 0;
        }
    }
    private void UpdatePickup() 
    {

    }

    public void PickupStart() 
    {
        state = HandState.Pickup;

        SetSprite(Pickup);
    }
    public void PickupEnd()
    {
        state = HandState.Idle;

        SetSprite(Drop);
    }

    public void HandCapture()
    {
        SetSprite(Capture[Random.Range(0, Capture.Length)]);
    }
    public void HandAttackZombie() 
    {
        SetSprite(Zombie[Random.Range(0, Zombie.Length)]);
    }
    public void HandAttackSpell() 
    {
        SetSprite(Spell[Random.Range(0, Spell.Length)]);
    }
    public void HandKill() 
    {
        SetSprite(Kill[Random.Range(0, Kill.Length)]);
    }

    private void SetSprite(Sprite sprite) 
    {
        timer = timerTotalMin;
        HandRenderer.sprite = sprite;

        HandRenderer.flipX = Random.Range(0, 2) == 0;
    }

}
