using UnityEngine;
using UnityEngine.Rendering;
using static GameInfo;

public class TutorialManager : ManagerGameplay
{
    [Header("Tutorial Level")]
    public SoCard[] InitialHand;
    public SoCard[] InitialBattlefield;
    public TutorialGroup[] TutorialGroups;

    private int roundsCurrent;

    private void Start()
    {
        // Skip if tutorial already complete
        if (GameSave.Instance.TutorialComplete == 1)
        {
            TutorialEnd();
            return;
        }

        Setup();

        // hide aces
        GroupAces.gameObject.SetActive(false);

        // setup tutorial groups
        foreach (TutorialGroup group in TutorialGroups)
            group.Setup(this);

        TutorialStart();
    }


    public void TutorialStart() 
    {
        // add hand to player
        foreach (SoCard card in InitialHand)
        {
            GroupHand.CardAdd(card.GetCard());
            CardVisuals.MoveCardDraw(Library.transform as RectTransform, GroupHand);
        }

        // pickup all remaining spells
        for (int i = 0; i < InitialBattlefield.Length; i++)
        {
            Battlefield[i].SetCard(InitialBattlefield[i].GetCard());
        }

        // force library order
        Library.Cards.Clear();
        foreach (SoCard card in ReferenceLibrary.Cards)
            Library.Cards.Add(card.GetCard());

        TutorialGroups[0].Show();
    }
    protected override void NewBattlefield()
    {
        roundsCurrent++;

        if (roundsCurrent == 1)
        {
            TutorialGroups[1].Show();
        }
        else if (roundsCurrent == 2)
        {
            TutorialGroups[2].Show();
        }

        base.NewBattlefield();
    }

    public override void GameWin()
    {
        // save tutorial complete
        GameSave.Instance.SaveTutorialComplete();

        TutorialGroups[3].Show();
        GroupAces.gameObject.SetActive(true);
    }

    public void TutorialEnd() 
    {
        ButtonReplay();
    }
}
