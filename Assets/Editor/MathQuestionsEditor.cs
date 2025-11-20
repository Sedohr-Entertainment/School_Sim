using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MathQuestion))]
public class MathQuestionsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        MathQuestion mathQuestion = (MathQuestion)target;

        //Display operands
        EditorGUILayout.LabelField("Operands: ");
        for (int i = 0; i < mathQuestion.operands.Count; i++)
        {
            mathQuestion.operands[i] = EditorGUILayout.IntField($"Operand {i + 1}", mathQuestion.operands[i]);
        }

        // Buttons to add/remove operands
        if (GUILayout.Button("Add Operand"))
        {
            mathQuestion.operands.Add(0);
        }
        if (GUILayout.Button("Remove Last Operand") && mathQuestion.operands.Count > 0)
        {
            mathQuestion.operands.RemoveAt(mathQuestion.operands.Count - 1);
        }
        EditorGUILayout.Space();

        //Display operators
        EditorGUILayout.LabelField("Operators: ");
        for (int i = 0; i < mathQuestion.operators.Count; i++)
        {
            mathQuestion.operators[i] = (MathOperator)EditorGUILayout.EnumPopup($"Operator {i + 1}", mathQuestion.operators[i]);
        }

        //Add/Remove operator buttons
        if (GUILayout.Button("Add Operator"))
        {
            mathQuestion.operators.Add(MathOperator.Add);
        }
        if (GUILayout.Button("Remove Last Operator") && mathQuestion.operators.Count > 0)
        {
            mathQuestion.operators.RemoveAt(mathQuestion.operators.Count - 1);
        }

        EditorGUILayout.Space();

        // Display preview of the equation
        EditorGUILayout.LabelField("Equations Preview: ", EditorStyles.boldLabel);
        string equationPreview = mathQuestion.GetQuestionText();
        int correctAnswer = mathQuestion.GetCorrectAnswer();
        EditorGUILayout.HelpBox($"{equationPreview} Answer: {correctAnswer} ", MessageType.Info);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(mathQuestion);
        }
    }
}
