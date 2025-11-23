using TMPro;
using UnityEngine;

public class BlackboardRenderer : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI; // assign in inspector
    public LineRenderer lineRenderer; // assign in inspector


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

    public void DrawParabola(float a, float b, float c, float range = 5f, int resolution = 100)
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = resolution;
        float step = (2 * range) / (resolution - 1);

        for (int i = 0; i < resolution; i++)
        {
            float x = -range + i * step;
            float y = a * x * x + b * x + c;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }


}
