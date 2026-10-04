using UnityEngine;
using UnityEngine.UI;

public class InfoRounds : MonoBehaviour
{
    public Image Progress;
    public TMPro.TMP_Text Value;

    public void RefreshRounds(int cardsInLibrary) 
    {
        Progress.fillAmount = ((float)cardsInLibrary) / 36;
        Value.text = (cardsInLibrary / 4).ToString();
    }
}
