using UnityEngine;

public enum MathOperator { Add, Subtract, Multiply, Divide }

[CreateAssetMenu(fileName = "NewMathQuestion", menuName = "Quiz/MathQuestion")]
public class MathQuestion : ScriptableObject
{
    public int operandA;
    public int operandB;
    public MathOperator mathOperator;

    public string GetQuestionText()
    {
        string opSymbol = mathOperator switch
        {
            MathOperator.Add => "+",
            MathOperator.Subtract => "-",
            MathOperator.Multiply => "×",
            MathOperator.Divide => "÷",
            _ => "?"
        };
        //UI For Question
        return $"{operandA} {opSymbol} {operandB}?";
    }

    public int GetCorrectAnswer()
    {
        return mathOperator switch
        {
            MathOperator.Add => operandA + operandB,
            MathOperator.Subtract => operandA - operandB,
            MathOperator.Multiply => operandA * operandB,
            MathOperator.Divide => operandB != 0 ? operandA / operandB : 0,
            _ => 0
        };
    }
}