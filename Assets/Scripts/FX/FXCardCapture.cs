using UnityEngine;

public class FXCardCapture : MonoBehaviour
{
    public Transform[] Chains;

    public void Show() 
    {
        foreach (var chain in Chains)
        {
            chain.up = Random.insideUnitCircle.normalized;
            chain.gameObject.SetActive(true);
        }
    }
    public void Hide() 
    {
        foreach (var chain in Chains)
            chain.gameObject.SetActive(false);
    }
}
