using UnityEngine;
using UnityEngine.UI;

public class InfoRounds : MonoBehaviour
{
    public Image Progress;

    public void RefreshRounds(int cardsInLibrary) 
    {
        Progress.fillAmount = ((float)cardsInLibrary) / 36;
    }
}
