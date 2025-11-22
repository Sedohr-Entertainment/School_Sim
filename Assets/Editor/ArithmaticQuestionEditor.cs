using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArithmeticQuestion))]
public class ArithmaticQuestionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var q = (ArithmeticQuestion)target;

        // Metadata
        q.category = (MathCategory)EditorGUILayout.EnumPopup("Category", q.category);
        q.difficulty = EditorGUILayout.IntSlider("Difficulty", q.difficulty, 1, 10);

        EditorGUILayout.Space();

        // Operands
        EditorGUILayout.LabelField("Operands:", EditorStyles.boldLabel);
        for (int i = 0; i < q.operands.Count; i++)
        {
            q.operands[i] = EditorGUILayout.IntField($"Operand {i + 1}", q.operands[i]);
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Operand")) q.operands.Add(0);
        if (GUILayout.Button("Remove Last Operand") && q.operands.Count > 0) q.operands.RemoveAt(q.operands.Count - 1);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Operators
        EditorGUILayout.LabelField("Operators:", EditorStyles.boldLabel);
        for (int i = 0; i < q.operators.Count; i++)
        {
            q.operators[i] = (MathOperator)EditorGUILayout.EnumPopup($"Operator {i + 1}", q.operators[i]);
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Operator")) q.operators.Add(MathOperator.Add);
        if (GUILayout.Button("Remove Last Operator") && q.operators.Count > 0) q.operators.RemoveAt(q.operators.Count - 1);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Preview
        EditorGUILayout.LabelField("Equation Preview:", EditorStyles.boldLabel);
        string preview = q.GetQuestionText();
        string answer = q.GetCorrectAnswerText();
        EditorGUILayout.HelpBox($"{preview}\nAnswer: {answer}", MessageType.Info);

        if (GUI.changed) EditorUtility.SetDirty(q);
    }

}
