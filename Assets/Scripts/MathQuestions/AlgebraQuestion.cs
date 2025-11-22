using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/Questions/Algebra", fileName = "AlgebraQuestion")]
public class AlgebraQuestion : BaseQuestion
{
    [Header("Equation: ax + b = c")]
    public int a = 1;
    public int b = 0;
    public int c = 0;

    public override string GetQuestionText()
    {
        return $"{a}x + {b} = {c}";
    }

    public override string GetCorrectAnswerText()
    {
        if (a == 0) return "No solution";
        float x = (float)(c-b)/a;
        return x.ToString("0.##");
    }

    public override bool CheckAnswer(string playerAnswer)
    {
        if(float.TryParse(playerAnswer, out float val))
        {
            float correct = float.Parse(GetCorrectAnswerText());
            return Mathf.Approximately(val, correct);
        }
        return false;
    }

    public override void OnRenderBoard(GameObject boardroot)
    {
        Debug.Log($"Rendering Algebra Question on board not implemented yet. {GetQuestionText()}");
    }
}
