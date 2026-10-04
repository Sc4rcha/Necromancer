using UnityEngine;

[CreateAssetMenu(fileName = "Story Segment", menuName = "Scriptable Objects/Story Segment")]
public class SoStorySegment : ScriptableObject
{
    public string Title;
    public SoCard[] CorrectOrder;
}
