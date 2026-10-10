using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardObjectBattlefieldEnemy : CardObjectBattlefield
{
    [Header ("Enemy")]
    public DropObjectEnemy Dropper;
    public RectTransform CardHolder;
    public ParticleSystem ReviveParticles;
    public ParticleSystem ReviveParticlesBurst;
    public AudioSource SFXHitRevive;
    public AudioSource SFXDead;
    [Space]
    public FXCardCapture FXCapture;

    private List<CardObject> attacks;
    private int attacksTotalValue;

    private WaitForSeconds waitForDeadCardShake;


    public override void Setup(BattlefieldSlot battlefieldSlot)
    {
        base.Setup(battlefieldSlot);

        // setup variables
        attacks = new List<CardObject>();
        attacksTotalValue = 0;

        // setup dropper and lock
        Dropper.Setup(this);
        Dropper.Lock(true);

        waitForDeadCardShake = new WaitForSeconds(0.3f);
    }

    public override void ShowHide(bool isShow)
    {
        base.ShowHide(isShow);

        // hide capture effect
        if (!isShow)
            FXCapture.Hide();
    }

    public override void Activate()
    {
        base.Activate();

        Dropper.Lock(false);
    }

    public void AddAttack(CardObjectMove card)
    {
        // add attacker
        card.transform.SetParent(CardHolder);
        card.Lock(true);
        attacks.Add(card);

        // force vertical layout group update
        LayoutRebuilder.ForceRebuildLayoutImmediate(CardHolder);

        // move dropper down
        Dropper.transform.position = attacks[attacks.Count - 1].transform.position;

        // add attack to total value
        attacksTotalValue += card.CardReference.Value;

        // activate particle feedback if revive active
        if (CheckRevive() && !ReviveParticles.isPlaying)
        {
            CardImage.sprite = CardReference.SpriteHand;

            ReviveParticlesBurst.Play();
            ReviveParticles.Play();

            // replace card drop SFX for hit revive
            SFXHitRevive.Play();
            card.SFXPickupDrop.Stop();

            // do capture effect
            FXCapture.Show();
        }

        // kill if attack values is over card value
        if (attacksTotalValue >= CardReference.Value)
        {
            // replace card drop SFX for hit sfx
            if (card.SFXPickupDrop.isPlaying)
            {
                card.SFXPlayAttack();
                card.SFXPickupDrop.Stop();
            }

            // centre and lock dropper
            Dropper.transform.localPosition = Vector2.zero;
            Dropper.Lock(true);


            StartCoroutine(DamageAnimation());
        }
        else
        {
            // check player game over if player has no more cards and enemy survived
            ManagerGameplay.Instance.CheckGameOver();
        }
    }

    private void Kill() 
    {
        // check revive and revive, otherwise discard
        if (CheckRevive())
        {
            // add card to player
            ManagerGameplay.Instance.GroupHand.CardAdd(CardReference);
            // visual
            ManagerGameplay.Instance.CardVisuals.MoveCardPickup(GetComponent<RectTransform>(), ManagerGameplay.Instance.GroupHand);

            // save progress
            GameManager.Instance.CardUnlock(CardReference);
        }
        else
            ManagerGameplay.Instance.Discard.DiscardCard(this);

        // discard attacks
        foreach (CardObject card in attacks)
        {
            ManagerGameplay.Instance.Discard.DiscardCard(card);
            card.gameObject.SetActive(false);
        }

        // deactivate card
        ShowHide(false);

        // stop particles
        ReviveParticles.Stop();

        // reset variables
        attacks = new List<CardObject>();
        attacksTotalValue = 0;

        // clear slot
        battlefieldSlot.ClearSlot();

        // check for reveal battlefield
        ManagerGameplay.Instance.RevealBattlefield();
        // check for game over
        ManagerGameplay.Instance.CheckGameOver();
    }
    private bool CheckRevive() 
    {
        // go though attacks and check for correct suit
        foreach (CardObject card in attacks)
        {
            // if enemy = Clubs search for DIAMOND
            if (CardReference.Suit == GameInfo.SUIT.Clubs && card.CardReference.Suit == GameInfo.SUIT.Diamonds)
                return true;

            // if enemy = Spades search for HEARTS
            if (CardReference.Suit == GameInfo.SUIT.Spades && card.CardReference.Suit == GameInfo.SUIT.Hearts)
                return true;
        }

        // achivement canon run fail
        GameManager.Instance.Achivements.CanonRunFail();

        return false;
    }

    private IEnumerator DamageAnimation() 
    {
        //initial delay
        yield return waitForDeadCardShake;

        // go thourgh attacks and shake
        for (int i = attacks.Count - 1; i >= 0; i--)
        {
            if (i < attacks.Count - 1) 
            {
                // play sfx
                (attacks[i] as CardObjectMove).SFXPlayAttack();

                // shake card
                attacks[i].Shake.StartShake();
                yield return waitForDeadCardShake;
            }
        }

        // play sfx
        SFXDead.Play();

        Shake.StartShake();

        // final delay
        yield return waitForDeadCardShake;

        // kill card
        Kill();
    }
}
