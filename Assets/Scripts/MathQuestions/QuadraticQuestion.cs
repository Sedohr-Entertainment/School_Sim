using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/Questions/Quadratic", fileName = " QuadraticQuestion")]
public class QuadraticQuestion : BaseQuestion
{
    [Header("Equation: ax² + bx + c = 0")]
    public float a = 1;
    public float b = 0;
    public float c = 0;

    public override string GetQuestionText()
    {
        return $"{a}x² + {b}x + {c} = 0";
    }

    public override string GetCorrectAnswerText()
    {
        if (Math.Abs(a) < 1e-6) return "Not quadratic";

        double discriminant = b * b - 4 * a * c;
        if (discriminant < 0) return "No real solutions";

        if (Math.Abs(discriminant) < 1e-6)
        {
            double x = -b / (2.0 * a);
            return x.ToString("0.##");
        }
        else
        {
            double sqrtDisc = Math.Sqrt(discriminant);
            double x1 = (-b + sqrtDisc) / (2.0 * a);
            double x2 = (-b - sqrtDisc) / (2.0 * a);
            return $"{x1:0.##}, {x2:0.##}";
        }
    }

    public override bool CheckAnswer(string playerAnswer)
    {
        string correct = GetCorrectAnswerText();
        if (correct == "No real solutions" || correct == "Not quadratic")
            return playerAnswer.Trim().Equals(correct, StringComparison.OrdinalIgnoreCase);

        var correctParts = correct.Split(',');
        var playerParts = playerAnswer.Split(',');

        if (playerParts.Length != correctParts.Length) return false;

        for (int i = 0; i < correctParts.Length; i++)
        {
            if (!double.TryParse(playerParts[i], out double val)) return false;
            if (!double.TryParse(correctParts[i], out double correctVal)) return false;
            if (!Mathf.Approximately((float)val, (float)correctVal)) return false;
        }
        return true;
    }

    public override void OnRenderBoard(GameObject boardRoot)
    {
        var renderer = boardRoot.GetComponent<BlackboardRenderer>();
        if (renderer != null)
        {
            renderer.ShowEquation(GetQuestionText());
            if (renderer.lineRenderer != null)
                renderer.DrawParabola(a, b, c); // we'll add this next
        }
    }

}
