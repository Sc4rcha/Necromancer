using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class TutorialGroup : MonoBehaviour
{
    public GameObject[] Steps;

    private int stepCount;
    private WaitForSeconds waitShowTutorial;

    protected TutorialManager manager;

    public void Setup (TutorialManager manager)
    {
        this.manager = manager;
        waitShowTutorial = new WaitForSeconds(1.5f);

        // hide tutorials
        gameObject.SetActive(false);
        foreach (var obj in Steps)
            obj.SetActive(false);
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
        foreach (var obj in Steps)
            obj.SetActive(false);

        // unlock player
        manager.GroupHand.Lock(false);
    }

    public void ButtonNext() 
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
            obj.SetActive(false);

        // show current tutorial step
        Steps[stepCount].SetActive(true);
    }


    private IEnumerator ShowTutorialDelay() 
    {
        yield return waitShowTutorial;

        ShowStep();
    }
}
