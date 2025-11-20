using UnityEngine;

public class DynamicMathQuestion
{
    public int operandA;
    public int operandB;
    public MathOperator mathOperator;

    public DynamicMathQuestion()
    {
        operandA = Random.Range(1, 10);
        operandB = Random.Range(1, 10);
        mathOperator = (MathOperator)Random.Range(0, 4);
    }

    public string GetQuestionText() => $"{operandA} {GetSymbol()} {operandB} = ?";
    public int GetCorrectAnswer() => mathOperator switch
    {
        MathOperator.Add => operandA + operandB,
        MathOperator.Subtract => operandA - operandB,
        MathOperator.Multiply => operandA * operandB,
        MathOperator.Divide => operandB != 0 ? operandA / operandB : 0,
        _ => 0
    };

    private string GetSymbol() => mathOperator switch
    {
        MathOperator.Add => "+",
        MathOperator.Subtract => "-",
        MathOperator.Multiply => "×",
        MathOperator.Divide => "÷",
        _ => "?"
    };
}