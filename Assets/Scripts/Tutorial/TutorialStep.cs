using UnityEngine;

public class TutorialStep : MonoBehaviour
{
    private TutorialGroup group;

    public void Setup(TutorialGroup group) 
    {
        this.group = group;
        gameObject.SetActive(false);
    }

    public void Interact() 
    {
        group.Next();
    }
}
