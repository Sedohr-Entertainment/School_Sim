using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/Questions/Algebra", fileName = "AlgebraQuestion")]
public class AlgebraQuestion : BaseQuestion
{
    [Header("Equation Terms (Left Side)")]
    public List<AlgebraTerm> leftSide = new List<AlgebraTerm>();

    [Header("Right Side Constant (Right Side)")]
    public int rightSide = 0;

    public override string GetQuestionText()
    {
        string equation = "";
        for (int i = 0; i < leftSide.Count; i++)
        {
            var term = leftSide[i];
            string part = term.hasVariable ? $"{term.coefficient}x" : $"{term.coefficient}";
            if (i > 0 && term.coefficient >= 0) equation += " + ";
            equation += part;
        }
        return $"{equation} = {rightSide}";
    }

    public override string GetCorrectAnswerText()
    {
        int xCoeff = 0;
        int consantSum = 0;

        foreach (var term in leftSide)
        {
            if(term.hasVariable)
                xCoeff += term.coefficient;
            else
                consantSum += term.coefficient;
        }

        if (xCoeff == 0) return "No solution";
        float x = (float)(rightSide - consantSum)/xCoeff;
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
        // Optional: Custom rendering logic for arithmetic questions
        var renderer = boardroot.GetComponent<BlackboardRenderer>();
        if (renderer != null)
        {
            renderer.ShowEquation(GetQuestionText());
        }
    }
}
