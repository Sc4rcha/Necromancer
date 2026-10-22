using System.Text.RegularExpressions;
using UnityEngine;

public class StoryText : MonoBehaviour
{
    public TMPro.TMP_Text Text;

    public void SetFormattedText(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            Text.text = "";
            return;
        }

        // character before icon on bold
        input = Regex.Replace(input, @"(.)(?=Club|Spade)", "<b>$1</b>");

        // Replace card names with sprites
        input = input.Replace("Club", "<sprite=0>");
        input = input.Replace("Spade", "<sprite=1>");

        // Format text between **double asterisks** first
        input = Regex.Replace(
            input,
            @"\*\*(.+?)\*\*",
            "<b><color=#A80028>$1</color></b>"
        );

        // Format text between *single asterisks*
        input = Regex.Replace(
            input,
            @"\*(.+?)\*",
            "<b><color=#822258>$1</color></b>"
        );

        Text.text = input;
    }
}
