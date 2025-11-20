using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum MathOperator { Add, Subtract, Multiply, Divide }

[CreateAssetMenu(fileName = "NewMathQuestion", menuName = "Quiz/MathQuestion")]
public class MathQuestion : ScriptableObject
{
    [Header("Question Configurator")]
    public List<int> operands;
    public List<MathOperator> operators;

    public string GetQuestionText()
    {
        string question = operands[0].ToString(); // Start with the first operand

        for (int i = 0; i < operators.Count; i++)
        {
            string opSymbol = operators[i] switch
            {
                MathOperator.Add => "+",
                MathOperator.Subtract => "-",
                MathOperator.Multiply => "*",
                MathOperator.Divide => "/",
                _ => "?"
            };

            question += $" {opSymbol} {operands[i + 1]}"; // Append operator and next operand
        }

        return $"{question} = ?";
    }

    public int GetCorrectAnswer()
    {
        return EvaluateExpression();
    }

    private int EvaluateExpression()
    {
        // Convert operands and operators into a single list for evaluation
        List<string> expressionParts = new List<string>();
        for (int i = 0; i < operands.Count; i++)
        {
            expressionParts.Add(operands[i].ToString());
            if (i < operators.Count)
            {
                string opSymbol = operators[i] switch
                {
                    MathOperator.Add => "+",
                    MathOperator.Subtract => "-",
                    MathOperator.Multiply => "*",
                    MathOperator.Divide => "/",
                    _ => "?"
                };
                expressionParts.Add(opSymbol);
            }
        }

        // Evaluate the expression with operator precedence
        return EvaluateWithPrecedence(expressionParts);
    }

    private int EvaluateWithPrecedence(List<string> expressionParts)
    {
        Stack<int> values = new Stack<int>();
        Stack<string> ops = new Stack<string>();

        for (int i = 0; i < expressionParts.Count; i++)
        {
            string part = expressionParts[i];

            // If the part is a number, push it to the values stack
            if (int.TryParse(part, out int num))
            {
                values.Push(num);
            }
            else
            {
                // While the top of the ops stack has higher or equal precedence, apply the operator
                while (ops.Count > 0 && Precedence(part) <= Precedence(ops.Peek()))
                {
                    int b = values.Pop();
                    int a = values.Pop();
                    values.Push(ApplyOperator(ops.Pop(), a, b));
                }
                ops.Push(part);
            }
        }

        // Apply remaining operators
        while (ops.Count > 0)
        {
            int b = values.Pop();
            int a = values.Pop();
            values.Push(ApplyOperator(ops.Pop(), a, b));
        }

        // The final value in the stack is the result
        return values.Pop();
    }

    private int ApplyOperator(string op, int a, int b)
    {
        return op switch
        {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" => b != 0 ? a / b : 0, // Handle division by zero
            _ => 0
        };
    }

    private int Precedence(string op)
    {
        return op switch
        {
            "+" => 1,
            "-" => 1,
            "*" => 2,
            "/" => 2,
            _ => 0
        };
    }
}