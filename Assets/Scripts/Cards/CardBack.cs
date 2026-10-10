using UnityEngine;
using UnityEngine.UI;

public class CardBack : MonoBehaviour
{
    public Sprite[] CardBacks;

    private void OnEnable()
    {
        GetComponent<Image>().sprite = CardBacks[Random.Range (0, CardBacks.Length)];
    }
}
