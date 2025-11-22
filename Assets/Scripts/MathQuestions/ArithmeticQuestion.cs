using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum MathOperator { Add, Subtract, Multiply, Divide }

[CreateAssetMenu(fileName = "ArithmeticQuestion", menuName = "Quiz/Questions/Arithmetic")]
public class ArithmeticQuestion : BaseQuestion
{
    [Header("Expression")]
    public List<int> operands = new List<int> { 1, 1 };
    public List<MathOperator> operators = new List<MathOperator> { MathOperator.Add };

    private string OpToSymbol(MathOperator op) => op switch
    {
        MathOperator.Add => "+",
        MathOperator.Subtract => "-",
        MathOperator.Multiply => "*",
        MathOperator.Divide => "/",
        _ => "?"
    };

    public override string GetQuestionText()
    {
        string question = operands.Count > 0 ? operands[0].ToString() : "";
        for (int i = 0; i < operators.Count && i + 1 < operands.Count; i++)
        {
            question += $" {OpToSymbol(operators[i])} {operands[i + 1]}";
        }
        return $"{question} = ?";
    }

    public override string GetCorrectAnswerText()
    {
        int result = EvaluateExpression();
        return result.ToString();
    }

    public override bool CheckAnswer(string playerAnswer)
    {
        // Basic numeric compare; future: tolerate whitespace, accept fractions for division if needed
        if (int.TryParse(playerAnswer, out int val))
        {
            return val.ToString() == GetCorrectAnswerText();
        }
        return false;
    }

    private int EvaluateExpression()
    {
        // Build token list
        var tokens = new List<string>();
        for (int i = 0; i < operands.Count; i++)
        {
            tokens.Add(operands[i].ToString());
            if (i < operators.Count) tokens.Add(OpToSymbol(operators[i]));
        }
        return EvaluateWithPrecedence(tokens);
    }

    private int EvaluateWithPrecedence(List<string> tokens)
    {
        var values = new Stack<int>();
        var ops = new Stack<string>();

        for (int i = 0; i < tokens.Count; i++)
        {
            string t = tokens[i];

            if (int.TryParse(t, out int num))
            {
                values.Push(num);
            }
            else
            {
                while (ops.Count > 0 && Precedence(t) <= Precedence(ops.Peek()))
                {
                    int b = values.Pop();
                    int a = values.Pop();
                    values.Push(ApplyOperator(ops.Pop(), a, b));
                }
                ops.Push(t);
            }
        }

        while (ops.Count > 0)
        {
            int b = values.Pop();
            int a = values.Pop();
            values.Push(ApplyOperator(ops.Pop(), a, b));
        }

        return values.Pop();
    }

    private int ApplyOperator(string op, int a, int b) => op switch
    {
        "+" => a + b,
        "-" => a - b,
        "*" => a * b,
        "/" => b != 0 ? a / b : 0,
        _ => 0
    };

    private int Precedence(string op) => op switch
    {
        "+" => 1,
        "-" => 1,
        "*" => 2,
        "/" => 2,
        _ => 0
    };

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