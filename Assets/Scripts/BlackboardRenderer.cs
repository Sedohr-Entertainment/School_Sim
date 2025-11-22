using TMPro;
using UnityEngine;

public class BlackboardRenderer : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI; // assign in inspector

    public void ShowEquation(string equationText)
    {
        if (textMeshProUGUI != null)
        {
            Debug.Log("Updating Blackboard Text: " + equationText);
            textMeshProUGUI.text = equationText;
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI reference not set!");
        }
    }

    public void ClearBoard()
    {
        if (textMeshProUGUI != null)
            textMeshProUGUI.text = "";
    }

}
