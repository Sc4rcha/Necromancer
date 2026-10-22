using System.Collections;
using UnityEngine;

public class TutorialGroup : MonoBehaviour
{
    public TutorialStep[] Steps;

    private int stepCount;
    private WaitForSeconds waitShowTutorial;

    protected TutorialManager manager;

    public void Setup (TutorialManager manager)
    {
        this.manager = manager;
        waitShowTutorial = new WaitForSeconds(1.5f);

        // hide tutorials
        gameObject.SetActive(false);
        foreach (var step in Steps)
            step.Setup(this);
    }

    public void Show() 
    {
        // set step count to 0
        stepCount = 0;

        // actiavte and show first tutorial step
        gameObject.SetActive(true);

        // lock player
        manager.GroupHand.Lock(true);

        // Show tutorial delay
        StartCoroutine(ShowTutorialDelay());
    }
    public virtual void Hide()
    {
        // hide tutorials
        gameObject.SetActive(false);

        // unlock player
        manager.GroupHand.Lock(false);
    }

    public void Next() 
    {
        // add step count
        stepCount++;

        // if group is done end
        if (stepCount == Steps.Length)
            Hide();
        // if group is NOT done show next step
        else
            ShowStep();
    }

    private void ShowStep() 
    {
        // hide all tutorial steps
        foreach (var obj in Steps)
            obj.gameObject.SetActive(false);

        // show current tutorial step
        Steps[stepCount].gameObject.SetActive(true);
    }


    private IEnumerator ShowTutorialDelay() 
    {
        yield return waitShowTutorial;

        ShowStep();
    }
}
