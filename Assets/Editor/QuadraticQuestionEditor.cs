using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuadraticQuestion))]
public class QuadraticQuestionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var q = (QuadraticQuestion)target;

        q.category = (MathCategory)EditorGUILayout.EnumPopup("Category", q.category);
        q.difficulty = EditorGUILayout.IntSlider("Difficulty", q.difficulty, 1, 10);

        q.a = EditorGUILayout.FloatField("Coefficient a", q.a);
        q.b = EditorGUILayout.FloatField("Coefficient b", q.b);
        q.c = EditorGUILayout.FloatField("Constant c", q.c);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Equation Preview:", EditorStyles.boldLabel);
        string preview = q.GetQuestionText();
        string answer = q.GetCorrectAnswerText();
        EditorGUILayout.HelpBox($"{preview}\nSolutions: {answer}", MessageType.Info);

        if (GUI.changed) EditorUtility.SetDirty(q);
    }

}
