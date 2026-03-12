using UnityEngine;

public class TutorialGroupLast : TutorialGroup
{
    public override void Hide()
    {
        base.Hide();

        manager.TutorialEnd();
    }
}
